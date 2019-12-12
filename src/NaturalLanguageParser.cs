using System.Collections.Generic;

namespace TexterBox
{
    /// <summary>
    /// A representation of a simple sentence.
    /// <para>
    /// A sentence has the following structure
    /// <code>
    /// sentence = verb [preposition] object
    /// object   = [adjective] noun
    /// </code>
    /// </para>
    /// </summary>
    public struct Sentence
    {
        /// <summary>
        /// The verb of the sentence.
        /// </summary>
        public Token verb;

        /// <summary>
        /// The prepositions of the sentence.
        /// </summary>
        public Token[] prepositions;

        /// <summary>
        /// The adjectives of the sentence.
        /// </summary>
        public Token[] adjectives;

        /// <summary>
        /// The noun of the sentence.
        /// </summary>
        public Token noun;

        /// <summary>
        /// Construct a sentence.
        /// </summary>
        /// <param name="verb">The verb of the sentence.</param>
        /// <param name="prepositions">The prepositions of the sentence.</param>
        /// <param name="adjectives">The adjectives of the sentence.</param>
        /// <param name="noun">The noun of the sentence.</param>
        public Sentence(Token verb, Token[] prepositions, Token[] adjectives, Token noun)
        {
            this.verb = verb;
            this.prepositions = prepositions;
            this.adjectives = adjectives;
            this.noun = noun;
        }

        /// <summary>
        /// Construct an empty sentence, with all fields equal to <c>null</c>.
        /// </summary>
        /// <returns>An empty sentence.</returns>
        public static Sentence EmptySentence()
        {
            return new Sentence(null, null, null, null);
        }
    }

    /// <summary>
    /// Class for parsing a stream of tokens to a sentence.
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// Parse a stream of tokens,
        /// and fill <paramref name="sentence"/> with the approprate ones.
        /// </summary>
        /// <param name="tokens">The tokens to parse.</param>
        /// <param name="sentence">A reference to the sentence to fill.</param>
        /// <returns>
        /// <c>true</c> if the tokens were successfully parse,
        /// otherwise <c>false</c>.
        /// </returns>
        public static bool ParseTokens(Token[] tokens, ref Sentence sentence)
        {
            int i = 0;

            // When acessing the array of tokens, the index may be out of bounds.
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
                // If the index was out of bounds, the tokens were not parsed correctly.
                return false;
            }

            // If nothing went wrong, the tokens were parsed correctly.
            return true;
        }
    }
}