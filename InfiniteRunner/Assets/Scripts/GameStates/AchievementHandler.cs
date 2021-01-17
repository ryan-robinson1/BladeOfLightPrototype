using System.Collections.Generic;
using UnityEngine;

/**
 * Responsible for handling the unlocking of achievements and updating our player
 * preferences as follows. Contains a dictionary database of achievements that can
 * be accessed by other scripts.
 * 
 * @author Maxfield Barden
 */

public class AchievementHandler : MonoBehaviour
{
    private Dictionary<string, Achievement> achievements;
        

    /**
     * Called before the first frame update.
     */
    void Start()
    {
        AchievementEvents.aEvents.onAchievementUnlocked += UnlockAchievement;
        Debug.Log($"The {achievements["score1000"].Getid()} is {achievements["score1000"].IsUnlocked()}");
    }

    /**
     * Called when the script instance is being loaded.
     */
    private void Awake()
    {
        InitializeAchievements();
    }

    /**
     * Sets the player prefs for the given achievement to become unlocked.
     * 
     * @param id The id of the achievement.
     */
    private void UnlockAchievement(string id)
    {
        // no need to execute this method if the achievement is already unlocked.
        if (achievements[id].IsUnlocked())
        {
            return;
        }

        // notify our player prefs that the achievement is unlocked
        PlayerPrefs.SetString(id, "unlocked");
        achievements[id].SetUnlocked();
        Debug.Log("you unlocked the " + achievements[id].GetDisplayName() + " achievement");
    }

    /**
     * Returns our dictionary of achievements so other scripts can access it.
     * 
     * @return The achievements dictionary.
     */
    public Dictionary<string, Achievement> GetAchievements()
    {
        return achievements;
    }

    /**
     * Called on the destruction of this MonoBehaviour script.
     */
    void OnDestroy()
    {
        AchievementEvents.aEvents.onAchievementUnlocked -= UnlockAchievement;
    }

    /**
     * Initializes the player achievement dictionary.
     */
    private void InitializeAchievements()
    {
        achievements = new Dictionary<string, Achievement>(){

            {"score1000", new Achievement(
                "score1000", PlayerPrefs.GetString("score1000", "locked"), "Noob Assassin",
                "Acquire a score of 1000 or better in a single run.")
            },

        };
    }
}
