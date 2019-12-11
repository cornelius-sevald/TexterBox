/// <summary>
/// Class representing a thing in the game.
/// </summary>
abstract public class Thing : TexterObject
{
    /// <summary>
    /// The noun that describes this thing.
    /// </summary>
    public string noun;

    /// <summary>
    /// The prepositions that describes this thing.
    /// </summary>
    public string[] prepositions;

    /// <summary>
    /// The adjectives that describes this thing.
    /// </summary>
    public string[] adjectives;

    /// <summary>
    /// Construct a new thing.
    /// </summary>
    /// <param name="id">The indentifier of the object.</param>
    /// <param name="noun">The noun describing this object.</param>
    /// <param name="prepositions">The prepositions describing this object.</param>
    /// <param name="adjectives">The adjectives describing this object.</param>
    public Thing(string id, string noun, string[] prepositions, string[] adjectives)
    {
        this.Id = id;
        this.noun = noun;
        this.prepositions = prepositions;
        this.adjectives = adjectives;
    }

    /// <summary>
    /// Construct a new thing with no prepositions or adjectives.
    /// </summary>
    /// <param name="id">The indentifier of the object.</param>
    /// <param name="noun">The noun describing this object.</param>
    public Thing(string id, string noun)
    {
        this.Id = id;
        this.noun = noun;
        this.prepositions = new string[] { };
        this.adjectives = new string[] { };
    }
}

/// <summary>
/// Thing that can be thrown.
/// <para>
/// This is only meant to be implemented by the <c>Thing</c> class.
/// </para>
/// </summary>
public interface IThrowable
{

    /// <summary>
    /// Trow the thing.
    /// </summary>
    void Throw();
}

/// <summary>
/// Thing that can be eaten.
/// <para>
/// This is only meant to be implemented by the <c>Thing</c> class.
/// </para>
/// </summary>
public interface IEdible
{

    /// <summary>
    /// Eat the thing.
    /// </summary>
    void Eat();
}

/// <summary>
/// Thing that can be opened.
/// <para>
/// This is only meant to be implemented by the <c>Thing</c> class.
/// </para>
/// </summary>
public interface IOpenable
{

    /// <summary>
    /// Open the thing.
    /// </summary>
    void Open();
}

/// <summary>
/// Thing that can be closed.
/// <para>
/// This is only meant to be implemented by the <c>Thing</c> class.
/// </para>
/// </summary>
public interface ICloseable
{

    /// <summary>
    /// Close the thing.
    /// </summary>
    void Close();
}

/// <summary>
/// Thing that can be stopped.
/// <para>
/// This is only meant to be implemented by the <c>Thing</c> class.
/// </para>
/// </summary>
public interface IStoppable
{

    /// <summary>
    /// Stop the thing.
    /// </summary>
    void Stop();
}

/// <summary>
/// Thing that can be punched.
/// <para>
/// This is only meant to be implemented by the <c>Thing</c> class.
/// </para>
/// </summary>
public interface IPunchable
{

    /// <summary>
    /// Punch the thing.
    /// </summary>
    void Punch();
}

/// <summary>
/// Thing that can be collected.
/// <para>
/// This is only meant to be implemented by the <c>Thing</c> class.
/// </para>
/// </summary>
public interface ICollectable
{

    /// <summary>
    /// Collect the thing.
    /// </summary>
    void Collect();
}


/// <summary>
/// Thing that can be waited for.
/// <para>
/// This is only meant to be implemented by the <c>Thing</c> class.
/// </para>
/// </summary>
public interface IWaitable
{

    /// <summary>
    /// Wait for a thing.
    /// </summary>
    void Wait();
}

/// <summary>
/// Thing that can be crossed.
/// <para>
/// This is only meant to be implemented by the <c>Thing</c> class.
/// </para>
/// </summary>
public interface ICrossable
{

    /// <summary>
    /// Cross a thing.
    /// </summary>
    void Cross();
}