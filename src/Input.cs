using System;


public static class Input {
    public static string GetInput () {
        Console.Write("> ");
        string input = Console.ReadLine();
        Console.Clear();
        return input;
    }
}