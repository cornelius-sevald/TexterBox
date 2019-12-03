using System.IO;
using System.Linq;
using System;

namespace TexterBox
{
    class Program
    {
        const string tokensFilePath = "./examples/tokens-001.tkn";
        const string textFilePath = "./examples/text-001.txt";
        static void Main(string[] args)
        {
            Token[] potentialTokens = TokenUtils.FromFile(tokensFilePath);
            Sentence sentence = Sentence.EmptySentence();

            string input;
            while ((input = Input.GetInput()) != null)
            {
                Token[] tokenized = Lexer.LexInput(potentialTokens, input);
                if (!Parser.ParseTokens(tokenized, ref sentence))
                {
                    Console.WriteLine("huh?");
                    continue;
                }
                if ((sentence.verb.id == "luk" ||
                     sentence.verb.id == "stop") &&
                     sentence.noun.id == "program")
                {
                    if (sentence.adjectives.Any(a => a.id == "dum"))
                    {
                        Console.WriteLine("godt at dette program ikke er dumt B-)");
                    }
                    else
                    {
                        break;
                    }
                }

                Console.Write("\"" + sentence.verb.id + " ");
                foreach (Token prep in sentence.prepositions) {
                    Console.Write(prep.id + " ");
                }
                foreach (Token adj in sentence.adjectives) {
                    Console.Write(adj.id + " ");
                }
                Console.WriteLine(sentence.noun.id + "\" - meget filosofisk...");
            }
            Console.WriteLine("bye.");
        }
    }
}
