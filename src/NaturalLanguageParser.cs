using System.Collections.Generic;
/*
sentence = verb [preposition] object
object   = [adjective] noun
*/

struct Sentence
{
    public Token verb;
    public Token[] prepositions;
    public Token[] adjectives;
    public Token noun;


    public Sentence(Token verb, Token[] prepositions, Token[] adjectives, Token noun)
    {
        this.verb = verb;
        this.prepositions = prepositions;
        this.adjectives = adjectives;
        this.noun = noun;
    }

    public static Sentence EmptySentence()
    {
        return new Sentence(null, null, null, null);
    }
}

static class Parser
{
    public static bool ParseTokens(Token[] tokens, ref Sentence sentence)
    {
        int i = 0;

        try
        {
            // Verb.
            if (tokens[i].type != TokenType.VerbToken)
            {
                return false;
            }
            sentence.verb = tokens[i++];

            // Prepositions.
            List<Token> prepositions = new List<Token>();
            while (tokens[i].type == TokenType.PrepositionToken)
            {
                prepositions.Add(tokens[i++]);
            }
            sentence.prepositions = prepositions.ToArray();

            // Adjectives.
            List<Token> adjectives = new List<Token>();
            while (tokens[i].type == TokenType.AdjectiveToken)
            {
                adjectives.Add(tokens[i++]);
            }
            sentence.adjectives = adjectives.ToArray();

            // Noun.
            if (tokens[i].type != TokenType.NounToken)
            {
                return false;
            }
            sentence.noun = tokens[i++];

        }
        catch (System.IndexOutOfRangeException)
        {
            return false;
        }

        return true;
    }
}