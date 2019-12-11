public class Pants : Thing, IEdible, IThrowable
{
    static private string id = "bukser";

    /// <summary>
    /// Have the pants been eaten?
    /// </summary>
    public bool eaten = false;

    /// <summary>
    /// Is the player wearing pants?
    /// </summary>
    public bool worn = true;

    /// <summary>
    /// Construct pants object with an identifying noun,
    /// prepositions and adjectives.
    /// </summary>
    public Pants(string noun, string[] prepositions, string[] adjectives)
     : base(id, noun, prepositions, adjectives) { }

    /// <summary>
    /// Construct pants object with an identifying noun.
    /// </summary>
    public Pants(string noun)
     : base(id, noun) { }

     public void Eat(){
         if(eaten){
            Output.WriteMessageLn("Du har allerede spist dine bukser...");
         } else {
            Output.WriteMessageLn("Du spiser dine bukser.");
            GameManager.Instance.Lose("Når du æder bukserne for at stoppe din sult får bukseforgiftning og dør instantly.");
         }
     }

     public void Throw(){
         if(worn){
             GameManager.Instance.player.pants = false;
             Output.WriteMessageLn("Du smider dine bukser væk ind i en busk.");
         } else {
             Output.WriteMessageLn("Du har ikke nogle bukser på.");
         }
     }
}