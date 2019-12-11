/// <summary>
/// A ketchup object that the player can interact with.
/// <para>
/// Ketchup can be thrown, eaten and opened.
/// </para>
/// </summary>
public class Ketchup : Thing, IThrowable, IEdible, IOpenable, ICloseable, ICollectable
{
    static private string id = "ketchup";

    /// <summary>
    /// Has the ketchup been thrown?
    /// </summary>
    public bool thrown = false;

    /// <summary>
    /// Has the ketchup been opened?
    /// </summary>
    public bool open = false;

    /// <summary>
    /// Has the ketchup been eaten?
    /// </summary>
    public bool eaten = false;

    /// <summary>
    /// Has the ketchup been wasted?
    /// </summary>
    public bool wasted = false;

    /// <summary>
    /// Construct a ketchup object with an identifying noun,
    /// prepositions and adjectives.
    /// </summary>
    public Ketchup(string noun, string[] prepositions, string[] adjectives)
     : base(id, noun, prepositions, adjectives) { }

    /// <summary>
    /// Construct a ketchup object with an identifying noun.
    /// </summary>
    public Ketchup(string noun)
     : base(id, noun) { }

    /// <summary>
    /// Throw the ketchup.
    /// </summary>
    public void Throw()
    {
        if (thrown)
        {
            Output.WriteMessageLn("Du holder ikke nogen ketchup");
        }
        else if (open)
        {
            Output.WriteMessageLn("Du smider ketchup-flasken og ketchup flyver ud over det hele");
            this.wasted = true;
        }
        else
        {
            this.thrown = true;
            Output.WriteMessageLn("Du smider ketchuppen.");
        }
    }

    /// <summary>
    /// Eat the ketchup.
    /// </summary>
    public void Eat()
    {
        if (!open)
        {
            Output.WriteMessageLn("Det ser ud til at denne ketchup er lukket, og kan derfor ikke spises.");
        }
        else if (wasted)
        {
            Output.WriteMessageLn("Du slikker ketchuppen op fra jorden; meget klamt men det slukker sulten.");
            GameManager.Instance.Win("Med den klamme ketchup i din hals, vandt du videospillet - hvis det virkelig kan kaldes at vinde B-)...");
        }
        else if (eaten)
        {
            Output.WriteMessageLn("Nogen har allerede spist alt ketchuppen.");
        }
        else
        {
            this.eaten = true;
            Output.WriteMessageLn("Du spiser ketchuppen; den smager meget godt.");
            GameManager.Instance.Win("Med den kolde ketchup i din hals, vandt du videospillet B-)...");
        }
    }

    /// <summary>
    /// Open the lid of the ketchup.
    /// </summary>
    public void Open()
    {
        if (thrown)
        {
            Output.WriteMessageLn("Du holder ikke nogen ketchup");
        } 
        else if (open)
        {
            Output.WriteMessageLn("Ketchuppen er allerede åben.");
        }
        else
        {
            this.open = true;
            Output.WriteMessageLn("Du åbner forsigtigt ketchuppen.");
        }
    }

    /// <summary>
    /// Fail in closing the lid of the ketchup.
    /// </summary>
    public void Close()
    {
        if (!open)
        {
            Output.WriteMessageLn("Ketchuppen er allerede lukket");
        }
        else
        {
            Output.WriteMessageLn("Du tabte låget, og kan ikke finde det mere.");
            Output.WriteMessageLn("Du må nøjes med åben ketchup.");
        }
    }
    public void Collect()
    {
        if (thrown)
        {
            Output.WriteMessageLn("Du samler ketchuppen op");
            this.thrown = false;
        }
        else 
        {
            Output.WriteMessageLn("Du kan ikke samle noget op som du har");
        }
    }

}