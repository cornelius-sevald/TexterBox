using System.IO;
using System.Linq;
using System;

namespace TexterBox
{
    class Program
    {
        // The file path to the example tokens and text.
        const string tokensFilePath = "./examples/tokens-001.tkn";
        const string textFilePath = "./examples/text-001.txt";

        static void Main(string[] args)
        {
            // Read the example tokens from the file.
            Token[] potentialTokens = TokenUtils.FromFile(tokensFilePath);
            // Construct an empty sentence to fill later.
            Sentence sentence = Sentence.EmptySentence();

            // Keep reading input until EOF.
            string input;
            while ((input = Input.GetInput()) != null)
            {
                // Tokenize the read text.
                Token[] tokenized = Lexer.LexInput(potentialTokens, input);
                // Attempt to parse the tokens.
                if (!Parser.ParseTokens(tokenized, ref sentence))
                {
                    // If the parsing did not work, report it to the user.
                    Console.WriteLine("huh?");
                    continue;
                }
                // Check if the user wants to stop the program.
                if ((sentence.verb.Id == "luk" ||
                     sentence.verb.Id == "stop") &&
                     sentence.noun.Id == "program")
                {
                    // Do not stop the program if the user is rude.
                    if (sentence.adjectives.Any(a => a.Id == "dum"))
                    {
                        Console.WriteLine("godt at dette program ikke er dumt B-)");
                    }
                    // Otherwise, do stop the program.
                    else
                    {
                        break;
                    }
                }

                // Print the parsed sentence back to the user.
                Console.Write("\"" + sentence.verb.Id + " ");
                foreach (Token prep in sentence.prepositions) {
                    Console.Write(prep.Id + " ");
                }
                foreach (Token adj in sentence.adjectives) {
                    Console.Write(adj.Id + " ");
                }
                Console.WriteLine(sentence.noun.Id + "\" - meget filosofisk...");
            }
            // Print a boodbye message on exit.
            Console.WriteLine("bye.");
        }
    }
}
