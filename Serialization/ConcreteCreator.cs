using System;

namespace Serialization
{
	public interface ICreator
	{
		ISerializer FactoryMethod(string type);
	}

	public class ConcreteCreator : ICreator
	{
		public ISerializer FactoryMethod(string type)
		{
			switch (type.ToLower().Trim('.'))
			{
				case "xml": // if type == xml return XMLSerializer
					return new XMLSerializer();

				case "bin": // if type == bin return BinarySerializer
					return new BinarySerializer();

				default:
					throw new ArgumentException("Invalid type: ", type);
			}
		}
	}
}
