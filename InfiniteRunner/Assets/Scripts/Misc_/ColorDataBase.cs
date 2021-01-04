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
    private const float enemyMainIntensity = 8f;
    private const float enemyStripIntensity = 24f;
    private const float swordIntensity = 8f;

    /**
     * Dictionary of hero colors. Sorted by alphabetical order.
     */
    public static Dictionary<string, Color> heroColors =
        new Dictionary<string, Color>()
        {
            // IF ADDING A COLOR PUT IT IN ALPHABETICAL ORDER
            {"hero-aqua", new Color(
                0f, 0.74f * heroIntensity, 0.75f * heroIntensity) },

            {"hero-blue", new Color(
                0.012f * heroIntensity, 0.41f * heroIntensity, 0.75f * heroIntensity) },

            {"hero-green", new Color(
                0.02f * heroIntensity, 0.75f * heroIntensity, 0.06f * heroIntensity) },

            {"hero-red", new Color(
                1f * heroIntensity, 0.05f * heroIntensity, 0.05f * heroIntensity) },

            {"hero-pink", new Color(
                0.75f * heroIntensity, 0.016f * heroIntensity, 0.35f * heroIntensity) },

            {"hero-purple", new Color(
                0.275f * heroIntensity, 0.07f * heroIntensity, 0.75f * heroIntensity) },
        };

    /**
     * Dictionary of enemy colors. Sorted by alphabetical order.
     */
    // WILL MODIFY TO HAVE ARRAY OF COLORS AS THE VALUE PAIR
    public static Dictionary<string, Color> enemyColors =
        new Dictionary<string, Color>()
        {
            // IF ADDING A COLOR PUT IT IN ALPHABETICAL ORDER

            // main dissolve colors
            {"enemyDissolve-aqua", new Color(
                0f, 0.74f * enemyMainIntensity, 0.75f * enemyMainIntensity) },

            {"enemyDissolve-green", new Color(
                0f, 0.75f * enemyMainIntensity, 0.03f * enemyMainIntensity) },

            {"enemyDissolve-orangeYellow", new Color(
                0.75f * enemyMainIntensity, 0.21f * enemyMainIntensity, 0f) },

            {"enemyDissolve-red", new Color(
                0.75f * enemyMainIntensity, 0.03f * enemyMainIntensity, 0.03f * enemyMainIntensity) },


            // armor strip colors
            {"enemyStrip-aqua", new Color(
                0f, 0.74f * enemyStripIntensity, 0.75f * enemyStripIntensity) },

            {"enemyStrip-green", new Color(
                0f, 0.75f * enemyStripIntensity, 0.004f * enemyStripIntensity) },

            {"enemyStrip-orangeYellow", new Color(
                0.75f * enemyStripIntensity, 0.21f * enemyStripIntensity, 0f) },

            {"enemyStrip-red", new Color(
                0.75f * enemyStripIntensity, 0.03f * enemyStripIntensity, 0.03f * enemyStripIntensity) },

            
        };

    /**
     * Dictionary of sword colors. Sorted by alphabetical order.
     */
    public static Dictionary<string, Color> swordColors =
        new Dictionary<string, Color>()
        {
            // IF ADDING A COLOR PUT IT IN ALPHABETICAL ORDER
            {"sword-aqua", new Color(
                0f, 0.74f * swordIntensity, 0.75f * swordIntensity) },

            {"sword-blue", new Color(
                0.012f * swordIntensity, 0.41f * swordIntensity, 0.75f * swordIntensity) },

            {"sword-green", new Color(
                0.02f * heroIntensity, 0.75f * heroIntensity, 0.06f * heroIntensity) },

            {"sword-red", new Color(
                1f * swordIntensity, 0.05f * swordIntensity, 0.05f * swordIntensity) },

            {"sword-pink", new Color(
                0.75f * swordIntensity, 0.016f * swordIntensity, 0.35f * swordIntensity) },

            {"sword-purple", new Color(
                0.275f * swordIntensity, 0.07f * swordIntensity, 0.75f * swordIntensity) },
        };


    // will need to change this later to make it more variable
    private static Color heroColor = heroColors["hero-green"];

    private static Color enemyMain = enemyColors["enemyDissolve-aqua"];
    private static Color enemyStrip = enemyColors["enemyStrip-aqua"];

    private static Color swordColor = swordColors["sword-green"];


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
     * Returns the main dissolve color on the enemy.
     * 
     * @return The main dissolve color of the Enemy.
     */
    public static Color GetMainEnemyArmorColor()
    {
        return enemyMain;
    }

    /**
     * Returns the armor strip color on the enemy.
     * 
     * @return The armor strip color on the enemy.
     */
    public static Color GetEnemyArmorStripColor()
    {
        return enemyStrip;
    }

    /**
     * Returns the albedo element of the enemy armor strip.
     * 
     * @return The lower intensity version of the armor strip.
     */
    public static Color GetEnemyStripAlbedo()
    {
        Color strip = enemyStrip;
        strip.r = enemyStrip.r / enemyStripIntensity;
        strip.g = enemyStrip.g / enemyStripIntensity;
        strip.b = enemyStrip.b / enemyStripIntensity;
        return strip;
    }

    /**
     * Gets the enemy armor strip dissolve material color.
     * 
     * @return a scaled up intensity of the enemy dissolve color.
     */
    public static Color GetStripDissolveColor()
    {
        Color strip = GetEnemyStripAlbedo();
        float stripDissolveIntensity = 32f;
        strip.r *= stripDissolveIntensity;
        strip.g *= stripDissolveIntensity;
        strip.b *= stripDissolveIntensity;
        return strip;
    }

    /**
     * Gets the enemy armor strip dissolve material color.
     * 
     * @return a scaled up intensity of the enemy dissolve color.
     */
    public static Color GetBulletColor()
    {
        Color bullet = GetEnemyStripAlbedo();
        float bulletIntensity = 0.1f;
        bullet.r *= bulletIntensity;
        bullet.g *= bulletIntensity;
        bullet.b *= bulletIntensity;
        return bullet;
    }

    /**
     * Gets the bullet trail color.
     * 
     * @return the color of the bullet trail
     */
    public static Color GetBulletTrail()
    {
        Color trail = GetEnemyStripAlbedo();
        float trailIntensity = 3f;
        trail.r *= trailIntensity;
        trail.g *= trailIntensity;
        trail.b *= trailIntensity;
        return trail;
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
    public static Color GetHeroAlbedo()
    {
        Color healthColor = heroColor;
        healthColor.r = heroColor.r / heroIntensity;
        healthColor.g = heroColor.g / heroIntensity;
        healthColor.b = heroColor.b / heroIntensity;
        return healthColor;
    }

    /**
     * Returns the color the UI buttons should be.
     * 
     * @return The proper color of the UI buttons.
     */
    public static Color GetUIColor()
    {
        Color ui = GetHeroAlbedo();
        float uiIntensity = 3f;
        ui.r *= uiIntensity;
        ui.g *= uiIntensity;
        ui.b *= uiIntensity;
        return ui; 
    }
}
