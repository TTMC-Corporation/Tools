using System.Drawing;
using System.IO.Compression;
using System.Text;

namespace TTMC.Tools
{
	public class Engine
	{
		public static string See(byte[] bytes)
		{
			string resp = string.Empty;
			foreach (byte b in bytes)
			{
				resp += b + " ";
			}
			return resp[..^1];
		}
		public static void InsertDate()
		{
			Debug.Print("[" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "] ", ConsoleColor.Gray, false);
		}
		public static string CreateRandomPassword(int length = 100)
		{
			string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			char[] chars = new char[length];
			for (int i = 0; i < length; i++)
			{
				chars[i] = validChars[new Random().Next(0, validChars.Length)];
			}
			return new string(chars);
		}
		public static byte[] Serialize(IEnumerable<string> array)
		{
			List<byte> list = new();
			foreach (string item in array)
			{
				list.AddRange(Prefixed(Encoding.UTF8.GetBytes(item)));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<char> array)
		{
			List<byte> list = new();
			foreach (char item in array)
			{
				list.AddRange(Prefixed(BitConverter.GetBytes(item)));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<bool> array)
		{
			List<byte> list = new();
			foreach (bool item in array)
			{
				list.AddRange(Prefixed(BitConverter.GetBytes(item)));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<int> array)
		{
			List<byte> list = new();
			foreach (int item in array)
			{
				list.AddRange(Prefixed(BitConverter.GetBytes(item)));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<uint> array)
		{
			List<byte> list = new();
			foreach (uint item in array)
			{
				list.AddRange(Prefixed(BitConverter.GetBytes(item)));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<short> array)
		{
			List<byte> list = new();
			foreach (short item in array)
			{
				list.AddRange(Prefixed(BitConverter.GetBytes(item)));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<ushort> array)
		{
			List<byte> list = new();
			foreach (ushort item in array)
			{
				list.AddRange(Prefixed(BitConverter.GetBytes(item)));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<long> array)
		{
			List<byte> list = new();
			foreach (long item in array)
			{
				list.AddRange(Prefixed(BitConverter.GetBytes(item)));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<ulong> array)
		{
			List<byte> list = new();
			foreach (ulong item in array)
			{
				list.AddRange(Prefixed(BitConverter.GetBytes(item)));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<float> array)
		{
			List<byte> list = new();
			foreach (float item in array)
			{
				list.AddRange(Prefixed(BitConverter.GetBytes(item)));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<double> array)
		{
			List<byte> list = new();
			foreach (double item in array)
			{
				list.AddRange(Prefixed(BitConverter.GetBytes(item)));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<Guid> array)
		{
			List<byte> list = new();
			foreach (Guid item in array)
			{
				list.AddRange(Prefixed(item.ToByteArray()));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<DateTime> array)
		{
			List<byte> list = new();
			foreach (DateTime item in array)
			{
				list.AddRange(Prefixed(item.ToBinary()));
			}
			return Combine(list.ToArray());
		}
		public static byte[] Serialize(IEnumerable<Color> array)
		{
			List<byte> list = new();
			foreach (Color item in array)
			{
				list.AddRange(Prefixed(item.ToArgb()));
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
				byte[][]? bytes = Deserialize<byte[][]>(data);
				if (bytes != null)
				{
					List<char> list = new();
					foreach (byte[] array in bytes)
					{
						char item = BitConverter.ToChar(array);
						list.Add(item);
					}
					return (T)(object)list.ToArray();
				}
			}
			else if (typeof(T) == typeof(bool[]))
			{
				byte[][]? bytes = Deserialize<byte[][]>(data);
				if (bytes != null)
				{
					List<bool> list = new();
					foreach (byte[] array in bytes)
					{
						bool item = BitConverter.ToBoolean(array);
						list.Add(item);
					}
					return (T)(object)list.ToArray();
				}
			}
			else if (typeof(T) == typeof(int[]))
			{
				byte[][]? bytes = Deserialize<byte[][]>(data);
				if (bytes != null)
				{
					List<int> list = new();
					foreach (byte[] array in bytes)
					{
						int item = BitConverter.ToInt32(array);
						list.Add(item);
					}
					return (T)(object)list.ToArray();
				}
			}
			else if (typeof(T) == typeof(uint[]))
			{
				byte[][]? bytes = Deserialize<byte[][]>(data);
				if (bytes != null)
				{
					List<uint> list = new();
					foreach (byte[] array in bytes)
					{
						uint item = BitConverter.ToUInt32(array);
						list.Add(item);
					}
					return (T)(object)list.ToArray();
				}
			}
			else if (typeof(T) == typeof(short[]))
			{
				byte[][]? bytes = Deserialize<byte[][]>(data);
				if (bytes != null)
				{
					List<short> list = new();
					foreach (byte[] array in bytes)
					{
						short item = BitConverter.ToInt16(array);
						list.Add(item);
					}
					return (T)(object)list.ToArray();
				}
			}
			else if (typeof(T) == typeof(ushort[]))
			{
				byte[][]? bytes = Deserialize<byte[][]>(data);
				if (bytes != null)
				{
					List<ushort> list = new();
					foreach (byte[] array in bytes)
					{
						ushort item = BitConverter.ToUInt16(array);
						list.Add(item);
					}
					return (T)(object)list.ToArray();
				}
			}
			else if (typeof(T) == typeof(long[]))
			{
				byte[][]? bytes = Deserialize<byte[][]>(data);
				if (bytes != null)
				{
					List<long> list = new();
					foreach (byte[] array in bytes)
					{
						long item = BitConverter.ToInt64(array);
						list.Add(item);
					}
					return (T)(object)list.ToArray();
				}
			}
			else if (typeof(T) == typeof(ulong[]))
			{
				byte[][]? bytes = Deserialize<byte[][]>(data);
				if (bytes != null)
				{
					List<ulong> list = new();
					foreach (byte[] array in bytes)
					{
						ulong item = BitConverter.ToUInt64(array);
						list.Add(item);
					}
					return (T)(object)list.ToArray();
				}
			}
			else if (typeof(T) == typeof(float[]))
			{
				byte[][]? bytes = Deserialize<byte[][]>(data);
				if (bytes != null)
				{
					List<float> list = new();
					foreach (byte[] array in bytes)
					{
						float item = BitConverter.ToSingle(array);
						list.Add(item);
					}
					return (T)(object)list.ToArray();
				}
			}
			else if (typeof(T) == typeof(double[]))
			{
				byte[][]? bytes = Deserialize<byte[][]>(data);
				if (bytes != null)
				{
					List<double> list = new();
					foreach (byte[] array in bytes)
					{
						double item = BitConverter.ToDouble(array);
						list.Add(item);
					}
					return (T)(object)list.ToArray();
				}
			}
			else if (typeof(T) == typeof(Guid[]))
			{
				byte[][]? bytes = Deserialize<byte[][]>(data);
				if (bytes != null)
				{
					List<Guid> list = new();
					foreach (byte[] array in bytes)
					{
						Guid item = new(array);
						list.Add(item);
					}
					return (T)(object)list.ToArray();
				}
			}
			else if (typeof(T) == typeof(DateTime[]))
			{
				byte[][]? bytes = Deserialize<byte[][]>(data);
				if (bytes != null)
				{
					List<DateTime> list = new();
					foreach (byte[] array in bytes)
					{
						DateTime item = DateTime.FromBinary(BitConverter.ToInt64(array));
						list.Add(item);
					}
					return (T)(object)list.ToArray();
				}
			}
			else if (typeof(T) == typeof(Color[]))
			{
				byte[][]? bytes = Deserialize<byte[][]>(data);
				if (bytes != null)
				{
					List<Color> list = new();
					foreach (byte[] array in bytes)
					{
						Color item = Color.FromArgb(BitConverter.ToInt32(array));
						list.Add(item);
					}
					return (T)(object)list.ToArray();
				}
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
			if (newLine)
			{
				Console.WriteLine(value);
			}
			else
			{
				Console.Write(value);
			}
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
	}
}