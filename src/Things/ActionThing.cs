public sealed class ActionThing : Thing
{
    private static ActionThing instance = null;
    private static readonly object mutextLock = new object();

    public Player player = null;

    public ActionThing()
     : base("action", "", new string[] {}, new string[] {}) {}
    
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