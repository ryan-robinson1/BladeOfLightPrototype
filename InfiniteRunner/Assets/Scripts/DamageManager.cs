using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DamageManager : MonoBehaviour
{
    public Image HealthBar;
    public float bulletDamage = 1;
    public float startHealth = 100;
    public Material healthIndicator;
    [HideInInspector]
    public float health;

    public ParticleSystem damageEffect;
    public ParticleSystem damageLightEffect;

    private float min = 0f;
    private float max = 0.03f;
    private float t = 0f;
    private float edgeWidth;

    Color[] colors;




    /**
     * Called before any of the Update methods are called for the first time.
     */
    private void Start()
    {
        health = startHealth;
        edgeWidth = healthIndicator.GetFloat("edgeWidth");
        colors = this.generateColors();
        healthIndicator.SetColor("dissolveColor", colors[0]);
    }

    /**
     * Called every fixed frame rate.
     */
    private void FixedUpdate()
    {
        this.UpdateHealthStatus(health);

    }

    /**
     * Displays information on screen.
     */
    private void OnGUI()
    {
/*        int fps = (int)(1.0f / Time.smoothDeltaTime);
        var style = new GUIStyle();
        style.fontSize = 50;
        style.normal.textColor = Color.green;
        GUI.Label(new Rect(0, 0, 100, 100), "FPS: " + fps, style);*/
    }

    //Method executed if hit by bullet. Takes damage.
    public void TakeDamage()
    {
        health -= bulletDamage;
        HealthBar.fillAmount = health / startHealth;
        damageEffect.Play();
        damageLightEffect.Play();
        if(health <= 0)
        {
            this.setDefaultMats();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    /**
     * Updates what color we should be displaying on the Player object
     * based on what the Player's current health is.
     */
    private void UpdateHealthStatus(float health)
    {
        float healthMultiplier = 0.5f;

        var minRender = (1.15f - (health / startHealth)) * healthMultiplier;
       
        healthIndicator.SetFloat("minimumRender", minRender);
        
        if (health / startHealth < 0.3f)
        {
            this.AnimateEdgeWidth();
        }
    }

    /**
     * Animates the width of the edge for Player object's material.
     * Used when the player is at low health to give a pulsating effect.
     */
    private void AnimateEdgeWidth()
    {
        float speed = 0.7f;
        edgeWidth = Mathf.Lerp(min, max, t);
        healthIndicator.SetFloat("edgeWidth", edgeWidth);

        t += speed * Time.deltaTime;

        if (t > 1f)
        {
            var temp = max;
            max = min;
            min = temp;
            t = 0f;
        }
    }

    /**
     * Called when user quits the application.
     */
    public void OnApplicationQuit()
    {
        this.setDefaultMats();
    }

    /**
     * Generates an array of Color objects that we will use to indicate
     * the player's health.
     * 
     * @return An Array of three Colors.
     */
    private Color[] generateColors()
    {
        Color[] c = new Color[3];
        float intensity = 16f;
        // blue
        c[0] = new Color(0.08f * intensity, 0.66f * intensity, 0.75f * intensity);
        // yellow
        c[1] = new Color(0.75f * intensity, 0.65f * intensity, 0.08f * intensity);
        // green
        c[2] = new Color(0.85f * intensity, 0.11f * intensity, 0.11f * intensity);
        return c;
    }

    /**
     * Returns the material back to its default state.
     */
    private void setDefaultMats()
    {
        healthIndicator.SetColor("dissolveColor", colors[0]);
        healthIndicator.SetFloat("edgeWidth", 0.03f);
        healthIndicator.SetFloat("noiseScale", 120f);
        healthIndicator.SetFloat("minimumRender", 0.15f);
    }

}
