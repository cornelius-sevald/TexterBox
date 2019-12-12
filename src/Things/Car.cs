/// <summary>
/// A car object that the player can slightly interact with.
/// </summary>
public class Car : Thing, IWaitable
{
    static private string id = "bil";


    /// <summary>
    /// Has the car been waited for?
    /// </summary>
    public bool waitedFor = false;

    /// <summary>
    /// Construct a car object with an identifying noun,
    /// prepositions and adjectives.
    /// </summary>
    public Car(string noun, string[] prepositions, string[] adjectives)
     : base(id, noun, prepositions, adjectives) { }

    /// <summary>
    /// Construct a car object with an identifying noun.
    /// </summary>
    public Car(string noun)
     : base(id, noun) { }

    /// <summary>
    /// Wait for the car.
    /// </summary>
    public void Wait()
    {
        waitedFor = true;
        GameManager.Instance.timeLeft -= 15;
        Output.WriteMessageLn("Du venter tålmodigt på bilen.");
    }
}