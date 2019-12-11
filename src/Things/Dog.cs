public class Dog : Thing, IEdible, IPunchable
{
    static private string id = "hund";


    /// <summary>
    /// Is the dog alive?
    /// </summary>
    public bool alive = true;

    /// <summary>
    /// Has the dig been eaten?
    /// </summary>
    public bool eaten = false;

    /// <summary>
    /// Construct a dog object with an identifying noun,
    /// prepositions and adjectives.
    /// </summary>
    public Dog(string noun, string[] prepositions, string[] adjectives)
     : base(id, noun, prepositions, adjectives) { }

    /// <summary>
    /// Construct a dog object with an identifying noun.
    /// </summary>
    public Dog(string noun)
     : base(id, noun) { }


    public void Punch()
    {
        if (alive)
        {
            alive = false;
            Output.WriteMessageLn("Du slår hunden, og den føler det ikke så godt.");
        }
        else
        {
            Output.WriteMessageLn("Du slår hunden igen, men der sker ikke så meget.");
        }
    }

    public void Eat()
    {
        if (alive)
        {
            Output.WriteMessageLn("Du forsøger at spise hunden, men den er ikke samarbejdsvillig.");
            GameManager.Instance.Loose("Hunden gik sicko mode, og dræbte dig.");
        }
        else
        {
            Output.WriteMessageLn("Du spiser hunden.");
            GameManager.Instance.Win("Med hundens varme kød i din mave, vandt du videospillet B-)...");
        }
    }
}