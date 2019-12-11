/// <summary>
/// A ketchup object that the player can interact with.
/// <para>
/// Ketchup can be thrown, eaten and opened.
/// </para>
/// </summary>
public class Ketchup : Thing, IThrowable, IEdible, IOpenable, ICloseable
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
            Output.WriteMessageLn("Hvem ville kaste god ketchup mere end èn gang?");
            return;
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
        if (open)
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

}