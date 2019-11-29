using System.Collections.Generic;
using System.IO;
using System;

namespace TexterBox
{
    class Program
    {
        const string tokensFilePath = "./examples/tokens-001.tkn";
        const string textFilePath = "./examples/text-001.txt";
        static void Main(string[] args)
        {
            List<Token> tokens = TokenUtils.FromFile(tokensFilePath);
            string text = File.ReadAllText(textFilePath);
            List<Token> tokenized = Lexer.LexInput(tokens.ToArray(), text);

            Console.WriteLine("Potential tokens:");
            Console.WriteLine("==============================");
            foreach (Token token in tokens) {
                Console.WriteLine(token.id);
            }
            Console.WriteLine("==============================\n");

            Console.WriteLine("Text:");
            Console.WriteLine("==============================");
            Console.WriteLine(text);
            Console.WriteLine("==============================\n");

            Console.WriteLine("Tokenized text:");
            Console.WriteLine("==============================");
            foreach (Token token in tokenized) {
                Console.WriteLine(token.id);
            }
            Console.WriteLine("==============================");
        }
    }
}
