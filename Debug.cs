namespace TTMC.Tools
{
	public class Debug
	{
		public static void Print(object value, ConsoleColor foregroundColor = ConsoleColor.Gray, ConsoleColor backgroundColor = ConsoleColor.Black, bool newLine = true)
		{
			Console.ForegroundColor = foregroundColor;
			Console.BackgroundColor = backgroundColor;
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
			Print("[" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "] ", ConsoleColor.Gray, ConsoleColor.Black, newLine: false);
		}
	}
}