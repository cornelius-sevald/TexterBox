class Interaction : TexterObject
{
    public Thing thing;
    public Token verb;

    public Interaction(string id, Thing thing, Token verb) {
        this.Id = id;
        this.thing = thing;
        this.verb = verb;
    }

    public InteractionMatch Match (Sentence sentence)
    {
        if (thing.noun.Id != sentence.noun.Id)
        {
            return new InteractionFaliure(sentence.noun.Copy());
        }
        foreach (Token prep1 in thing.prepositions)
        {
            foreach (Token prep2 in sentence.prepositions)
            {
                if (prep1.Id != prep2.Id)
                {
                    return new InteractionFaliure(prep2.Copy());
                }
            }
        }
        foreach (Token adj1 in thing.adjectives)
        {
            foreach (Token adj2 in sentence.adjectives)
            {
                if (adj1.Id != adj2.Id)
                {
                    return new InteractionFaliure(adj2.Copy());
                }
            }
        }
        if (verb.Id != sentence.verb.Id) {
            return new InteractionFaliure(sentence.verb.Copy());
        }
        return new InteractionSucess();
    }
}

abstract class InteractionMatch {}

class InteractionSucess : InteractionMatch {}

class InteractionFaliure : InteractionMatch
{
    public Token mismatch;

    public InteractionFaliure (Token mismatch)
    {
        this.mismatch = mismatch;
    }
}