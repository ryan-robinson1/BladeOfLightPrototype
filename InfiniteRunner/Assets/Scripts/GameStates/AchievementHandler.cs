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
     * IF ADDING AN ACHIEVEMENT ORGANIZE BY TYPE (i.e. Score-based, attack-based, etc.)
     */
    private void InitializeAchievements()
    {
        achievements = new Dictionary<string, Achievement>(){

            // SCORE BASED ACHIEVEMENTS

            {"score1500", new Achievement(
                "score1500", PlayerPrefs.GetString("score1500", "locked"), "Noob Assassin",
                "Acquire a score of 1,500 or better in a single run.")
            },

            {"score15k", new Achievement(
                "score15k", PlayerPrefs.GetString("score15k", "locked"), "Skilled Assassin",
                "Acquire a score of 15,000 or better in a single run.")
            },

            {"score50k", new Achievement(
                "score50k", PlayerPrefs.GetString("score50k", "locked"), "Elite Assassin",
                "Acquire a score of 50,000 or better in a single run.") 
            },

            {"score200k", new Achievement(
                "score200k", PlayerPrefs.GetString("score200k", "locked"), "Legendary Assassin",
                "Acquire a score of 200,000 or better in a single run.") 
            },

            // STREAK BASED ACHIEVEMENTS

            {"streak25", new Achievement(
                "streak25", PlayerPrefs.GetString("streak25", "locked"), "Chain 'em Together",
                "Acquire a streak of 25 or higher in a single run.") 
            },

            // THESE GO TOGETHER FOR THE MEME

            {"firsthealth", new Achievement(
                "firsthealth", PlayerPrefs.GetString("firsthealth", "locked"), "Call an Ambulance!",
                "Use a health pack for the first time.") 
            },

            {"kill200noheal", new Achievement(
                "kill200noheal", PlayerPrefs.GetString("kill200noheal", "locked"), "But not for me",
                "Eliminate 200 enemies in a single run without using a health pack.") 
            },

        };
    }
}
