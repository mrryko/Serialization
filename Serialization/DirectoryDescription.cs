using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Serialization
{
	[Serializable]
	public class DirectoryDescription
	{
		[XmlAttribute]
		public string Name { set; get; } = "null";
		[XmlAttribute]
		public DateTime CreationDate { set; get; } = DateTime.Now;
		[XmlAttribute]
		public string Attributes { set; get; } = "null";

		public List<FileDescription> SubFiles = new List<FileDescription>();
		public List<DirectoryDescription> SubDirectories = new List<DirectoryDescription>();

		private const string _formatter = "-";

		public DirectoryDescription() { }

		#region public methods
		public static DirectoryDescription GetDerictoriesAndFiles(DirectoryInfo dInfo)
		{
			return GetFilesRecursively(dInfo);
		}

		public void Print()
		{
			StringBuilder strBuilder = new StringBuilder(_formatter);
			PrintAllElements(this, strBuilder);
		}

		public override string ToString()
		{
			return $"Name: {Name} {Environment.NewLine}CreationDate: {CreationDate} { Environment.NewLine}Attributes: {Attributes} {Environment.NewLine}";
		}

		public override bool Equals(object obj)
		{
			DirectoryDescription test = obj as DirectoryDescription;
			if (obj == null)
			{
				return false;
			}

			return Name == test.Name
				&& Attributes == test.Attributes
				&& CreationDate == test.CreationDate
				&& SubDirectories.All(test.SubDirectories.Contains)
				&& SubFiles.All(test.SubFiles.Contains);
		}

		public override int GetHashCode()
		{
			int hash = 19;
			hash = hash * 31 + Name.SafeGetHashCode(); ;
			hash = hash * 31 + Attributes.SafeGetHashCode();
			hash = hash * 31 + CreationDate.GetHashCode();
			hash = hash * SubDirectories.GetHashCode();
			hash = hash * SubFiles.GetHashCode();
			return hash;
		}
		#endregion

		#region private methods
		private static DirectoryDescription GetFilesRecursively(DirectoryInfo dInfo)
		{
			DirectoryDescription dirDescription = new DirectoryDescription
			{
				Name = dInfo.Name,
				CreationDate = dInfo.CreationTime,
				Attributes = dInfo.Attributes.ToString()
			};

			try
			{
				foreach (var file in dInfo.GetFiles("*.*")) // get FileInfo[] array
				{
					dirDescription.SubFiles.Add(
						new FileDescription
						{
							Name = file.Name,
							Size = file.Length,
							CreationDate = file.CreationTime,
							Attributes = file.Attributes.ToString()
						});
				}

				DirectoryInfo[] subDirectories = dInfo.GetDirectories();
				foreach (var subDir in subDirectories)
				{
					dirDescription.SubDirectories.Add(GetFilesRecursively(subDir));
				}
			}
			catch (UnauthorizedAccessException e)
			{
				Console.WriteLine(e.Message);
			}
			catch (DirectoryNotFoundException e)
			{
				Console.WriteLine(e.Message);
			}

			return dirDescription;
		}

		private void PrintAllElements(DirectoryDescription dirDesc, StringBuilder formatter)
		{
			string tF = formatter.ToString(); // temp formatter
			string output = $"{tF} Directory{Environment.NewLine}{tF} Name: {dirDesc.Name}{Environment.NewLine}{tF} Attributes: {dirDesc.Attributes}{Environment.NewLine}{tF} CreationDate: {dirDesc.CreationDate.GetDateTimeFormats('G')[0]}";
			Console.WriteLine(output);
			Console.WriteLine();
			foreach (var file in dirDesc.SubFiles)
			{
				formatter.Append(_formatter); // formatting
				output = $"{tF} File{Environment.NewLine}{tF} Name: {file.Name}{Environment.NewLine}{tF} Size: {file.Size}{Environment.NewLine}{tF} Attributes: {file.Attributes}{Environment.NewLine}{tF} CreationDate: {file.CreationDate.GetDateTimeFormats('G')[0]}";
				Console.WriteLine(output);
				formatter.Remove(formatter.Length - _formatter.Length, _formatter.Length);
				Console.WriteLine();
			}
			foreach (var directory in dirDesc.SubDirectories)
			{
				formatter.Append(_formatter); // formatting
				PrintAllElements(directory, formatter);
				formatter.Remove(formatter.Length - _formatter.Length, _formatter.Length);
			}

		}


		
		#endregion
	}
}
