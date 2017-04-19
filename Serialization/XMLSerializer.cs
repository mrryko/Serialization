using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Serialization
{
	public class XMLSerializer : ISerializer
	{
		#region public methods
		/// <summary>
		/// Serializes DirectoryInfo to file
		/// </summary>
		/// <param name="dInfo"></param>
		/// <param name="fileName"></param>
		public void Serialize(DirectoryInfo dInfo, string fileName)
		{
			DirectoryDescription dirDescription = DirectoryDescription.GetDerictoriesAndFiles(dInfo);
			WriteToFile(dirDescription, fileName);
		}
		/// <summary>
		///  Deserializes DirectoryInfo from file
		/// </summary>
		/// <param name="filepath"></param>
		public DirectoryDescription Deserialize(string filepath)
		{
			return ReadFromFile(filepath);
		}		
		#endregion

		#region private methods
		/// <summary>
		/// Writes DirectoryDescription to file in bin format
		/// </summary>
		/// <param name="dirDescription"></param>
		/// <param name="filepath"></param>
		private void WriteToFile(DirectoryDescription dirDescription, string filepath)
		{
			if (dirDescription != null)
			{
				XmlSerializer xmlFormat = new XmlSerializer(typeof(DirectoryDescription));
				using (Stream fStream = new FileStream(filepath,
					FileMode.Create, FileAccess.Write, FileShare.None))
				{
					xmlFormat.Serialize(fStream, dirDescription);
				}
			}
		}

		/// <summary>
		/// Reads DirectoryDescription to file in bin format
		/// </summary>
		/// <param name="filepath"></param>
		/// <returns></returns>
		private DirectoryDescription ReadFromFile(string filepath)
		{
			using (FileStream fs = new FileStream(filepath, FileMode.Open))
			{
				// Create an instance of the XmlSerializer specifying type and namespace.
				XmlSerializer serializer = new XmlSerializer(typeof(DirectoryDescription));

				using (XmlReader reader = XmlReader.Create(fs))
				{
					// Declare an object variable of the type to be deserialized.
					return (DirectoryDescription)serializer.Deserialize(reader);
				}
			}
		}
		#endregion
	}
}

