/// <summary>
/// Class representing a thing in the game.
/// </summary>
public class Thing : TexterObject
{
    /// <summary>
    /// The noun that describes this thing.
    /// </summary>
    public Token noun;

    /// <summary>
    /// The prepositions that describes this thing.
    /// </summary>
    public Token[] prepositions;

    /// <summary>
    /// The adjectives that describes this thing.
    /// </summary>
    public Token[] adjectives;

    /// <summary>
    /// Construct a new thing.
    /// </summary>
    /// <param name="id">The indentifier of the object.</param>
    /// <param name="noun">The noun describing this object.</param>
    /// <param name="prepositions">The prepositions describing this object.</param>
    /// <param name="adjectives">The adjectives describing this object.</param>
    public Thing(string id, Token noun, Token[] prepositions, Token[] adjectives)
    {
        this.Id = id;
        this.noun = noun;
        this.prepositions = prepositions;
        this.adjectives = adjectives;
    }

    /// <summary>
    /// Construct a new thing with no prepositions or adjectives.
    /// </summary>
    /// <param name="id">The indentifier of the object.</param>
    /// <param name="noun">The noun describing this object.</param>
    public Thing(string id, Token noun)
    {
        this.Id = id;
        this.noun = noun;
        this.prepositions = new Token[] {};
        this.adjectives = new Token[] {};
    }

    /// <summary>
    /// Construct a thing with the same noun and identifier.
    /// </summary>
    /// <param name="noun">The noun and identifier string of this thing.</param>
    public Thing(Token noun)
    {
        this.Id = noun.Id;
        this.noun = noun;
        this.prepositions = new Token[] {};
        this.adjectives = new Token[] {};
    }
}