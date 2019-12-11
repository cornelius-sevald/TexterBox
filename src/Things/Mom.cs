public class Mom : Thing
{
    static private string id = "mor";

    /// <summary>
    /// Construct a mom with an identifying noun,
    /// prepositions and adjectives.
    /// </summary>
    public Mom(string noun, string[] prepositions, string[] adjectives)
     : base(id, noun, prepositions, adjectives) { }

    /// <summary>
    /// Construct a mom with an identifying noun.
    /// </summary>
    public Mom(string noun)
     : base(id, noun) { }

    /// <summary>
    /// Send the player to the shadow realm.
    /// </summary>
    public void Send(Thing thing)
    {
        GameManager.Instance.Lose("Mor sender dig til skyggeverdenen.\nDu er allerede død.");
    }
}
