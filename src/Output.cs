using System;
using System.Text;
using System.Threading;

/// <summary>
/// Helper class for handling output to the console.
/// </summary>
public static class Output
{

    /// <summary>
    /// The delay between each character is printed to the console.
    /// Measured in milliseconds.
    /// </summary>
    public static int printDelay = 35;

    /// <summary>
    /// Write some text to the console, with some delay.
    /// </summary>
    /// <param name="message">The message to write.</param>
    public static void WriteMessage(string message)
    {
        const int ENTER = 13;
        var _printDelay = printDelay;
        var cursX = Console.CursorLeft;
        var cursY = Console.CursorTop;

        foreach (char c in message) {

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.KeyChar == ENTER)
                {
                    Console.CursorTop = cursY;
                    Console.CursorLeft = cursX;
                }
                else
                {
                    Console.CursorLeft = cursX;
                }
                _printDelay = 0;
            }

            Console.Write(c);

            cursX = Console.CursorLeft;
            cursY = Console.CursorTop;

            Thread.Sleep(_printDelay);
        }
    }

    /// <summary>
    /// Write a line of text to the console, with some delay.
    /// </summary>
    /// <param name="message">The message to write.</param>
    public static void WriteMessageLn(string message)
    {
        WriteMessage(message);
        Console.Write("\n");
    }
}