/// <summary>
/// A Nut object that the player can interact with.
/// <para>
/// Nut can be thrown, eaten, opened, punched and collected.
/// </para>
/// </summary>
public class Nut : Thing, IThrowable, IEdible, IOpenable, IPunchable, ICollectable
{
    static private string id = "nut";

    /// <summary>
    /// Has the nut been destroyed?
    /// </summary>
    public bool destroyed = false;

    /// <summary>
    /// Has the nut been eaten?
    /// </summary>
    public bool eaten = false;

    /// <summary>
    /// Has the nut been collected?
    /// </summary>
    public bool collected = false;

    /// <summary>
    /// Construct a nut object with an identifying noun,
    /// prepositions and adjectives.
    /// </summary>
    public Nut(string noun, string[] prepositions, string[] adjectives)
     : base(id, noun, prepositions, adjectives) { }

    /// <summary>
    /// Construct a nut object with an identifying noun.
    /// </summary>
    public Nut(string noun)
     : base(id, noun) { }

    /// <summary>
    /// Throw the nut.
    /// </summary>
    public void Throw()
    {
        if (!collected)
        {
            Output.WriteMessageLn("Du kan ikke kaste noget du ikke har");
        }
        else
        {
            this.destroyed = true;
            this.collected = false;
            Output.WriteMessageLn("Du smider nøden på jorden og den går i stykker");
        }
    }

    /// <summary>
    /// Eat the nut.
    /// </summary>
    public void Eat()
    {
        if (!collected)
        {
            Output.WriteMessageLn("Du vil ikke æde noget fra jorden");
        }
        else
        {
            this.eaten = true;
            Output.WriteMessageLn("Du spiser nøden; den smager meget godt.");
            GameManager.Instance.Win("Med den kolde nød i din hals, vandt du videospillet D-)...");
        }
    }

    /// <summary>
    /// Open the nut
    /// </summary>
    public void Open()
    {
        if (!collected)
        {
            Output.WriteMessageLn("Du holder ikke nogen nød");
        } 
        else
        {
            Output.WriteMessageLn("Du indser at man ikke behøver åbne en hasselnød for at spise den og lader være");
        }
    }

    /// <summary>
    /// Collect the nut
    /// </summary>
    public void Collect()
    {
        if (destroyed)
        {
            Output.WriteMessageLn("Du kan ikke bruge en ødelagt nød til noget");
        }
        else 
        {
            Output.WriteMessageLn("Du samler nøden op");
            this.collected = true;
        }
    }

    /// <summary>
    /// Punch the nut
    /// </summary>
    public void Punch()
    {
        if (!collected)
        {
            Output.WriteMessageLn("Du vil ikke slå noget der ligger på jorden");
        }
        else 
        {
            Output.WriteMessageLn("Du slår nøden så den går i stykker og kaster den fra dig");
            this.destroyed = true;
            this.collected = false;
        }
    }
}