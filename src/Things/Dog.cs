namespace TexterBox
{
    /// <summary>
    /// A dog object that the player can interact with.
    /// <para>
    /// Dog can be eaten and punched.
    /// </para>
    /// </summary>
    public class Dog : Thing, IEdible, IPunchable
    {
        static private string id = "hund";


        /// <summary>
        /// Is the dog alive?
        /// </summary>
        public bool alive = true;

        /// <summary>
        /// Has the dog been eaten?
        /// </summary>
        public bool eaten = false;

        /// <summary>
        /// Construct a dog object with an identifying noun,
        /// prepositions and adjectives.
        /// </summary>
        public Dog(string noun, string[] prepositions, string[] adjectives)
         : base(id, noun, prepositions, adjectives) { }

        /// <summary>
        /// Construct a dog object with an identifying noun.
        /// </summary>
        public Dog(string noun)
         : base(id, noun) { }

        /// <summary>
        /// Method to punch dog with.
        /// </summary>
        public void Punch()
        {
            if (alive)
            {
                alive = false;
                Output.WriteMessageLn("Du slår hunden, og den føler det ikke så godt.");
            }
            else
            {
                Output.WriteMessageLn("Du slår hunden igen, men der sker ikke så meget.");
            }
        }

        /// <summary>
        /// Method to eat the dog.
        /// </summary>
        public void Eat()
        {
            if (alive)
            {
                Output.WriteMessageLn("Du forsøger at spise hunden, men den er ikke samarbejdsvillig.");
                GameManager.Instance.Lose("Hunden gik sicko mode, og dræbte dig.");
            }
            else
            {
                Output.WriteMessageLn("Du spiser hunden.");
                GameManager.Instance.Win("Med hundens varme kød i din mave, er du ikke sulten mere. Du vandt videospillet... B-)");
            }
        }
    }
}