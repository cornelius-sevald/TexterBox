using System.Linq;

public delegate void DoInteraction(Thing thing);

public class Interaction : TexterObject
{
    public Thing thing;
    public Token verb;
    public DoInteraction interactFunc;

    public Interaction(string id, Thing thing, Token verb, DoInteraction interactFunc) {
        this.Id = id;
        this.thing = thing;
        this.verb = verb;
        this.interactFunc = interactFunc;
    }

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

public abstract class InteractionMatch {}

public class InteractionSucess : InteractionMatch {}

public class InteractionFaliure : InteractionMatch
{
    public Token mismatch;

    public InteractionFaliure (Token mismatch)
    {
        this.mismatch = mismatch;
    }
}