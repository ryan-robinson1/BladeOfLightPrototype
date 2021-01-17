
/**
 * The Achievement class is in charge of tracking if an achievement is unlocked
 * or not.
 * 
 * @author Maxfield Barden
 */
public class Achievement
{
    private string id;
    private string lockState;
    private bool unlocked;

    /**
     * A new Achievement object.
     * 
     * @param id The achievement's string identifier.
     * @param unlocked Tells whether the achievement is unlocked or not.
     */
    public Achievement(string id, string lockState)
    {
        this.id = id;
        this.lockState = lockState;
        unlocked = IsUnlocked();
    }

    /**
     * Returns the string id of this Achievement.
     * 
     * @return The string id.
     */
    public string Getid()
    {
        return id;
    }

    /**
     * Determines if this achievement is still locked or not at the beginning
     * of the round.
     */
    public bool IsUnlocked()
    {
        return lockState == "unlocked";
    }

    /**
     * Sets the unlocked boolean value to true upon the achievement being unlocked.
     */
    public void SetUnlocked()
    {
        lockState = "unlocked";
        unlocked = true;
    }
}
