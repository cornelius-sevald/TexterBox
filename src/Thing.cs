public class Thing : TexterObject
{
    public Token noun;
    public Token[] prepositions;
    public Token[] adjectives;

    public Thing(string id, Token noun, Token[] prepositions, Token[] adjectives)
    {
        this.Id = id;
        this.noun = noun;
        this.prepositions = prepositions;
        this.adjectives = adjectives;
    }

    public Thing(string id, Token noun)
    {
        this.Id = id;
        this.noun = noun;
        this.prepositions = new Token[] {};
        this.adjectives = new Token[] {};
    }

    public Thing(Token noun)
    {
        this.Id = noun.Id;
        this.noun = noun;
        this.prepositions = new Token[] {};
        this.adjectives = new Token[] {};
    }
}