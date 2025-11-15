using System.Drawing;
using System.IO.Compression;
using System.Numerics;
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
		public static string CreateRandomPassword(int length = 100, string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")
		{
			char[] array = new char[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = validChars[random.Next(validChars.Length)];
			}
			return new(array);
		}
		public static byte[] Serialize(IEnumerable<byte> array)
		{
			return Combine(array.ToArray());
		}
		public static byte[] Serialize(IEnumerable<byte[]> array)
		{
			byte[][] bytes = new byte[array.Count() + 1][];
			bytes[0] = VarintBitConverter.GetVarintBytes(array.Count());
			for (int i = 0; i < array.Count(); i++)
			{
				bytes[i + 1] = Prefixed(array.ElementAt(i));
			}
			return Combine(bytes);
		}
		public static byte[] Serialize(IEnumerable<string> array)
		{
			byte[][] bytes = new byte[array.Count()][];
			for (int i = 0; i < array.Count(); i++)
			{
				bytes[i] = Encoding.UTF8.GetBytes(array.ElementAt(i));
			}
			return Serialize(bytes);
		}
		public static byte[] Serialize(IEnumerable<char> array)
		{
			byte[][] bytes = new byte[array.Count()][];
			for (int i = 0; i < array.Count(); i++)
			{
				bytes[i] = BitConverter.GetBytes(array.ElementAt(i));
			}
			return Combine(bytes);
		}
		public static byte[] Serialize(IEnumerable<bool> array)
		{
			byte[][] bytes = new byte[array.Count()][];
			for (int i = 0; i < array.Count(); i++)
			{
				bytes[i] = BitConverter.GetBytes(array.ElementAt(i));
			}
			return Combine(bytes);
		}
		public static byte[] Serialize(IEnumerable<int> array)
		{
			byte[][] bytes = new byte[array.Count()][];
			for (int i = 0; i < array.Count(); i++)
			{
				bytes[i] = BitConverter.GetBytes(array.ElementAt(i));
			}
			return Combine(bytes);
		}
		public static byte[] Serialize(IEnumerable<uint> array)
		{
			byte[][] bytes = new byte[array.Count()][];
			for (int i = 0; i < array.Count(); i++)
			{
				bytes[i] = BitConverter.GetBytes(array.ElementAt(i));
			}
			return Combine(bytes);
		}
		public static byte[] Serialize(IEnumerable<short> array)
		{
			byte[][] bytes = new byte[array.Count()][];
			for (int i = 0; i < array.Count(); i++)
			{
				bytes[i] = BitConverter.GetBytes(array.ElementAt(i));
			}
			return Combine(bytes);
		}
		public static byte[] Serialize(IEnumerable<ushort> array)
		{
			byte[][] bytes = new byte[array.Count()][];
			for (int i = 0; i < array.Count(); i++)
			{
				bytes[i] = BitConverter.GetBytes(array.ElementAt(i));
			}
			return Combine(bytes);
		}
		public static byte[] Serialize(IEnumerable<long> array)
		{
			byte[][] bytes = new byte[array.Count()][];
			for (int i = 0; i < array.Count(); i++)
			{
				bytes[i] = BitConverter.GetBytes(array.ElementAt(i));
			}
			return Combine(bytes);
		}
		public static byte[] Serialize(IEnumerable<ulong> array)
		{
			byte[][] bytes = new byte[array.Count()][];
			for (int i = 0; i < array.Count(); i++)
			{
				bytes[i] = BitConverter.GetBytes(array.ElementAt(i));
			}
			return Combine(bytes);
		}
		public static byte[] Serialize(IEnumerable<Half> array)
		{
			byte[][] bytes = new byte[array.Count()][];
			for (int i = 0; i < array.Count(); i++)
			{
				bytes[i] = BitConverter.GetBytes(array.ElementAt(i));
			}
			return Combine(bytes);
		}
		public static byte[] Serialize(IEnumerable<float> array)
		{
			byte[][] bytes = new byte[array.Count()][];
			for (int i = 0; i < array.Count(); i++)
			{
				bytes[i] = BitConverter.GetBytes(array.ElementAt(i));
			}
			return Combine(bytes);
		}
		public static byte[] Serialize(IEnumerable<double> array)
		{
			byte[][] bytes = new byte[array.Count()][];
			for (int i = 0; i < array.Count(); i++)
			{
				bytes[i] = BitConverter.GetBytes(array.ElementAt(i));
			}
			return Combine(bytes);
		}
		public static byte[] Serialize(IEnumerable<decimal> array)
		{
			long[] longs = new long[array.Count()];
			for (int i = 0; i < array.Count(); i++)
			{
				longs[i] = decimal.ToOACurrency(array.ElementAt(i));
			}
			return Serialize(longs);
		}
		public static byte[] Serialize(IEnumerable<Guid> array)
		{
			byte[][] bytes = new byte[array.Count()][];
			for (int i = 0; i < array.Count(); i++)
			{
				bytes[i] = array.ElementAt(i).ToByteArray();
			}
			return Combine(bytes);
		}
		public static byte[] Serialize(IEnumerable<DateTime> array)
		{
			long[] longs = new long[array.Count()];
			for (int i = 0; i < array.Count(); i++)
			{
				longs[i] = array.ElementAt(i).ToBinary();
			}
			return Serialize(longs);
		}
		public static byte[] Serialize(IEnumerable<TimeSpan> array)
		{
			long[] longs = new long[array.Count()];
			for (int i = 0; i < array.Count(); i++)
			{
				longs[i] = array.ElementAt(i).Ticks;
			}
			return Serialize(longs);
		}
		public static byte[] Serialize(IEnumerable<DateTimeOffset> array)
		{
			long[] longs = new long[array.Count()];
			for (int i = 0; i < array.Count(); i++)
			{
				longs[i] = array.ElementAt(i).ToFileTime();
			}
			return Serialize(longs);
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
		public static byte[] Serialize(Vector2 vector)
		{
			float[] array = new float[2];
			vector.CopyTo(array);
			return Serialize(array);
		}
		public static byte[] Serialize(Vector3 vector)
		{
			float[] array = new float[3];
			vector.CopyTo(array);
			return Serialize(array);
		}
		public static byte[] Serialize(Vector4 vector)
		{
			float[] array = new float[4];
			vector.CopyTo(array);
			return Serialize(array);
		}
		public static byte[] Serialize(IEnumerable<Vector2> array)
		{
			float[] floats = new float[array.Count() * 2];
			for (int i = 0; i < array.Count(); i++)
			{
				Vector2 vector = array.ElementAt(i);
				floats[i * 2] = vector.X;
				floats[i * 2 + 1] = vector.Y;
			}
			return Serialize(floats);
		}
		public static byte[] Serialize(IEnumerable<Vector3> array)
		{
			float[] floats = new float[array.Count() * 3];
			for (int i = 0; i < array.Count(); i++)
			{
				Vector3 vector = array.ElementAt(i);
				floats[i * 3] = vector.X;
				floats[i * 3 + 1] = vector.Y;
				floats[i * 3 + 2] = vector.Z;
			}
			return Serialize(floats);
		}
		public static byte[] Serialize(IEnumerable<Vector4> array)
		{
			float[] floats = new float[array.Count() * 4];
			for (int i = 0; i < array.Count(); i++)
			{
				Vector4 vector = array.ElementAt(i);
				floats[i * 4] = vector.X;
				floats[i * 4 + 1] = vector.Y;
				floats[i * 4 + 2] = vector.Z;
				floats[i * 4 + 3] = vector.W;
			}
			return Serialize(floats);
		}
		public static T Deserialize<T>(byte[] data)
		{
			if (typeof(T) == typeof(byte[][]))
			{
				int counter = 0;
				int arrayLength = VarintBitConverter.ToInt32(data);
				byte[][] array = new byte[arrayLength][];
				int i = VarintBitConverter.GetVarintBytes(arrayLength).Length;
				for (; i < data.Length;)
				{
					int j = VarintBitConverter.ToInt32(data[i..]);
					int k = VarintBitConverter.GetVarintBytes(j).Length;
					array[counter++] = data[(i + k)..][..j];
					i += k + j;
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(string[]))
			{
				byte[][] bytes = Deserialize<byte[][]>(data);
				string[] array = new string[bytes.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Encoding.UTF8.GetString(bytes[i]);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(char[]))
			{
				char[] array = new char[data.Length / 2];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = BitConverter.ToChar(data, i * 2);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(bool[]))
			{
				bool[] array = new bool[data.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = BitConverter.ToBoolean(data, i);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(int[]))
			{
				int[] array = new int[data.Length / 4];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = BitConverter.ToInt32(data, i * 4);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(uint[]))
			{
				uint[] array = new uint[data.Length / 4];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = BitConverter.ToUInt32(data, i * 4);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(short[]))
			{
				short[] array = new short[data.Length / 2];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = BitConverter.ToInt16(data, i * 2);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(ushort[]))
			{
				ushort[] array = new ushort[data.Length / 2];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = BitConverter.ToUInt16(data, i * 2);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(long[]))
			{
				long[] array = new long[data.Length / 8];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = BitConverter.ToInt64(data, i * 8);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(ulong[]))
			{
				ulong[] array = new ulong[data.Length / 8];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = BitConverter.ToUInt64(data, i * 8);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(Half[]))
			{
				Half[] array = new Half[data.Length / 2];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = BitConverter.ToHalf(data, i * 2);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(float[]))
			{
				float[] array = new float[data.Length / 4];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = BitConverter.ToSingle(data, i * 4);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(double[]))
			{
				double[] array = new double[data.Length / 8];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = BitConverter.ToDouble(data, i * 8);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(decimal[]))
			{
				long[] longs = Deserialize<long[]>(data);
				decimal[] array = new decimal[longs.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = decimal.FromOACurrency(longs[i]);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(Guid[]))
			{
				Guid[] array = new Guid[data.Length / 16];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new(data[(i * 16)..][..16]);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(DateTime[]))
			{
				long[] longs = Deserialize<long[]>(data);
				DateTime[] array = new DateTime[longs.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = DateTime.FromBinary(longs[i]);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(TimeSpan[]))
			{
				long[] longs = Deserialize<long[]>(data);
				TimeSpan[] array = new TimeSpan[longs.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = TimeSpan.FromTicks(longs[i]);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(DateTimeOffset[]))
			{
				long[] longs = Deserialize<long[]>(data);
				DateTimeOffset[] array = new DateTimeOffset[longs.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = DateTimeOffset.FromFileTime(longs[i]);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(Color[]))
			{
				int[] ints = Deserialize<int[]>(data);
				Color[] array = new Color[ints.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Color.FromArgb(ints[i]);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(Vector2))
			{
				float[] floats = Deserialize<float[]>(data);
				Vector2 vector = new(floats[0], floats[1]);
				return (T)(object)vector;
			}
			else if (typeof(T) == typeof(Vector3))
			{
				float[] floats = Deserialize<float[]>(data);
				Vector3 vector = new(floats[0], floats[1], floats[2]);
				return (T)(object)vector;
			}
			else if (typeof(T) == typeof(Vector4))
			{
				float[] floats = Deserialize<float[]>(data);
				Vector4 vector = new(floats[0], floats[1], floats[2], floats[3]);
				return (T)(object)vector;
			}
			else if (typeof(T) == typeof(Vector2[]))
			{
				Vector2[] array = new Vector2[data.Length / 8];
				for (int i = 0; i < data.Length; i += 8)
				{
					byte[] bytes = data[i..][..8];
					array[(i / 8)] = Deserialize<Vector2>(bytes);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(Vector3[]))
			{
				Vector3[] array = new Vector3[data.Length / 12];
				for (int i = 0; i < data.Length; i += 12)
				{
					byte[] bytes = data[i..][..12];
					array[(i / 12)] = Deserialize<Vector3>(bytes);
				}
				return (T)(object)array;
			}
			else if (typeof(T) == typeof(Vector4[]))
			{
				Vector4[] array = new Vector4[data.Length / 16];
				for (int i = 0; i < data.Length; i += 16)
				{
					byte[] bytes = data[i..][..16];
					array[(i / 16)] = Deserialize<Vector4>(bytes);
				}
				return (T)(object)array;
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
		public static byte[] Zip(byte[] data)
		{
			MemoryStream memoryStreamInput = new(data);
			MemoryStream memoryStreamOutput = new();
			GZipStream gZipStream = new(memoryStreamOutput, CompressionMode.Compress);
			memoryStreamInput.CopyTo(gZipStream);
			memoryStreamInput.Close();
			gZipStream.Close();
			return memoryStreamOutput.ToArray();
		}
		public static byte[] Unzip(byte[] data)
		{
			MemoryStream memoryStreamInput = new(data);
			MemoryStream memoryStreamOutput = new();
			GZipStream gZipStream = new(memoryStreamInput, CompressionMode.Decompress);
			gZipStream.CopyTo(memoryStreamOutput);
			gZipStream.Close();
			return memoryStreamOutput.ToArray();
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
		public static byte[] Prefixed(DateTimeOffset dateTimeOffset)
		{
			return Prefixed(dateTimeOffset.ToUnixTimeMilliseconds());
		}
		public static byte[] Prefixed(Color color)
		{
			return Prefixed(color.ToArgb());
		}
		public static byte[] Prefixed(Vector2 vector)
		{
			return Prefixed(Combine(BitConverter.GetBytes(vector.X), BitConverter.GetBytes(vector.Y)));
		}
		public static byte[] Prefixed(Vector3 vector)
		{
			return Prefixed(Combine(BitConverter.GetBytes(vector.X), BitConverter.GetBytes(vector.Y), BitConverter.GetBytes(vector.Z)));
		}
		public static byte[] Prefixed(Vector4 vector)
		{
			return Prefixed(Combine(BitConverter.GetBytes(vector.X), BitConverter.GetBytes(vector.Y), BitConverter.GetBytes(vector.Z), BitConverter.GetBytes(vector.W)));
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
				if (value.Contains('='))
				{
					string[] tmp = value.Split('=');
					keyValuePairs.Add(tmp[0], tmp[1]);
				}
			}
			return keyValuePairs;
		}
		public static string Crimp(string text)
		{
			string resp = string.Empty;
			bool god = false;
			foreach (char c in text)
			{
				if (god || (c != ' ' && c != '\t'))
				{
					resp += c;
					if (c == '\"')
					{
						god = !god;
					}
				}
			}
			return resp;
		}
	}
}