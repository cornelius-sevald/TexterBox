using System;
using System.Linq;

namespace TexterBox
{
    /// <summary>
    /// Class representing an interaction between an action and a thing.
    /// </summary>
    public class Interaction
    {
        /// <summary>
        /// The thing this interation will act upon.
        /// </summary>
        public Thing thing;

        /// <summary>
        /// The verb that triggers the interaction.
        /// </summary>

        public string verb;
        /// <summary>
        /// The action that will happen if the interaction matches.
        /// </summary>
        public Action<Thing> interactFunc;

        /// <summary>
        /// Construct a new interaction.
        /// </summary>
        /// <param name="thing">The thing to interact with.</param>
        /// <param name="verb">The trigger of the interaction.</param>
        /// <param name="interactFunc">The action of the interaction.</param>
        public Interaction(Thing thing, string verb, Action<Thing> interactFunc)
        {
            this.thing = thing;
            this.verb = verb;
            this.interactFunc = interactFunc;
        }

        /// <summary>
        /// Attempt to match a sentence to this interaction.
        /// <para>
        /// If the interaction matches, <c>interactFunc</c> is called.
        /// </para>
        /// </summary>
        /// <param name="sentence">The sentence to match.</param>
        /// <returns>An <c>InteractionMatch</c> status.</returns>
        public InteractionMatch Match(Sentence sentence)
        {
            if (thing.noun != sentence.noun.Id)
            {
                return new InteractionFaliure(sentence.noun.Copy());
            }
            foreach (Token prep in sentence.prepositions)
            {
                if (!thing.prepositions.Any(s => s == prep.Id))
                {
                    return new InteractionFaliure(prep.Copy());
                }
            }
            foreach (Token adj in sentence.adjectives)
            {
                if (!thing.adjectives.Any(s => s == adj.Id))
                {
                    return new InteractionFaliure(adj.Copy());
                }
            }
            if (verb != sentence.verb.Id)
            {
                return new InteractionFaliure(sentence.verb.Copy());
            }
            interactFunc(thing);
            return new InteractionSucess();
        }
    }

    /// <summary>
    /// Class for giving information about a interaction match.
    /// <seealso cref="TexterBox.Interaction.Match(Sentence)"/>.
    /// </summary>
    public abstract class InteractionMatch { }

    /// <summary>
    /// Class representing a successful interaction match.
    /// No additional information is needed.
    /// </summary>
    public class InteractionSucess : InteractionMatch { }

    /// <summary>
    /// Class representing an unsuccessful interaction match.
    /// Holds a copy of the mismatching token for context.
    /// </summary>
    public class InteractionFaliure : InteractionMatch
    {
        /// <summary>
        /// The mismatching token.
        /// </summary>
        public Token mismatch;

        /// <summary>
        /// Construct a new <c>InteractionFaliure</c>.
        /// </summary>
        /// <param name="mismatch">The mismatching token.</param>
        public InteractionFaliure(Token mismatch)
        {
            this.mismatch = mismatch;
        }
    }
}