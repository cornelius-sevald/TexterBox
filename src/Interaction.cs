using System.Linq;

/// <summary>
/// Delegate to do some interaction on some thing.
/// </summary>
/// <param name="thing">The ting to interact with.</param>
public delegate void DoInteraction(Thing thing);

/// <summary>
/// Class representing an interaction between an action and a thing.
/// </summary>
public class Interaction : TexterObject
{
    /// <summary>
    /// The thing this interation will act upon.
    /// </summary>
    public Thing thing;

    /// <summary>
    /// The verb that triggers the interaction.
    /// </summary>

    public Token verb;
    /// <summary>
    /// The action that will happen if the interaction matches.
    /// </summary>
    public DoInteraction interactFunc;

    /// <summary>
    /// Construct a new interaction.
    /// </summary>
    /// <param name="id">The identifier of the interaction.</param>
    /// <param name="thing">The thing to interact with.</param>
    /// <param name="verb">The trigger of the interaction.</param>
    /// <param name="interactFunc">The action of the interaction.</param>
    public Interaction(string id, Thing thing, Token verb, DoInteraction interactFunc) {
        this.Id = id;
        this.thing = thing;
        this.verb = verb;
        this.interactFunc = interactFunc;
    }

    /// <summary>
    /// Attempt to match a sentence to this interaction.
    /// <para>
    /// If the interaction matches, <c>interactFunc</c> is called.
    /// </para>
    /// </summary>
    /// <param name="sentence">The sentence to match.</param>
    /// <returns>An <c>InteractionMatch</c> status.</returns>
    public InteractionMatch Match (Sentence sentence)
    {
        if (thing.noun.Id != sentence.noun.Id)
        {
            return new InteractionFaliure(sentence.noun.Copy());
        }
        foreach (Token prep in sentence.prepositions)
        {
            if (!thing.prepositions.Contains(prep)) {
                return new InteractionFaliure(prep.Copy());
            }
        }
        foreach (Token adj in sentence.adjectives)
        {
            if (!thing.adjectives.Contains(adj)) {
                return new InteractionFaliure(adj.Copy());
            }
        }
        if (verb.Id != sentence.verb.Id) {
            return new InteractionFaliure(sentence.verb.Copy());
        }
        interactFunc(thing);
        return new InteractionSucess();
    }
}

/// <summary>
/// Class for giving information about a interaction match.
/// <see cref="Interaction.Match(Sentence)"/>.
/// </summary>
public abstract class InteractionMatch {}

/// <summary>
/// Class representing a successful interaction match.
/// No additional information is needed.
/// </summary>
public class InteractionSucess : InteractionMatch {}

/// <summary>
/// Class representing an unsuccessful interaction match.
/// Holds a copy of the mismatching token for context.
/// </summary>
public class InteractionFaliure : InteractionMatch
{
    /// <summary>
    /// The mismatching token.
    /// </summary>
    public Token mismatch;

    /// <summary>
    /// Construct a new <c>InteractionFaliure</c>.
    /// </summary>
    /// <param name="mismatch">The mismatching token.</param>
    public InteractionFaliure (Token mismatch)
    {
        this.mismatch = mismatch;
    }
}