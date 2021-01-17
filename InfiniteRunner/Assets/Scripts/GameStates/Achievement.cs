
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
    private string displayName;
    private string description;

    /**
     * A new Achievement object.
     * 
     * @param id The achievement's string identifier.
     * @param unlocked Tells whether the achievement is unlocked or not.
     */
    public Achievement(string id, string lockState, string displayName,
        string description)
    {
        this.id = id;
        this.lockState = lockState;
        this.displayName = displayName;
        this.description = description;
    }

    /**
     * Returns the string id of this Achievement. Used for scripting purposes.
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
     * Gets the lockState string.
     * 
     * @return the lockState string.
     */
    public string GetLockState()
    {
        return lockState;
    }

    /**
     * Returns the display name of the achievement. Used for UI purposes.
     * 
     * @return The displayName of this achievement.
     */
    public string GetDisplayName()
    {
        return displayName;
    }

    /**
     * Gets the description of this achievement. Used for UI purposes.
     * 
     * @return The description of this achievement.
     */
    public string GetDescription()
    { 
        return description;
    }

    /**
     * Sets the unlocked boolean value to true upon the achievement being unlocked.
     */
    public void SetUnlocked()
    {
        lockState = "unlocked";
    }
}
