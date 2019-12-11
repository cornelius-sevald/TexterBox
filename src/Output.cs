using System;
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
    public static int printDelay = 50;

    /// <summary>
    /// Write some text to the console, with some delay.
    /// </summary>
    /// <param name="message">The message to write.</param>
    public static void WriteMessage(string message)
    {
        foreach (char c in message) {
            Console.Write(c);
            Thread.Sleep(printDelay);
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