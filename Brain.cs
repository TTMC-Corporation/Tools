using System.Drawing;
using System.IO.Compression;
using System.Text;

namespace TTMC.Tools
{
	public class Engine
	{
		private static Random random = new();
		public static string See(byte[] bytes)
		{
			string resp = string.Empty;
			foreach (byte b in bytes)
			{
				resp += b + " ";
			}
			return resp[..^1];
		}
		public static string CreateRandomPassword(int length = 100, string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")
		{
			string text = string.Empty;
			for (int i = 0; i < length; i++)
			{
				text += validChars[random.Next(validChars.Length)];
			}
			return text;
		}
		public static byte[] Serialize(IEnumerable<byte> array)
		{
			return Combine(array.ToArray());
		}
		public static byte[] Serialize(IEnumerable<byte[]> array)
		{
			List<byte> list = new();
			foreach (byte[] item in array)
			{
				list.AddRange(Prefixed(item));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<string> array)
		{
			List<byte> list = new();
			foreach (string item in array)
			{
				list.AddRange(Prefixed(item));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<char> array)
		{
			List<byte> list = new();
			foreach (char item in array)
			{
				list.AddRange(BitConverter.GetBytes(item));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<bool> array)
		{
			List<byte> list = new();
			foreach (bool item in array)
			{
				list.AddRange(BitConverter.GetBytes(item));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<int> array)
		{
			List<byte> list = new();
			foreach (int item in array)
			{
				list.AddRange(BitConverter.GetBytes(item));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<uint> array)
		{
			List<byte> list = new();
			foreach (uint item in array)
			{
				list.AddRange(BitConverter.GetBytes(item));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<short> array)
		{
			List<byte> list = new();
			foreach (short item in array)
			{
				list.AddRange(BitConverter.GetBytes(item));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<ushort> array)
		{
			List<byte> list = new();
			foreach (ushort item in array)
			{
				list.AddRange(BitConverter.GetBytes(item));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<long> array)
		{
			List<byte> list = new();
			foreach (long item in array)
			{
				list.AddRange(BitConverter.GetBytes(item));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<ulong> array)
		{
			List<byte> list = new();
			foreach (ulong item in array)
			{
				list.AddRange(BitConverter.GetBytes(item));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<Half> array)
		{
			List<byte> list = new();
			foreach (Half item in array)
			{
				list.AddRange(BitConverter.GetBytes(item));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<float> array)
		{
			List<byte> list = new();
			foreach (float item in array)
			{
				list.AddRange(BitConverter.GetBytes(item));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<double> array)
		{
			List<byte> list = new();
			foreach (double item in array)
			{
				list.AddRange(BitConverter.GetBytes(item));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<Guid> array)
		{
			List<byte> list = new();
			foreach (Guid item in array)
			{
				list.AddRange(item.ToByteArray());
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<DateTime> array)
		{
			List<byte> list = new();
			foreach (DateTime item in array)
			{
				list.AddRange(BitConverter.GetBytes(item.ToBinary()));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<TimeSpan> array)
		{
			List<byte> list = new();
			foreach (TimeSpan item in array)
			{
				list.AddRange(BitConverter.GetBytes(item.Ticks));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<Color> array)
		{
			List<byte> list = new();
			foreach (Color item in array)
			{
				list.AddRange(BitConverter.GetBytes(item.ToArgb()));
			}
			return Combine(list.ToArray());
		}
		public static T Deserialize<T>(byte[] data)
		{
			if (typeof(T) == typeof(byte[][]))
			{
				List<byte[]> list = new();
				List<byte> tmp = data.ToList();
				while (tmp.Count > 0)
				{
					int length = VarintBitConverter.ToInt32(tmp.ToArray());
					tmp.RemoveRange(0, VarintBitConverter.GetVarintBytes(length).Length);
					list.Add(tmp.ToArray()[..length]);
					tmp.RemoveRange(0, length);
				}
				return (T)(object)list.ToArray();
			}
			else if (typeof(T) == typeof(string[]))
			{
				byte[][]? bytes = Deserialize<byte[][]>(data);
				if (bytes != null)
				{
					List<string> list = new();
					foreach (byte[] array in bytes)
					{
						string item = Encoding.UTF8.GetString(array);
						list.Add(item);
					}
					return (T)(object)list.ToArray();
				}
			}
			else if (typeof(T) == typeof(char[]))
			{
				List<char> list = new();
				for (int i = 0; i < data.Length; i += 2)
				{
					char item = BitConverter.ToChar(data, i);
					list.Add(item);
				}
				return (T)(object)list.ToArray();
			}
			else if (typeof(T) == typeof(bool[]))
			{
				List<bool> list = new();
				for (int i = 0; i < data.Length; i++)
				{
					bool item = BitConverter.ToBoolean(data, i);
					list.Add(item);
				}
				return (T)(object)list.ToArray();
			}
			else if (typeof(T) == typeof(int[]))
			{
				List<int> list = new();
				for (int i = 0; i < data.Length; i += 4)
				{
					int item = BitConverter.ToInt32(data, i);
					list.Add(item);
				}
				return (T)(object)list.ToArray();
			}
			else if (typeof(T) == typeof(uint[]))
			{
				List<uint> list = new();
				for (int i = 0; i < data.Length; i += 4)
				{
					uint item = BitConverter.ToUInt32(data, i);
					list.Add(item);
				}
				return (T)(object)list.ToArray();
			}
			else if (typeof(T) == typeof(short[]))
			{
				List<short> list = new();
				for (int i = 0; i < data.Length; i += 2)
				{
					short item = BitConverter.ToInt16(data, i);
					list.Add(item);
				}
				return (T)(object)list.ToArray();
			}
			else if (typeof(T) == typeof(ushort[]))
			{
				List<ushort> list = new();
				for (int i = 0; i < data.Length; i += 2)
				{
					ushort item = BitConverter.ToUInt16(data, i);
					list.Add(item);
				}
				return (T)(object)list.ToArray();
			}
			else if (typeof(T) == typeof(long[]))
			{
				List<long> list = new();
				for (int i = 0; i < data.Length; i += 8)
				{
					long item = BitConverter.ToInt64(data, i);
					list.Add(item);
				}
				return (T)(object)list.ToArray();
			}
			else if (typeof(T) == typeof(ulong[]))
			{
				List<ulong> list = new();
				for (int i = 0; i < data.Length; i += 8)
				{
					ulong item = BitConverter.ToUInt64(data, i);
					list.Add(item);
				}
				return (T)(object)list.ToArray();
			}
			else if (typeof(T) == typeof(Half[]))
			{
				List<Half> list = new();
				for (int i = 0; i < data.Length; i += 2)
				{
					Half item = BitConverter.ToHalf(data, i);
					list.Add(item);
				}
				return (T)(object)list.ToArray();
			}
			else if (typeof(T) == typeof(float[]))
			{
				List<float> list = new();
				for (int i = 0; i < data.Length; i += 4)
				{
					float item = BitConverter.ToSingle(data, i);
					list.Add(item);
				}
				return (T)(object)list.ToArray();
			}
			else if (typeof(T) == typeof(double[]))
			{
				List<double> list = new();
				for (int i = 0; i < data.Length; i += 8)
				{
					double item = BitConverter.ToDouble(data, i);
					list.Add(item);
				}
				return (T)(object)list.ToArray();
			}
			else if (typeof(T) == typeof(Guid[]))
			{
				List<Guid> list = new();
				for (int i = 0; i < data.Length; i += 16)
				{
					Guid item = new(data[i..][..16]);
					list.Add(item);
				}
				return (T)(object)list.ToArray();
			}
			else if (typeof(T) == typeof(DateTime[]))
			{
				List<DateTime> list = new();
				for (int i = 0; i < data.Length; i += 8)
				{
					DateTime item = DateTime.FromBinary(BitConverter.ToInt64(data, i));
					list.Add(item);
				}
				return (T)(object)list.ToArray();
			}
			else if (typeof(T) == typeof(TimeSpan[]))
			{
				List<TimeSpan> list = new();
				for (int i = 0; i < data.Length; i += 8)
				{
					TimeSpan item = TimeSpan.FromTicks(BitConverter.ToInt64(data, i));
					list.Add(item);
				}
				return (T)(object)list.ToArray();
			}
			else if (typeof(T) == typeof(Color[]))
			{
				List<Color> list = new();
				for (int i = 0; i < data.Length; i += 8)
				{
					Color item = Color.FromArgb(BitConverter.ToInt32(data, i));
					list.Add(item);
				}
				return (T)(object)list.ToArray();
			}
			throw new("Invalid type");
		}
		public static byte[] Combine(params byte[][] arrays)
		{
			byte[] rv = new byte[arrays.Sum(a => a.Length)];
			int offset = 0;
			foreach (byte[] array in arrays)
			{
				Buffer.BlockCopy(array, 0, rv, offset, array.Length);
				offset += array.Length;
			}
			return rv;
		}
		public static void CopyTo(Stream src, Stream dest)
		{
			byte[] bytes = new byte[4096];
			int cnt;
			while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
			{
				dest.Write(bytes, 0, cnt);
			}
		}
		public static byte[] Zip(byte[] data)
		{
			using (var msi = new MemoryStream(data))
			using (var mso = new MemoryStream())
			{
				using (var gs = new GZipStream(mso, CompressionMode.Compress))
				{
					CopyTo(msi, gs);
				}
				return mso.ToArray();
			}
		}
		public static byte[] Unzip(byte[] bytes)
		{
			using (var msi = new MemoryStream(bytes))
			using (var mso = new MemoryStream())
			{
				using (var gs = new GZipStream(msi, CompressionMode.Decompress))
				{
					CopyTo(gs, mso);
				}
				return mso.ToArray();
			}
		}
		public static byte[] Prefixed(string text)
		{
			return Prefixed(Encoding.UTF8.GetBytes(text));
		}
		public static byte[] Prefixed(char character)
		{
			return Prefixed(BitConverter.GetBytes(character));
		}
		public static byte[] Prefixed(bool boolean)
		{
			return Prefixed(BitConverter.GetBytes(boolean));
		}
		public static byte[] Prefixed(int number)
		{
			return Prefixed(BitConverter.GetBytes(number));
		}
		public static byte[] Prefixed(uint number)
		{
			return Prefixed(BitConverter.GetBytes(number));
		}
		public static byte[] Prefixed(short number)
		{
			return Prefixed(BitConverter.GetBytes(number));
		}
		public static byte[] Prefixed(ushort number)
		{
			return Prefixed(BitConverter.GetBytes(number));
		}
		public static byte[] Prefixed(long number)
		{
			return Prefixed(BitConverter.GetBytes(number));
		}
		public static byte[] Prefixed(ulong number)
		{
			return Prefixed(BitConverter.GetBytes(number));
		}
		public static byte[] Prefixed(Half number)
		{
			return Prefixed(BitConverter.GetBytes(number));
		}
		public static byte[] Prefixed(float number)
		{
			return Prefixed(BitConverter.GetBytes(number));
		}
		public static byte[] Prefixed(double number)
		{
			return Prefixed(BitConverter.GetBytes(number));
		}
		public static byte[] Prefixed(Guid guid)
		{
			return Prefixed(guid.ToByteArray());
		}
		public static byte[] Prefixed(DateTime dateTime)
		{
			return Prefixed(dateTime.ToBinary());
		}
		public static byte[] Prefixed(TimeSpan timeSpan)
		{
			return Prefixed(timeSpan.Ticks);
		}
		public static byte[] Prefixed(Color color)
		{
			return Prefixed(color.ToArgb());
		}
		public static byte[] Prefixed(byte[] data)
		{
			return Combine(VarintBitConverter.GetVarintBytes(data.Length), data);
		}
		public static byte[] GetData(byte[] data)
		{
			int length = VarintBitConverter.ToInt32(data);
			byte[] lengthx = VarintBitConverter.GetVarintBytes(length);
			return data[lengthx.Length..][..length];
		}
		public static string GetString(byte[] data)
		{
			byte[] bytes = GetData(data);
			return Encoding.UTF8.GetString(bytes);
		}
		public static string FormUrlEncode(Dictionary<string, string> keyValuePairs)
		{
			string text = string.Empty;
			foreach (KeyValuePair<string, string> keyValuePair in keyValuePairs)
			{
				text += keyValuePair.Key + '=' + keyValuePair.Value + "&";
			}
			return text[..^1];
		}
		public static Dictionary<string, string> FormUrlDecode(string text)
		{
			Dictionary<string, string> keyValuePairs = new();
			foreach (string value in text.Split('&'))
			{
				string[] tmp = value.Split('=');
				keyValuePairs.Add(tmp[0], tmp[1]);
			}
			return keyValuePairs;
		}
	}
	public class Debug
	{
		public static void Print(object value, ConsoleColor color = ConsoleColor.Gray, bool newLine = true)
		{
			Console.ForegroundColor = color;
			Console.Write(value + (newLine ? "\n" : string.Empty));
			Console.ResetColor();
		}
		public static void Log(object value)
		{
			Print(value, ConsoleColor.Gray);
		}
		public static void Warn(object value)
		{
			Print(value, ConsoleColor.Yellow);
		}
		public static void Error(object value)
		{
			Print(value, ConsoleColor.Red);
		}
		public static void OK(object value)
		{
			Print(value, ConsoleColor.Green);
		}
		public static void Info(object value)
		{
			Print(value, ConsoleColor.Blue);
		}
		public static void Comment(object value)
		{
			Print(value, ConsoleColor.DarkGray);
		}
		public static void InsertDate()
		{
			Debug.Print("[" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "] ", ConsoleColor.Gray, false);
		}
	}
}