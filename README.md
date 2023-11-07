# Tools
These tools are used in almost every TTMC product mostly for Serializing and Deserializing almost any type of variables into it's byte array form. It also have VarInts and Debug functions inside and some other fun stuff like random password generator and insert date function.

## Serializing and Deserializing
To serialize a string array:
```cs
string[] strings = { "Hello", "there!", "How", "are", "you?" };
byte[] bytes = Engine.Serialize(strings);
```
To deserialize a byte array:
```cs
string[] items = Engine.Deserialize<string[]>(bytes);
```
Serializing and deserializing works with almost every type of variables.
# Debug
Only white messages are boring. Let's color the terminal!
```
// Pre color funtions
Debug.Error("Red message");
Debug.Warn("Yellow message");
Debug.OK("Green message");
Debug.Comment("Gray message");
Debug.Info("Blue message");

// Custom color function
Debug.Print("Magenta message", ConsoleColor.Magenta);
```
You can insert current time before you write something to the terminal.
```
Debug.InsertDate();
Debug.Info("Server started!");
```