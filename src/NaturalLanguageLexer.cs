using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// The different types of tokens, representing different parts of speech.
/// The <c>NoneToken</c> represents no PoS, and is used for error checking.
/// </summary>
public enum TokenType
{
    NoneToken, NounToken, VerbToken, AdjectiveToken, PrepositionToken
}

/// <summary>
/// Tokens represent the meaning of one or more words, and are identified by their
/// <c>Id</c> property. They use regular expressions to match different words.
/// Tokens are case insensitive.
/// </summary>
public class Token : TexterObject
{
    /// <summary>
    /// The part of speech of the token.
    /// </summary>
    public TokenType type;

    /// <summary>
    /// The regex string matching different words.
    /// </summary>
    string rxStr;

    /// <summary>
    /// The compiled regex this token matches.
    /// </summary>
    Regex rx;

    /// <summary>
    /// Construct a token with the same identifier and regex string.
    /// </summary>
    /// <param name="type">The part of speech of the token.</param>
    /// <param name="id">The identifier and regex string of the token.</param>
    /// <returns></returns>
    public Token(TokenType type, string id) : this(type, id, id) { }

    /// <summary>
    /// Construct a new token from a type, an identifier and a regex string.
    /// </summary>
    /// <param name="type">The part of speech of the token.</param>
    /// <param name="id">The tokens identifier.</param>
    /// <param name="rxString">The case insensitive regex string matching words.</param>
    public Token(TokenType type, string id, string rxString)
    {
        this.type = type;
        this.Id = id;
        this.rxStr = rxString;
        this.rx = new Regex(rxString, RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// Copy a token.
    /// </summary>
    /// <returns>A new token with the same type, ID and regex string.</returns>
    public Token Copy()
    {
        return new Token(type, Id, rxStr);
    }

    /// <summary>
    /// Match a token with a word.
    /// </summary>
    /// <param name="word">The word to match the token with.</param>
    /// <returns><c>true</c> if the token matches the word, otherwise <c>fase</c>.</returns>
    public bool MatchWord(string word)
    {
        return rx.IsMatch(word);
    }
}

/// <summary>
/// A lexer / tokenizer class, that can turn a stream of words into a stream of tokens.
/// </summary>
public static class Lexer
{

    static Token actionToken = new Token(TokenType.NounToken, "", "a^");

    /// <summary>
    /// Tokenize an input string into an array of tokens.
    /// </summary>
    /// <param name="potentialTokens">The potential tokens the words of <c>input</c> can match.</param>
    /// <param name="input">The input string.</param>
    /// <returns>An array of tokens matching the string in order of appearance in <c>input</c>.</returns>
    public static Token[] LexInput(Token[] potentialTokens, string input)
    {
        // Remove non-alphanumerical characters.
        // This makes it easier to deal with punktuation.
        Regex rgx = new Regex("[^a-zæøåA-ZÆØÅ0-9 -]");
        input = rgx.Replace(input, "");

        // Split input into words.
        string[] words = input.Split(' ');

        List<Token> tokens = new List<Token>();
        // Match every word with every token.
        foreach (string word in words)
        {
            foreach (Token token in potentialTokens)
            {
                if (token.MatchWord(word))
                {
                    // If the token matches, add it and stop looking for new tokens.
                    // This means only one token can match one word.
                    tokens.Add(token.Copy());
                    break;
                }
            }
        }
        // Add the magical action token.
        tokens.Add(actionToken);

        // Return the array of tokens.
        return tokens.ToArray();
    }
}

/// <summary>
/// Helper class for dealing with tokens.
/// </summary>
public static class TokenUtils
{
    /// <summary>
    /// Read tokens from a file.
    /// The file has to have a format:
    /// <code>
    /// {token-type}
    ///     {token-id} : {regex}
    ///     {token-id} : {regex}
    ///     ...
    /// {token-type}
    ///     {token-id} : {regex}
    ///     ...
    /// </code>
    /// </summary>
    /// <param name="filePath">The path to the toke file.</param>
    /// <returns>An array of tokens.</returns>
    /// <exception cref="TokenReadException">Trown if the file is formatted incorrectly.</exception>
    public static Token[] FromFile(string filePath)
    {
        List<Token> tokens = new List<Token>();
        try
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                string s;
                TokenType currentType = TokenType.NoneToken;
                while ((s = sr.ReadLine()) != null)
                {
                    switch (s)
                    {
                        case "":
                            break;
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
        }
        catch (IOException)
        {
            throw new TokenReadException("Could not read file " + Path.GetFullPath(filePath));
        }
        return tokens.ToArray();
    }
}

/// <summary>
/// Exception class used when reading tokens from a file.
/// <seealso cref="TokenUtils.FromFile(string)"/>
/// </summary>
[System.Serializable]
public class TokenReadException : System.Exception
{
    /// <summary>
    /// Construct a new token read exception.
    /// </summary>
    public TokenReadException() { }

    /// <summary>
    /// Construct a new token read exception.
    /// </summary>
    public TokenReadException(string message) : base(message) { }

    /// <summary>
    /// Construct a new token read exception.
    /// </summary>
    public TokenReadException(string message, System.Exception inner) : base(message, inner) { }

    /// <summary>
    /// Construct a new token read exception.
    /// </summary>
    protected TokenReadException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}