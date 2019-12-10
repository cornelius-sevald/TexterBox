/// <summary>
/// Superclass for identifiable objects.
/// </summary>
public abstract class TexterObject {
    private string id;

    /// <summary>
    /// The identifying string of this object.
    /// Does not need to be unique.
    /// </summary>
    public string Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }
}