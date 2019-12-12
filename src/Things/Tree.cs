/// <summary>
/// A tree object that the player can interact with.
/// <para>
/// Tree can be eaten, opened and punched.
/// </para>
/// </summary>
public class Tree : Thing, IEdible, IOpenable, IPunchable
{
    static private string id = "træ";

    Nut nut;

    /// <summary>
    /// Has the tree been opened?
    /// </summary>
    public bool nutSpawn = false;

    /// <summary>
    /// Has the tree been eaten?
    /// </summary>
    public bool eaten = false;

    /// <summary>
    /// How many times have the tree been punched
    /// </summary>
    public int punchedTree = 0;

    /// <summary>
    /// Construct a tree object with an identifying noun,
    /// prepositions and adjectives.
    /// </summary>
    public Tree(string noun, string[] prepositions, string[] adjectives, Nut nut)
     : base(id, noun, prepositions, adjectives) {this.nut = nut;}

    /// <summary>
    /// Construct a tree object with an identifying noun.
    /// </summary>
    public Tree(string noun, Nut nut)
     : base(id, noun) {this.nut = nut;}

    /// <summary>
    /// Eat the tree.
    /// </summary>
    public void Eat()
    {
        this.eaten = true;
        Output.WriteMessageLn("Du prøver at spise træet, men dens bark er for stærk.");
        GameManager.Instance.Lose("Du har permanent ødelagt dine tænder og forbliver sulten...");
    }

    /// <summary>
    /// Open the tree.
    /// </summary>
    public void Open()
    {
        if(nutSpawn)
        {
            Output.WriteMessageLn("Du åbner træet men der er ingen nødder derinde.");
        }
        else
        {
            Output.WriteMessageLn("Du åbner træet og finder en nød.");
            nutSpawn = true;
            GameManager.Instance.things.Add(nut);
            GameManager.Instance.player.location.things.Add(nut);
        }
    }

    /// <summary>
    /// Punch the tree.
    /// </summary>
    public void Punch()
    {
        if(nutSpawn)
        {
            Output.WriteMessageLn("Du slår træet men der sker ingenting.");
            punchedTree++;
        }
        else
        {
            Output.WriteMessageLn("Du slår træet og der falder en nød ned.");
            nutSpawn = true;
            GameManager.Instance.things.Add(nut);
            GameManager.Instance.player.location.things.Add(nut);
            punchedTree++;
        }
        if(punchedTree >= 3)
        {
            Output.WriteMessageLn("Din hånd gør ondt.");
        }
        if(punchedTree >= 10)
        {
            GameManager.Instance.Lose("Du har slået træet for meget og du mistet alt energi i kroppen og kan derfor ikke spise.");
        }
    }
}