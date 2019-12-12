using System.Collections.Generic;

namespace TexterBox
{
    /// <summary>
    /// Thing representing a location in the world.
    /// </summary>
    public class Location : Thing
    {

        /// <summary>
        /// The name of the location.
        /// </summary>
        public string name;

        /// <summary>
        /// List of things in a specified location. 
        /// </summary>
        public List<Thing> things;

        /// <summary>
        /// Construct a new location.
        /// </summary>
        /// <param name="id">The indentifier of the object.</param>
        /// <param name="name">The name of the location.</param>
        /// <param name="noun">The noun describing this object.</param>
        /// <param name="prepositions">The prepositions describing this object.</param>
        /// <param name="adjectives">The adjectives describing this object.</param>
        /// <param name="things">The things in this location.</param>
        public Location(string id, string name, string noun, string[] prepositions, string[] adjectives, List<Thing> things)
         : base(id, noun, prepositions, adjectives)
        {
            this.name = name;
            this.things = things;
        }

        /// <summary>
        /// Construct a new location with no prepositions or adjectives.
        /// </summary>
        /// <param name="id">The indentifier of the object.</param>
        /// <param name="name">The name of the location.</param>
        /// <param name="noun">The noun describing this object.</param>
        /// <param name="things">The things in this location.</param>
        public Location(string id, string name, string noun, List<Thing> things)
         : base(id, noun)
        {
            this.name = name;
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
                Output.WriteMessageLn("Du er allerede ved " + name + ".");
                return;
            }
            Output.WriteMessageLn("Du ankommer ved " + name + ".");
            player.location = this;
            foreach (Thing thing in things)
            {
                GameManager.Instance.things.Add(thing);
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
            Output.WriteMessageLn("Du forlader " + name + ".");
            player.location = null;
            foreach (Thing thing in things)
            {
                GameManager.Instance.things.Remove(thing);
            }
        }
    }
}