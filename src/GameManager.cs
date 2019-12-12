using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

/// <summary>
/// The global game state and game logic.
/// This class is a singleton.
/// </summary>
public sealed class GameManager : Thing, ICloseable, IStoppable
{
    private static GameManager instance = null;
    private static readonly object mutextLock = new object();
    private static readonly string tokenFilePath = Path.Combine("Resources", "tokens.tkn");
    private static readonly string id = "spil";

    private GameState state;
    private Token[] validTokens;
    public List<Thing> things;
    public List<Interaction> interactions;

    public Player player = null;

    public int timeLeft;

    private bool hungry = false;

    private bool veryHungry = false;

    /// <summary>
    /// Construct a new player with an identifying noun,
    /// prepositions and adjectives.
    /// </summary>
    public GameManager(string noun, string[] prepositions, string[] adjectives)
     : base(id, noun, prepositions, adjectives)
    {
        SetUpGame();
        state = GameState.GameGaming;
    }

    void SetUpGame()
    {
        timeLeft = 100;

        /* The valid tokens: */

        validTokens = TokenUtils.FromFile(tokenFilePath);

        /* The things in the game: */

        ActionThing action = ActionThing.Instance;

        player = new Player(
            "spiller",
            new string[] { },
            new string[] { }
        );

        Pants pants = new Pants(
            "bukser",
            new string[] { },
            new string[] {
                "orange",
                "god",
                "wholesome"
            }
        );

        Ketchup ketchup = new Ketchup(
            "ketchup",
            new string[] { },
            new string[] {
                "rød",
                "god",
                "kold"
            }
        );

        Nut nut = new Nut(
            "nød",
            new string[] { },
            new string[] {
                "brun",
                "kold"
            }
        );

        Tree tree = new Tree(
           "træ",
           new string[] { },
           new string[] {
                "brun",
                "kold"
           },
           nut
       );

        Car car = new Car(
            "bil",
            new string[] { },
            new string[] {
                "blå",
                "hurtig"
            }
        );

        Road road = new Road(
            "vej",
            car
        );

        Food food = new Food(
            "mad",
            road
        );

        Mom mom = new Mom(
            "mor"
        );

        Location legeplads = new Location(
            "legeplads",
            "en legeplads",
            "legeplads",
            new List<Thing> { tree }
        );
        Location supermarket = new Location(
            "supermarket",
            "supermarkedet",
            "supermarket",
            new List<Thing> { road, car, food }
        );
        Location home = new Location(
            "hjem",
            "dit hjem",
            "hjem",
            new List<Thing> { mom }
        );


        things = new List<Thing> { this, action, player, ketchup, pants, legeplads, supermarket, home };

        /* The interactions in the game: */

        interactions = new List<Interaction> {
            // Actions
            new Interaction(action, "vent", this.Wait),
            new Interaction(action, "owo", this.Smite),
            new Interaction(action, "shit", this.Shit),
            new Interaction(action, "se", this.Look),
            // Program interactions:
            new Interaction(this, "luk",  player.CloseThing),
            new Interaction(this, "stop", player.StopThing),
            // Location interactions:
            new Interaction(legeplads, "gå", player.GoToLocation),
            new Interaction(supermarket, "gå", player.GoToLocation),
            new Interaction(home, "gå", player.GoToLocation),
            // Mom interactions
            new Interaction(mom, "slå", mom.Send),
            // Ketchup interactions:
            new Interaction(ketchup, "kast", player.ThrowThing),
            new Interaction(ketchup, "spis", player.EatThing),
            new Interaction(ketchup, "åben", player.OpenThing),
            new Interaction(ketchup, "luk",  player.CloseThing),
            new Interaction(ketchup, "hent", player.CollectThing),
            new Interaction(ketchup, "giv", player.GiveThing),
            // Tree interactions:
            new Interaction(tree, "spis", player.EatThing),
            new Interaction(tree, "åben", player.OpenThing),
            new Interaction(tree, "slå", player.PunchThing),
            // Nut interactions:
            new Interaction(nut, "spis", player.EatThing),
            new Interaction(nut, "åben", player.OpenThing),
            new Interaction(nut, "slå", player.PunchThing),
            new Interaction(nut, "hent", player.CollectThing),
            new Interaction(nut, "kast", player.ThrowThing),
            // Car interactions
            new Interaction(car, "vent", player.WaitForThing),
            // Road interactions
            new Interaction(road, "kryds", player.CrossThing),
            new Interaction(road, "gå", player.CrossThing),
            // Food interactions
            new Interaction(food, "spis", player.EatThing),
            new Interaction(food, "hent", player.CollectThing),
            // Pants interactions
            new Interaction(pants, "spis", player.EatThing),
            new Interaction(pants, "kast", player.ThrowThing),
            new Interaction(pants, "hent", player.CollectThing)
        };

    }

    /// <summary>
    /// Add a dog to the list of things
    /// as well as some interactions.
    /// </summary>
    void AddDog()
    {
        Dog dog = new Dog(
            "hund",
            new string[] { },
            new string[] { "wholesome", "irriterende" }
        );

        things.Add(dog);

        interactions.Add(
            new Interaction(
                dog,
                "slå",
                player.PunchThing
            )
        );
        interactions.Add(
            new Interaction(
                dog,
                "spis",
                player.EatThing
            )
        );

        Output.WriteMessageLn("Du ser en irriterende hund der følger efter dig.");
    }

    /// <summary>
    /// The main loop of the game.
    /// </summary>
    public void GameLoop()
    {
        Output.WriteMessage("Du er på vej tilbage fra Ben’s hus da du pludselig føler en stærk sult I maven, ");
        Thread.Sleep(400);
        Output.WriteMessage("du er klar over hvad det her betyder, ");
        Thread.Sleep(400);
        Output.WriteMessageLn("der er noget der skal spises snart.");
        Output.WriteMessageLn(" ");
        Thread.Sleep(800);

        (things.Find(t => t.Id == "legeplads") as Location).Arrive(player);
        Thread.Sleep(400);
        Output.WriteMessageLn("Du burde se dig omkring.");

        while (state == GameState.GameGaming)
        {
            if (--timeLeft <= 0)
            {
                Lose("Du ventede i for lang tid, og døde af sult.");
                break;
            }
            if (timeLeft <= 80)
            {
                if (!things.Any(t => t.Id == "hund"))
                {
                    AddDog();
                }
            }
            if (timeLeft <= 60)
            {
                if (hungry == false)
                {
                    Output.WriteMessage("Du føler dig noget sulten, ");
                    Thread.Sleep(400);
                    Output.WriteMessageLn("du burde nok finde noget at spise snart.");
                }
                hungry = true;
            }
            if (timeLeft <= 30)
            {
                if (veryHungry == false)
                {
                    Output.WriteMessage("Din mave rumler med lyden af tomhed, ");
                    Thread.Sleep(400);
                    Output.WriteMessageLn("du føler du kommer til at dø snart hvis du ikke får noget at spise.");
                }
                veryHungry = true;
            }

            // Parse input.
            string input = Input.GetInput();
            if (input == "") {
                continue;
            }
            if (input == null)
            {
                state = GameState.GameLost;
                break;
            }

            Token[] tokenized = Lexer.LexInput(validTokens, input);
            Sentence sentence = Sentence.EmptySentence();
            if (!Parser.ParseTokens(tokenized, ref sentence))
            {
                Output.WriteMessageLn("Du forstår ikke hvad du selv tænker...\nVær lidt mere specifik.");
                continue;
            }
            DoSentence(sentence);
        }
    }

    /// <summary>
    /// Do the action (if any) of a sentence.
    /// This checks if the sentence matches any of <c>interactions</c>,
    /// and executes the first one that matches.
    /// </summary>
    /// <param name="sentence">The sentence to act on.</param>
    public void DoSentence(Sentence sentence)
    {
        InteractionFaliure[] faliures = new InteractionFaliure[interactions.Count];
        for (int i = 0; i < interactions.Count; i++)
        {
            Interaction interaction = interactions[i];

            if (!things.Contains(interaction.thing))
            {
                faliures[i] = new InteractionFaliure(new Token(TokenType.NounToken, interaction.thing.noun));
                continue;
            }

            InteractionMatch match = interaction.Match(sentence);

            if (match is InteractionSucess)
            {
                return;
            }
            faliures[i] = (match as InteractionFaliure);
        }

        // Loop through all of the faliures, and find the 'best' one.
        // They are ranked after:
        // No matching noun, no matching verb, no matching adjective, no matching preposition
        InteractionFaliure best = faliures[0];
        foreach (InteractionFaliure faliure in faliures)
        {
            // If the numerical value of the token type is greater
            // than the currently best one, make this the new best.
            if (best.mismatch.type < faliure.mismatch.type)
            {
                best = faliure;
            }
        }
        switch (best.mismatch.type)
        {
            case TokenType.NounToken:
                Output.WriteMessageLn("Du ser ingen " + best.mismatch.Id);
                break;
            case TokenType.VerbToken:
                Output.WriteMessageLn("Du bestemmer dig for at det ikke er en god ide.");
                break;
            case TokenType.AdjectiveToken:
                Output.WriteMessageLn("Der er ingen " + best.mismatch.Id + " " + sentence.noun.Id);
                break;
            case TokenType.PrepositionToken:
                Output.WriteMessageLn("øhhhhh..");
                break;
        }
    }


    /// <summary>
    /// Wait some time.
    /// </summary>
    /// <param name="thing">The object that caused the waiting.</param>
    public void Wait(Thing thing)
    {
        timeLeft -= 25;
    }

    /// <summary>
    /// Smite klaus.
    /// </summary>
    /// <param name="thing"></param>
    public void Smite(Thing thing)
    {
        Lose("Fuck dig Klaus");
    }

    public void Look(Thing thing)
    {
        Output.WriteMessage("Du ser ");
        if (things.Count == 0)
        {
            Output.WriteMessageLn("ikke noget.");
        }
        else
        {
            int i = 0;
            for (; i < things.Count - 1; i++)
            {
                Thing lookThing = things[i];
                switch (lookThing.Id)
                {
                    case "action":
                    case "player":
                    case "spil":
                        break;
                    default:
                        Output.WriteMessage(lookThing.Id + ", ");
                        break;
                }
            }
            Output.WriteMessageLn(things[i].Id + ".");
        }
    }

    public void Shit(Thing thing)
    {
        if (GameManager.Instance.player.pants == true)
        {
            Output.WriteMessageLn("Du skider");
            GameManager.Instance.Lose("Med en umådelig kraft ryger lorten ud før du opdager at du ikke har taget dine bukser af, det er dog allerede for sent, der opstår en brun plet ved din bagside og dit syn falmer...");
        }
        else
        {
            Output.WriteMessageLn("Du skider på jorden");
            GameManager.Instance.Win("Du ved ikke hvorfor, men med lorten på vejen fylder det dig med en vis tilfredsstilelse, du mærker ikke sulten mere...");
        }
    }

    /// <summary>
    /// Win the game.
    /// </summary>
    /// <param name="winMessage">The message to display to the player.</param>
    public void Win(string winMessage)
    {
        state = GameState.GameWon;

        Thread.Sleep(3000);
        Console.Clear();
        Output.WriteMessageLn(winMessage);
        System.Console.ReadKey();
    }


    /// <summary>
    /// Lose the game.
    /// </summary>
    /// <param name="loseMessage">The message to display to the player.</param>
    public void Lose(string loseMessage)
    {
        state = GameState.GameLost;

        Thread.Sleep(3000);
        Console.Clear();
        Output.WriteMessageLn(loseMessage);
        Thread.Sleep(1000);
        Output.WriteMessageLn("Du har tabt videospillet.");
        System.Console.ReadKey();
    }

    /// <summary>
    /// Close the game.
    /// <seealso cref="GameManager.Stop"/>
    /// </summary>
    public void Close()
    {
        Output.WriteMessageLn("Du lukker videospillet.");
        Win("Man kan ikke være sulten, hvis man ikke findes. Du vinder.");
    }

    /// <summary>
    /// Stop the game.
    /// <seealso cref="GameManager.Close"/>
    /// </summary>
    public void Stop()
    {
        Output.WriteMessageLn("Du stopper videospillet.");
        Win("Man kan ikke være sulten, hvis man ikke findes. Du vinder.");
    }

    /// <summary>
    /// Get the singleton instance of the game manager.
    /// If no instance is active, constructs a new one.
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            lock (mutextLock)
            {
                if (instance == null)
                {
                    instance = new GameManager(
                        "spil",
                        new string[] { },
                        new string[] { }
                    );
                }
                return instance;
            }
        }
    }
}

/// <summary>
/// Different game states.
/// </summary>
public enum GameState
{
    /// <summary>
    /// Currently gaming, i.e. the game is running.
    /// </summary>
    GameGaming,
    /// <summary>
    /// The game is lost.
    /// </summary>
    GameLost,
    /// <summary>
    /// The game is won.
    /// </summary>
    GameWon
}