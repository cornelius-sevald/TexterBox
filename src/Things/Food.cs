/// <summary>
/// A food object that the player can interact with.
/// <para>
/// Food can be eaten and collected.
/// </para>
/// </summary>
public class Food : Thing, ICollectable, IEdible
{
    static private string id = "mad";

    /// <summary>
    /// Reference to the road, to see if the player can pick up the food.
    /// </summary>
    public Road road;

    /// <summary>
    /// Has the food been collected?
    /// </summary>
    public bool collected = false;

    /// <summary>
    /// Has the food been eaten?
    /// </summary>
    public bool eaten = false;

    /// <summary>
    /// Construct a food object with an identifying noun,
    /// prepositions and adjectives.
    /// </summary>
    public Food(string noun, string[] prepositions, string[] adjectives, Road road)
     : base(id, noun, prepositions, adjectives)
    {
        this.road = road;
    }

    /// <summary>
    /// Construct a food object with an identifying noun.
    /// </summary>
    public Food(string noun, Road road)
     : base(id, noun) 
    {
        this.road = road;
    }

    /// <summary>
    /// Collect the food.
    /// </summary>
    public void Collect()
    {
        if (collected)
        {
            Output.WriteMessageLn("Du har allerede maden.");
        }
        else if (road != null && !road.crossed)
        {
            Output.WriteMessageLn("Du står på den forkerte side af vejen.");
        }
        else
        {
            collected = true;
            GameManager.Instance.player.location.things.Remove(this);
            Output.WriteMessageLn("Du tager maden.");
        }
    }

    /// <summary>
    /// Eat the food.
    /// </summary>
    public void Eat()
    {
        if (!collected)
        {
            Output.WriteMessageLn("Du kan ikke spise mad du ikke ejer.");
        }
        else
        {
            eaten = true;
            Output.WriteMessageLn("Du spiser maden. Den smager ikke specielt godt.");
            GameManager.Instance.Win("Med middelmådigt supermarket-mad i dit spiserør, vinder du spillet... B-)");
        }
    }
}