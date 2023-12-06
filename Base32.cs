using System.Text;

namespace TTMC.Tools
{
	public class Base32
	{
		private static string base32Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
		public static string ToString(byte[] inArray)
		{
			StringBuilder result = new();
			int bitCount = 0;
			int currentByte = 0;
			foreach (byte b in inArray)
			{
				currentByte = (currentByte << 8) | b;
				bitCount += 8;
				while (bitCount >= 5)
				{
					int index = (currentByte >> (bitCount - 5)) & 0x1F;
					result.Append(base32Chars[index]);
					bitCount -= 5;
				}
			}
			if (bitCount > 0)
			{
				int index = (currentByte << (5 - bitCount)) & 0x1F;
				result.Append(base32Chars[index]);
			}
			return result.ToString();
		}
		public static byte[] FromString(string s)
		{
			List<byte> result = new();
			int bitCount = 0;
			int currentByte = 0;
			foreach (char c in s)
			{
				int index = base32Chars.IndexOf(c);
				if (index == -1)
				{
					throw new ArgumentException("Invalid base32 string.");
				}
				currentByte = (currentByte << 5) | index;
				bitCount += 5;
				if (bitCount >= 8)
				{
					result.Add((byte)(currentByte >> (bitCount - 8)));
					bitCount -= 8;
				}
			}
			return result.ToArray();
		}
	}
}