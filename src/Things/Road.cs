namespace TexterBox
{
    /// <summary>
    /// A road object that the player can walk over.
    /// <para>
    /// Road can be walked over.
    /// </para>
    /// </summary>
    public class Road : Thing, ICrossable
    {
        static private string id = "vej";

        /// <summary>
        /// Has the player crossed the road.
        /// </summary>
        public bool crossed = false;

        /// <summary>
        /// A car that crosses the road.
        /// </summary>
        public Car car = null;

        /// <summary>
        /// Construct a road object with an identifying noun,
        /// prepositions and adjectives.
        /// </summary>
        public Road(string noun, string[] prepositions, string[] adjectives, Car car)
         : base(id, noun, prepositions, adjectives)
        {
            this.car = car;
        }

        /// <summary>
        /// Construct a road object with an identifying noun.
        /// </summary>
        public Road(string noun, Car car)
         : base(id, noun)
        {
            this.car = car;
        }

        /// <summary>
        /// Cross the road.
        /// </summary>
        public void Cross()
        {
            if (car == null || car.waitedFor == true)
            {
                crossed = !crossed;
                Output.WriteMessageLn("Du går over vejen...");
            }
            else
            {
                Output.WriteMessageLn("En blå bil kørte dig over.");
                GameManager.Instance.Lose("Med dine brækket ben, kan du ikke spise mad og du dør.");
            }
        }
    }
}