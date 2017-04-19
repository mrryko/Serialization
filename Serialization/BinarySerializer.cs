using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialization
{
	public class BinarySerializer : ISerializer
	{
		#region public methods
		/// <summary>
		/// Serializes DirectoryInfo to file in XML format
		/// </summary>
		/// <param name="dInfo"></param>
		/// <param name="fileName"></param>
		public void Serialize(DirectoryInfo dInfo, string fileName)
		{
			DirectoryDescription dirDescription = DirectoryDescription.GetDerictoriesAndFiles(dInfo);
			WriteToFile(dirDescription, fileName);
		}

		/// <summary>
		/// Deserializes DirectoryInfo from file in XML format
		/// </summary>
		/// <param name="filepath"></param>
		public DirectoryDescription Deserialize(string filepath)
		{
			return ReadFromFile(filepath);
		}
		#endregion

		#region private methods		
		/// <summary>
		/// Writes DirectoryDescription to file in XML format
		/// </summary>
		/// <param name="dirDescription"></param>
		/// <param name="filepath"></param>
		private void WriteToFile(DirectoryDescription dirDescription, string filepath)
		{
			using (Stream stream = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				IFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(stream, dirDescription);
			}
		}


		/// <summary>
		/// Reads DirectoryDescription to file in XML format
		/// </summary>
		/// <param name="filepath"></param>
		/// <returns>DirectoryDescription</returns>
		private DirectoryDescription ReadFromFile(string filepath)
		{
			IFormatter binaryFormatter = new BinaryFormatter();
			using (Stream fStream = File.OpenRead(filepath))
			{
				return (DirectoryDescription)binaryFormatter.Deserialize(fStream);
			}
		}
		#endregion
	}

}

