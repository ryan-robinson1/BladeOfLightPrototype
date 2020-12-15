using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Keeps track of all the different colors used for customization
 * in the game.
 * 
 * @author Maxfield Barden
 */
public static class ColorDataBase
{
    private const float intensity = 16f;

    /**
     * Dictionary of colors.
     */
    public static Dictionary<string, Color> colorDict =
        new Dictionary<string, Color>()
        {
            {"default-blue", new Color(
                0.012f * intensity, 0.41f * intensity, 0.75f * intensity) },

            {"default-red", new Color(
                0.85f * intensity, 0.11f * intensity, 0.11f * intensity) },
        };

    // will need to change this later to make it more variable
    private static Color heroColor = colorDict["default-red"];


    /**
     * Returns the active color on the hero model.
     * 
     * @return The color of the Hero.
     */
    public static Color GetCurrentHeroColor()
    {
        // will modify based on selection
        return heroColor;
    }

    /**
     * Returns the color of the health pack. The color of the health pack
     * is the same color of the hero model, but with the intensity scaled
     * down.
     * 
     * @return The color of the healthpack.
     */
    public static Color GetHealthPackColor()
    {
        Color healthColor = heroColor;
        healthColor.r = heroColor.r / intensity;
        healthColor.g = heroColor.g / intensity;
        healthColor.b = heroColor.b / intensity;
        return healthColor;
    }
}
