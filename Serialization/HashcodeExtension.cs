namespace Serialization
{
	public static class HashcodeExtension
	{
		public static int SafeGetHashCode<T>(this T value) where T : class
		{
			return value == null ? 0 : value.GetHashCode();
		}		
	}
}
