namespace TexterBox
{
    /// <summary>
    /// The action thing allows for sentences without a noun.
    /// <para>
    /// For example, sentences like "wait" can be valid.
    /// </para>
    /// </summary>
    public sealed class ActionThing : Thing
    {
        private static ActionThing instance = null;
        private static readonly object mutextLock = new object();

        /// <summary>
        /// Construct a new singleton action thing.
        /// </summary>
        public ActionThing()
         : base("action", "", new string[] { }, new string[] { }) { }


        /// <summary>
        /// Get the singleton instance of the action thing.
        /// If no instance is active, constructs a new one.
        /// </summary>
        public static ActionThing Instance
        {
            get
            {
                lock (mutextLock)
                {
                    if (instance == null)
                    {
                        instance = new ActionThing();
                    }
                    return instance;
                }
            }
        }

    }
}