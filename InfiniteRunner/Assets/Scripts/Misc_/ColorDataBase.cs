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
    private const float heroIntensity = 16f;
    private const float swordIntensity = 8f;

    /**
     * Dictionary of hero colors.
     */
    public static Dictionary<string, Color> heroColors =
        new Dictionary<string, Color>()
        {
            {"hero-blue", new Color(
                0.012f * heroIntensity, 0.41f * heroIntensity, 0.75f * heroIntensity) },

            {"hero-red", new Color(
                0.85f * heroIntensity, 0.11f * heroIntensity, 0.11f * heroIntensity) },

            {"hero-pink", new Color(
                0.75f * heroIntensity, 0.016f * heroIntensity, 0.35f * heroIntensity) },

            {"hero-purple", new Color(
                0.275f * heroIntensity, 0.07f * heroIntensity, 0.75f * heroIntensity) },
        };

    /**
     * Dictionary of sword colors.
     */
    public static Dictionary<string, Color> swordColors =
        new Dictionary<string, Color>()
        {
            {"sword-blue", new Color(
                0.012f * swordIntensity, 0.41f * swordIntensity, 0.75f * swordIntensity) },

            {"sword-red", new Color(
                0.85f * swordIntensity, 0.11f * swordIntensity, 0.11f * swordIntensity) },

            {"sword-pink", new Color(
                0.75f * swordIntensity, 0.016f * swordIntensity, 0.35f * swordIntensity) },

            {"sword-purple", new Color(
                0.275f * swordIntensity, 0.07f * swordIntensity, 0.75f * swordIntensity) },
        };

    // will need to change this later to make it more variable
    private static Color heroColor = heroColors["hero-blue"];

    private static Color swordColor = swordColors["sword-red"];


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
     * Returns the active color on the blade.
     * 
     * @return The color of the Sword.
     */
    public static Color GetSwordColor()
    {
        return swordColor;
    }

    /**
     * Returns the base albedo of the sword. Scales down the intensity
     * to prevent too much emission.
     * 
     * @return The albedo value of the base sword offset.
     */
    public static Color GetSwordAlbedo()
    {
        Color offset = swordColor;
        offset.r = swordColor.r / swordIntensity;
        offset.g = swordColor.g / swordIntensity;
        offset.b = swordColor.b / swordIntensity;
        return offset;
    }

    /**
     * Returns the color of the health pack. The color of the health pack
     * is the same color of the hero model, but with the heroIntensity scaled
     * down.
     * 
     * @return The color of the healthpack.
     */
    public static Color GetHealthPackColor()
    {
        Color healthColor = heroColor;
        healthColor.r = heroColor.r / heroIntensity;
        healthColor.g = heroColor.g / heroIntensity;
        healthColor.b = heroColor.b / heroIntensity;
        return healthColor;
    }
}
