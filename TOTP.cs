using System.Security.Cryptography;

namespace TTMC.Tools
{
	public class TOTP
	{
		public static string Generate(byte[] secret, int totpLength = 6)
		{
			long counter = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / 30;
			byte[] counterBytes = BitConverter.GetBytes(counter);
			if (BitConverter.IsLittleEndian) { Array.Reverse(counterBytes); }
			HMACSHA1 hmac = new HMACSHA1(secret);
			byte[] hash = hmac.ComputeHash(counterBytes);
			int offset = hash[hash.Length - 1] & 0x0F;
			int binary = ((hash[offset] & 0x7F) << 24) | ((hash[offset + 1] & 0xFF) << 16) | ((hash[offset + 2] & 0xFF) << 8) | (hash[offset + 3] & 0xFF);
			int totpValue = binary % (int)Math.Pow(10, totpLength);
			return totpValue.ToString($"D{totpLength}");
		}
		public static bool CheckValid(string totp, byte[] secret, int totpLength = 6)
		{
			return totp == Generate(secret, totpLength);
		}
	}
}