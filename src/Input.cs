using System;

/// <summary>
/// Simple class for getting the input from the user.
/// </summary>
public static class Input {

    /// <summary>
    /// The user prompt string.
    /// </summary>
    public static string prompt = "> ";

    /// <summary>
    /// Get one line of input from the user.
    /// </summary>
    /// <returns>A line of input from the console, <c>null</c> when EOF is reached.</returns>
    public static string GetInput () {
        Console.Write(prompt);
        string input = Console.ReadLine();
        Console.Clear();
        return input;
    }
}