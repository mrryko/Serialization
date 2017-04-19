using System.IO;

namespace Serialization
{
	public interface ISerializer
	{
		void Serialize(DirectoryInfo dir, string fileName);
		DirectoryDescription Deserialize(string filepath);
	}
}
