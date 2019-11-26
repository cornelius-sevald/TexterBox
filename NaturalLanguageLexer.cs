using System.Linq;
using System.Collections.Generic;

public interface IToken {
    TokenMatch Tokenize (string word);
}

struct MatchPair {
    public TokenMatch match;
    public List<string> tokenWords;
}

static class Lexer {
    public List<TokenMatch> LexInput (List<IToken> potentialTokens, Queue<string> words) {
        if (words.Count == 0) {
            return null;
        }
        if (words.Count == 1) {
            string word = words.Dequeue();

            TokenMatch match = null;
            foreach (IToken token in potentialTokens) {
                match = match.Join(token.Tokenize(word));
            }
            MatchPair pair = {match, words};

            return LexTokens(potentialTokens, words, pair);
        }
        else {
            string word1 = words.Dequeue();
            string word2 = words.Dequeue();
        }
    }

    public List<TokenMatch> LexTokens (List<IToken> potentialTokens, Queue<string> words, MatchPair tokens) {
        return null;
    }

    public List<TokenMatch> LexTokens (List<IToken> potentialTokens, Queue<string> words, MatchPair tokens1, MatchPair tokens2) {
        return null;
    }
}

class TokenMatch {
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
    TokenMatch Join (TokenMatch m1, TokenMatch m2) {
        if (m1 == null) {
            return m2;
        }
        if (m2 == null) {
            return m1;
        }
        if (m1.word == m2.word) {
            string word = m1.word;
            IToken[] tokens = m1.tokens.Concat(m2.tokens);
            return new TokenMatch(word, tokens);
        }
        return null;
    }
}