using System;
using UnityEngine;

public class AchievementEvents : MonoBehaviour
{
    public static AchievementEvents aEvents;

    /**
     * Awake is called when the script instance is being loaded.
     */
    private void Awake()
    {
        aEvents = this;
    }

    /**
     * Triggers an achievement to be unlocked.
     * 
     * @param id The id of the achievement.
     */
    public event Action<string> onAchievementUnlocked;
    public void UnlockAchievementTrigger(string id)
    {
        if (onAchievementUnlocked != null)
        {
            onAchievementUnlocked(id);
        }
    }
}
