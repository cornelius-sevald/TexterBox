namespace TexterBox
{
    /// <summary>
    /// Thing representing the player.
    /// </summary>
    public class Player : Thing
    {
        static private string id = "player";

        /// <summary>
        /// The location of the player.
        /// </summary>
        public Location location = null;

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
        /// Move the player to a new location, if said thing is a location.
        /// </summary>
        /// <param name="thing">Location to go to.</param>
        public void GoToLocation(Thing thing)
        {
            if (thing is Location t)
            {
                if (t != location)
                {
                    location.Leave(this);
                }
                t.Arrive(this);
            }
        }

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

        /// <summary>
        /// Wait for something, if waitable.
        /// </summary>
        /// <param name="thing">Thing to wait for.</param>
        public void WaitForThing(Thing thing)
        {
            if (thing is IWaitable t)
            {
                t.Wait();
            }
        }

        /// <summary>
        /// Cross something, if crossable.
        /// </summary>
        /// <param name="thing">The thing to cross.</param>
        public void CrossThing(Thing thing)
        {
            if (thing is ICrossable t)
            {
                t.Cross();
            }
        }

        /// <summary>
        /// Give a thing, if givable.
        /// </summary>
        /// <param name="thing">The thing to give.</param>
        public void GiveThing(Thing thing)
        {
            if (thing is IGiveable t)
            {
                t.Give();
            }
        }
    }
}