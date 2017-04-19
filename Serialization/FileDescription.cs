using System;
using System.Text;
using System.Xml.Serialization;

namespace Serialization
{


	[Serializable]
	public class FileDescription
	{
		[XmlAttribute]
		public string Name { set; get; } = "null";
		[XmlAttribute]
		public long Size { set; get; } = 0;
		[XmlAttribute]
		public DateTime CreationDate { set; get; } = DateTime.Now;
		[XmlAttribute]
		public string Attributes { set; get; } = "null";

		public FileDescription() { }

		#region public methods
		public override string ToString()
		{
			return $"Name: {Name} {Environment.NewLine}Size: {Size} { Environment.NewLine}CreationDate: { CreationDate} {Environment.NewLine}Attributes: {Attributes} {Environment.NewLine}";
		}

		public override bool Equals(object obj)
		{
			FileDescription test = obj as FileDescription;
			if (obj == null)
			{
				return false;
			}

			return Name == test.Name
				&& Size == test.Size
				&& CreationDate == test.CreationDate
				&& Attributes == test.Attributes;
		}
		
		public override int GetHashCode()
		{
			int hash = 19;
			hash = hash * 31 + Name.SafeGetHashCode();
			hash = hash * 31 + Size.GetHashCode();
			hash = hash * 31 + Attributes.SafeGetHashCode();
			hash = hash * 31 + CreationDate.GetHashCode();
			return hash;
		}
		#endregion



	}
}
