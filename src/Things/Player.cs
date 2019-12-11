/// <summary>
/// Thing representing the player.
/// </summary>
public class Player : Thing
{
    static private string id = "player";

    /// <summary>
    /// Is the player wearing pants?
    /// </summary>
    public bool pants = true;

    /// <summary>
    /// Has the player shit yet?
    /// </summary>
    public bool shid = false;

    /// <summary>
    /// Construct a new player with an identifying noun,
    /// prepositions and adjectives.
    /// </summary>
    public Player(string noun, string[] prepositions, string[] adjectives)
     : base(id, noun, prepositions, adjectives) { }

    /// <summary>
    /// Construct a new player with an identifying noun.
    /// </summary>
    public Player(string noun)
     : base(id, noun) { }

    /// <summary>
    /// Throw something, if throwable.
    /// </summary>
    /// <param name="thing">Thing to throw.</param>
    public void ThrowThing(Thing thing)
    {
        if (thing is IThrowable t)
        {
            t.Throw();
        }
    }

    /// <summary>
    /// Eat something, if edible.
    /// </summary>
    /// <param name="thing">Thing to eat.</param>
    public void EatThing(Thing thing)
    {
        if (thing is IEdible t)
        {
            t.Eat();
        }
    }

    /// <summary>
    /// Open something, if openable.
    /// </summary>
    /// <param name="thing">Thing to open.</param>
    public void OpenThing(Thing thing)
    {
        if (thing is IOpenable t)
        {
            t.Open();
        }
    }

    /// <summary>
    /// Close something, if closeable.
    /// </summary>
    /// <param name="thing">Thing to close.</param>
    public void CloseThing(Thing thing)
    {
        if (thing is ICloseable t)
        {
            t.Close();
        }
    }

    /// <summary>
    /// Stop something, if stoppable.
    /// </summary>
    /// <param name="thing">Thing to stop.</param>
    public void StopThing(Thing thing)
    {
        if (thing is IStoppable t)
        {
            t.Stop();
        }
    }

    /// <summary>
    /// Punch something, if punchable.
    /// </summary>
    /// <param name="thing">Thing to punch.</param>
    public void PunchThing(Thing thing)
    {
        if (thing is IPunchable t)
        {
            t.Punch();
        }
    }

    /// <summary>
    /// Collect something, if collectable.
    /// </summary>
    /// <param name="thing">Thing to collect.</param>
    public void CollectThing(Thing thing)
    {
        if (thing is ICollectable t)
        {
            t.Collect();
        }
    }
}