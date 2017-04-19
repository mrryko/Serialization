using System;
using NUnit.Framework;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;

namespace Serialization.Test
{
	[TestFixture]
	[Category("Serialization Tests")]
	public class SerializationTest
	{
		DirectoryDescription sampleDirDesc; // sample
		DirectoryInfo dirInfo = null;
		static string fileOutput = $@"shouldbe";
		string sampleDirectoryPath = null;

		[OneTimeSetUp]
		public void TestSetup()
		{
			string currentDirectory = GetCurrentDirectory();
			sampleDirectoryPath = $@"{currentDirectory}\test folder";
			CreateSampleDirectory(sampleDirectoryPath);
			dirInfo = new DirectoryInfo(sampleDirectoryPath);
			sampleDirDesc = DirectoryDescription.GetDerictoriesAndFiles(dirInfo);
		}

		[OneTimeTearDown]
		public void TestDown()
		{
			DeleteSampleDirectory();
		}

		[Test]
		public void ShouldSerializeXML()
		{
			XMLSerializer serializer = new XMLSerializer();
			serializer.Serialize(dirInfo, fileOutput);
			DirectoryDescription serializedDirDesc = serializer.Deserialize($"{fileOutput}.xml");
			Assert.AreEqual(sampleDirDesc, serializedDirDesc);
		}

		[Test]
		public void ShouldSerializeBin()
		{
			BinarySerializer serializer = new BinarySerializer();
			serializer.Serialize(dirInfo, fileOutput);
			DirectoryDescription otherDD = serializer.Deserialize($"{fileOutput}.bin");
			Assert.AreEqual(sampleDirDesc, otherDD);
		}

		private static void CreateSampleDirectory(string path)
		{
			try
			{
				string rootDir = $@"{path}\dir";
				Directory.CreateDirectory(rootDir);
				Directory.SetCurrentDirectory(rootDir);
				File.Create("file.txt").Dispose();
				File.Create("file2.txt").Dispose();
				Directory.CreateDirectory($@"{rootDir}\dir");
				Directory.SetCurrentDirectory($@"{rootDir}\dir");
				File.Create("file.txt").Dispose();
				File.Create("file2.txt").Dispose();
				Directory.SetCurrentDirectory(GetCurrentDirectory());

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		public static string GetCurrentDirectory()
		{
			string codeBase = Assembly.GetExecutingAssembly().CodeBase;
			UriBuilder uri = new UriBuilder(codeBase);
			string path = Uri.UnescapeDataString(uri.Path);
			return Path.GetDirectoryName(path);
		}

		private void DeleteSampleDirectory()
		{
			Directory.Delete(sampleDirectoryPath, true);
		}


	}
}
