﻿using System.IO.Compression;
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
		public static byte[] SerializeByteArray(byte[][] array)
		{
			List<byte[]> list = new();
			foreach (byte[] value in array)
			{
				list.Add(Combine(VarintBitConverter.GetVarintBytes(value.Length), value));
			}
			return Combine(list.ToArray());
		}
		public static byte[][] DeserializeByteArray(byte[] array)
		{
			List<byte[]> list = new();
			List<byte> tmp = array.ToList();
			while (tmp.Count > 0)
			{
				int length = VarintBitConverter.ToInt32(tmp.ToArray());
				tmp.RemoveRange(0, VarintBitConverter.GetVarintBytes(length).Length);
				list.Add(tmp.ToArray()[..length]);
				tmp.RemoveRange(0, length);
			}
			return list.ToArray();
		}
		public static byte[] SerializeUshortArray(ushort[] array)
		{
			List<byte[]> list = new();
			foreach (ushort value in array)
			{
				list.Add(BitConverter.GetBytes(value));
			}
			return Combine(list.ToArray());
		}
		public static ushort[] DeserializeUshortArray(byte[] array)
		{
			List<ushort> list = new();
			for (int i = 0; i < array.Length / 2; i++)
			{
				list.Add(BitConverter.ToUInt16(array, i * 2));
			}
			return list.ToArray();
		}
		public static byte[] SerializeStringArray(string[] array)
		{
			List<byte> list = new();
			foreach (string str in array)
			{
				list.AddRange(PrefixedString(str));
			}
			return Combine(list.ToArray());
		}
		public static string[] DeserializeStringArray(byte[] array)
		{
			List<byte> data = array.ToList();
			List<string> words = new();
			while (data.Count > 0)
			{
				string nzx = GetString(data.ToArray());
				words.Add(nzx);
				data.RemoveRange(0, PrefixedString(nzx).Length);
			}
			return words.ToArray();
		}
		public static string GetString(byte[] data)
		{
			int length = VarintBitConverter.ToInt32(data);
			byte[] lengthx = VarintBitConverter.GetVarintBytes(length);
			return Encoding.UTF8.GetString(data, lengthx.Length, length);
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
		public static byte[] PrefixedString(string text)
		{
			byte[] data = Encoding.UTF8.GetBytes(text);
			return Combine(VarintBitConverter.GetVarintBytes(data.Length), data);
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