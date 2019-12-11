using System;
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
    private static readonly string tokenFilePath = "src/Tokens/tokens.tkn";
    private static readonly string id = "spil";

    private GameState state;
    private Token[] validTokens;
    private List<Thing> things;
    private List<Interaction> interactions;

    public Player player = null;
    public Location start = null;

    public int timeLeft;

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

        start = new Location("en legeplads", "legeplads", new List<Thing> {tree});
        Location supermarket = new Location("supermarkedet", "supermarket", new List<Thing> {road, car, food});
        Location home = new Location("dit hjem", "hjem", new List<Thing> {});


        things = new List<Thing> { this, action, player, ketchup, pants, start, supermarket, home};

        /* The interactions in the game: */

        interactions = new List<Interaction> {
            // Actions
            new Interaction(action, "vent", this.Wait),
            new Interaction(action, "owo", this.Smite),
            new Interaction(action, "shit", this.Shit),
            // Program interactions:
            new Interaction(this, "luk",  player.CloseThing),
            new Interaction(this, "stop", player.StopThing),
            // Location interactions:
            new Interaction(start, "gå", player.GoToLocation),
            new Interaction(supermarket, "gå", player.GoToLocation),
            new Interaction(home, "gå", player.GoToLocation),
            // Ketchup interactions:
            new Interaction(ketchup, "kast", player.ThrowThing),
            new Interaction(ketchup, "spis", player.EatThing),
            new Interaction(ketchup, "åben", player.OpenThing),
            new Interaction(ketchup, "luk",  player.CloseThing),
            new Interaction(ketchup, "hent", player.CollectThing),
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

    public void AddThing(Thing thing)
    {
        things.Add(thing);
    }

    public void RemoveThing(Thing thing)
    {
        things.Remove(thing);
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
        start.Arrive(player);
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
            string input = Input.GetInput();
            if (input == null)
            {
                state = GameState.GameLost;
                break;
            }
            Token[] tokenized = Lexer.LexInput(validTokens, input);
            Sentence sentence = Sentence.EmptySentence();
            if (!Parser.ParseTokens(tokenized, ref sentence))
            {
                Output.WriteMessageLn("Du forstår ikke hvad du selv tænker...");
                Output.WriteMessageLn("Vær lidt mere specifik.");
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
        timeLeft -= 35;
    }

    public void Smite(Thing thing)
    {
        GameManager.Instance.Lose("Fuck dig Klaus");
    }

    public void Shit(Thing thing)
    {
        if (GameManager.Instance.player.pants == true)
        {
            Output.WriteMessageLn("Du skider");
            GameManager.Instance.Lose("Med en umådelig kraft ryger lorten ud før du opdager at du ikke har taget dine bukser af, det er dog allerede for sent, der opstår en brun plet ved din bagside og dit syn falmer...");
        } else {
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
    /// <see cref="GameManager.Stop"/>
    /// </summary>
    public void Close()
    {
        Output.WriteMessageLn("Du lukker videospillet.");
        Win("Man kan ikke være sulten, hvis man ikke findes. Du vinder.");
    }

    /// <summary>
    /// Stop the game.
    /// <see cref="GameManager.Close"/>
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