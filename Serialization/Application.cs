using System;
using System.IO;

namespace Serialization
{
	public class Application
	{
		static void Main(string[] args)
		{
			ICreator creator = new ConcreteCreator();
			ISerializer serializer;
			try
			{
				if (args.Length != 0)
				{
					if (args[0].Equals("help", StringComparison.InvariantCultureIgnoreCase))
					{
						Console.WriteLine("Serialize:");
						Console.WriteLine("ser directory_path file_name format");
						Console.WriteLine(@"Format must be ""xml"" or ""bin"" ");
						Console.WriteLine("Deserialize:");
						Console.WriteLine("des file_path");
					}
					else if (args[0].Equals("ser", StringComparison.InvariantCultureIgnoreCase))
					{
						DirectoryInfo dInfo = new DirectoryInfo(args[1]);
						string filePath = args[2];
						string serializationType = args[3];
						serializer = creator.FactoryMethod(serializationType);
						serializer.Serialize(dInfo, filePath);
						Console.WriteLine("Serialized");
					}
					else if (args[0].Equals("des", StringComparison.InvariantCultureIgnoreCase))
					{
						string filePath = args[1];
						string extension = Path.GetExtension(filePath);
						serializer = creator.FactoryMethod(extension);
						DirectoryDescription dirDesc = serializer.Deserialize(filePath);
						dirDesc.Print();
					}
				}
				else
				{
					Console.WriteLine(@"Wrong amount of arguments. Type ""help"" for help"); // Check for null array
					Console.Read();
				}
			}
			catch (IndexOutOfRangeException)
			{
				Console.WriteLine(@"Wrong amount of arguments. Type ""help"" for help");
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("There's no such file");
			}
			catch (ArgumentException)
			{
				Console.WriteLine(@"Wrong amount of arguments. Type ""help"" for help");
			}
			catch (UnauthorizedAccessException ex)
			{
				Console.WriteLine(ex.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				Console.WriteLine("Press any key to finish...");
				Console.ReadKey();
			}

		}
	}

	
}
