using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementHandler : MonoBehaviour
{
    private Dictionary<string, Achievement> achievements;
        

    /**
     * Called before the first frame update.
     */
    void Start()
    {
        achievements = new Dictionary<string, Achievement>(){

            {"score1000", new Achievement(
                "score1000", PlayerPrefs.GetString("score1000", "locked")) },

        };

        AchievementEvents.aEvents.onAchievementUnlocked += UnlockAchievement;
        Debug.Log($"The {achievements["score1000"].Getid()} is {achievements["score1000"].IsUnlocked()}");
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
        PlayerPrefs.SetString(id, "unlocked");
        achievements[id].SetUnlocked();
        Debug.Log("you unlocked the " + id + " achievement");
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
}
