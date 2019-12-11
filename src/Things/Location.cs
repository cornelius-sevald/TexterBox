using System.Collections.Generic;

/// <summary>
/// Thing representing a location in the world.
/// </summary>
public class Location : Thing
{

    public List<Thing> things = null;

    /// <summary>
    /// Construct a new location.
    /// </summary>
    /// <param name="id">The indentifier of the object.</param>
    /// <param name="noun">The noun describing this object.</param>
    /// <param name="prepositions">The prepositions describing this object.</param>
    /// <param name="adjectives">The adjectives describing this object.</param>
    public Location(string id, string noun, string[] prepositions, string[] adjectives, List<Thing> things)
     : base(id, noun, prepositions, adjectives)
    {
        this.things = things;
    }

    /// <summary>
    /// Construct a new location with no prepositions or adjectives.
    /// </summary>
    /// <param name="id">The indentifier of the object.</param>
    /// <param name="noun">The noun describing this object.</param>
    public Location(string id, string noun, List<Thing> things)
     : base(id, noun)
    {
        this.things = things;
    }

    /// <summary>
    /// Let the player arrive at this location.
    /// </summary>
    /// <param name="player"></param>
    public void Arrive(Player player)
    {
        if (player.location == this)
        {
            Output.WriteMessageLn("Du er allerede ved " + Id);
            return;
        }
        Output.WriteMessageLn("Du går til " + Id);
        player.location = this;
        foreach (Thing thing in things)
        {
            GameManager.Instance.AddThing(thing);
        }
    }

    /// <summary>
    /// Let the player leave this location.
    /// </summary>
    /// <param name="player"></param>
    public void Leave(Player player)
    {
        if (player.location != null && player.location.Id != this.Id)
        {
            return;
        }
        Output.WriteMessageLn("Du forlader " + Id);
        player.location = null;
        foreach (Thing thing in things)
        {
            GameManager.Instance.RemoveThing(thing);
        }
    }
}