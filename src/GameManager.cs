using System;
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
    private Thing[] things;
    private Interaction[] interactions;

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
        /* The valid tokens: */

        validTokens = TokenUtils.FromFile(tokenFilePath);

        /* The things in the game: */

        ActionThing action = ActionThing.Instance;

        Player player = new Player(
            "spiller",
            new string[] { },
            new string[] { }
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

        action.player = player;

        things = new Thing[] { this, action, player, ketchup };

        /* The interactions in the game: */

        interactions = new Interaction[] {
            // Program interactions:
            new Interaction(this, "luk",  player.CloseThing),
            new Interaction(this, "stop", player.StopThing),
            // Ketchup interactions:
            new Interaction(ketchup, "kast", player.ThrowThing),
            new Interaction(ketchup, "spis", player.EatThing),
            new Interaction(ketchup, "åben", player.OpenThing),
            new Interaction(ketchup, "luk",  player.CloseThing)
        };
    }

    /// <summary>
    /// The main loop of the game.
    /// </summary>
    public void GameLoop()
    {
        while (state == GameState.GameGaming)
        {
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
        InteractionFaliure[] faliures = new InteractionFaliure[interactions.Length];
        for (int i = 0; i < interactions.Length; i++)
        {
            Interaction interaction = interactions[i];
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