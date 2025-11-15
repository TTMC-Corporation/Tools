using System.Security.Cryptography;

namespace TTMC.Tools
{
	public class TOTP
	{
		public static string Generate(byte[] secret, long counter, int totpLength = 6)
		{
			byte[] counterBytes = BitConverter.GetBytes(counter);
			if (BitConverter.IsLittleEndian) { Array.Reverse(counterBytes); }
			HMACSHA1 hmac = new HMACSHA1(secret);
			byte[] hash = hmac.ComputeHash(counterBytes);
			int offset = hash[hash.Length - 1] & 0x0F;
			int binary = ((hash[offset] & 0x7F) << 24) | ((hash[offset + 1] & 0xFF) << 16) | ((hash[offset + 2] & 0xFF) << 8) | (hash[offset + 3] & 0xFF);
			int totpValue = binary % (int)Math.Pow(10, totpLength);
			return totpValue.ToString($"D{totpLength}");
		}
		public static bool CheckValid(string totp, byte[] secret, long neighbour = 1, int totpLength = 6)
		{
			long counter = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / 30;
			for (long i = (counter - neighbour); i <= (counter + neighbour); i++)
			{
				if (totp == Generate(secret, i, totpLength))
				{
					return true;
				}
			}
			return false;
		}
	}
}