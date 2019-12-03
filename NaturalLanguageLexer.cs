using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public enum TokenType
{
    NoneToken, VerbToken, PrepositionToken, AdjectiveToken, NounToken
}

public class Token
{
    // The token type.
    public TokenType type;
    // The toke identifier i.e. what this token represents.
    public string id;
    // The regex string.
    string rxStr;
    // The regex this token matches.
    Regex rx;

    public Token(TokenType type, string id) : this(type, id, id) { }

    public Token(TokenType type, string id, string rxString)
    {
        this.type = type;
        this.id = id;
        this.rxStr = rxString;
        this.rx = new Regex(rxString, RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }

    public Token Copy()
    {
        return new Token(type, id, rxStr);
    }

    public bool MatchWord(string word)
    {
        return rx.IsMatch(word);
    }
}

public static class Lexer
{
    public static Token[] LexInput(Token[] potentialTokens, string input)
    {
        // Remove non-alphanumerical characters.
        Regex rgx = new Regex("[^a-zæøåA-ZÆØÅ0-9 -]");
        input = rgx.Replace(input, "");

        // Split input into words.
        string[] words = input.Split(' ');

        List<Token> tokens = new List<Token>();
        foreach (string word in words)
        {
            foreach (Token token in potentialTokens)
            {
                if (token.MatchWord(word))
                {
                    // If the token matches, add it and stop looking for new tokens.
                    tokens.Add(token.Copy());
                    break;
                }
            }
        }
        return tokens.ToArray();
    }
}

public static class TokenUtils
{
    public static Token[] FromFile(string filePath)
    {
        List<Token> tokens = new List<Token>();
        using (StreamReader sr = File.OpenText(filePath))
        {
            string s;
            TokenType currentType = TokenType.NoneToken;
            while ((s = sr.ReadLine()) != null)
            {
                switch (s)
                {
                    case "Verbs":
                        currentType = TokenType.VerbToken;
                        break;
                    case "Nouns":
                        currentType = TokenType.NounToken;
                        break;
                    case "Adjectives":
                        currentType = TokenType.AdjectiveToken;
                        break;
                    case "Prepositions":
                        currentType = TokenType.PrepositionToken;
                        break;
                    default:
                        if (currentType == TokenType.NoneToken)
                        {
                            // If no active token is specified, throw an exception.
                            throw new TokenReadException("No active token type.");
                        }
                        if (!(s.StartsWith('\t') || s.StartsWith("    ")))
                        {
                            // If the string is not indendet, throw an exception.
                            throw new TokenReadException("Invalid token syntax.");
                        }
                        // Split the string at the first " : ".
                        string[] _delimiters = { ": " };
                        string[] _parts = s.Split(_delimiters, 2, System.StringSplitOptions.None);
                        if (_parts.Length != 2)
                        {
                            // If the string did not split in two, throw an exception.
                            throw new TokenReadException("Invalid token syntax.");
                        }
                        string id = _parts[0].Trim();
                        string rxStr = _parts[1];

                        Token token = new Token(currentType, id, rxStr);
                        tokens.Add(token);
                        break;
                }
            }
        }
        return tokens.ToArray();
    }
}

/* Exception used when creating tokens from a file. */
[System.Serializable]
public class TokenReadException : System.Exception
{
    public TokenReadException() { }
    public TokenReadException(string message) : base(message) { }
    public TokenReadException(string message, System.Exception inner) : base(message, inner) { }
    protected TokenReadException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}