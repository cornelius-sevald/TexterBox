using System.Linq;
using System.Collections.Generic;

public interface IToken {
        TokenMatch Tokenize (string word);
}

struct MatchPair {
        public TokenMatch match;
        public IEnumerable<string> tokenWords;

        public MatchPair (TokenMatch m, IEnumerable<string> t) {
                match = m;
                tokenWords = t;
        }
}

public class TokenMatch {
        public string word;
        public List<IToken> tokens;

        public TokenMatch (string word, List<IToken> tokens) {
                this.word = word;
                this.tokens = tokens;
        }

        /// <summary>
        /// Join two TokenMatch objects into one.
        /// 
        /// If one TokenMatch object is null, the other is returned.
        /// If none are null, and the two words are the same, a merged TokenMatch is returned.
        /// Otherwise, null is returned.
        /// </summary>
        /// <param name="m1">The first TokenMatch</param>
        /// <param name="m2">The second TokenMatch</param>
        /// <returns>A merged TokenMatch</returns>
        public static TokenMatch Join (TokenMatch m1, TokenMatch m2) {
                if (m1 == null) {
                        return m2;
                }
                if (m2 == null) {
                        return m1;
                }
                if (m1.word == m2.word) {
                        string word = m1.word;
                        List<IToken> tokens = m1.tokens.Concat(m2.tokens).ToList();
                        return new TokenMatch(word, tokens);
                }
                else {
                        return null;
                }
        }
}

static class Lexer {
        public static List<TokenMatch> LexInput (List<IToken> potentialTokens, Queue<string> words) {
                if (words.Count == 0) {
                        return null;
                }
                if (words.Count == 1) {
                        string word = words.Dequeue();

                        TokenMatch match = Lexer.GetTokenMatch(potentialTokens, word);
                        MatchPair tokens = new MatchPair(match, words);

                        return LexTokens(potentialTokens, words, tokens);
                }
                else {
                        string word1 = words.Dequeue();
                        // Save a copy of words without `word2` removed.
                        Queue<string> wordsCopy = new Queue<string>(words);
                        string word2 = words.Dequeue();

                        // Prioritize look-ahead.
                        TokenMatch match1 = GetTokenMatch(potentialTokens, word1 + " " + word2);
                        TokenMatch match2 = GetTokenMatch(potentialTokens, word1);

                        MatchPair tokens1 = new MatchPair(match1, words);
                        MatchPair tokens2 = new MatchPair(match2, wordsCopy);

                        return LexTokens(potentialTokens, wordsCopy, tokens1, tokens2);
                }
        }

        public static TokenMatch GetTokenMatch (List<IToken> potentialTokens, string word) {
                TokenMatch match = null;
                foreach (IToken token in potentialTokens) {
                        match = TokenMatch.Join(match, token.Tokenize(word));
                }

                return match;
        }

        public static List<TokenMatch> LexTokens (List<IToken> potentialTokens, Queue<string> words, MatchPair tokens) {
                return null;
        }

        public static List<TokenMatch> LexTokens (List<IToken> potentialTokens, Queue<string> words, MatchPair tokens1, MatchPair tokens2) {
                return null;
        }
}
