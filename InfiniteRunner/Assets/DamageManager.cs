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
    private float noiseScale;
    [HideInInspector]
    public float health;


    private float min = 0f;
    private float max = 0.07f;
    private float t = 0f;
    private float edgeWidth;

    Color[] colors;




    /**
     * Called before any of the Update methods are called for the first time.
     */
    private void Start()
    {
        health = startHealth;
        noiseScale = healthIndicator.GetFloat("noiseScale");
        edgeWidth = healthIndicator.GetFloat("edgeWidth");
        colors = this.generateColors();
        healthIndicator.SetColor("dissolveColor", colors[2]);
    }

    /**
     * Called every fixed frame rate.
     */
    private void FixedUpdate()
    {
        this.UpdateHealthStatus();

    }

    //Method executed if hit by bullet. Takes damage.
    public void TakeDamage()
    {
        health -= bulletDamage;
        HealthBar.fillAmount = health / startHealth;
        if(health <= 0)
        {
            this.setDefaultMats();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    /**
     * Animates the noise on the hero model to give the hero a more lively
     * look and feel.
     */
    private void AnimateNoise()
    {
        var speed = 16f;
        noiseScale = Mathf.Lerp(min, max, t);
        healthIndicator.SetFloat("noiseScale", noiseScale);

        t += speed * Time.deltaTime;

        if (t > 1f)
        {
            float temp = max;
            max = min;
            min = temp;
            t = 0f;
        }
        
    }

    /**
     * Updates what color we should be displaying on the Player object
     * based on what the Player's current health is.
     */
    private void UpdateHealthStatus()
    {
        var midHealth = startHealth / 1.5f;
        var lowHealth = startHealth / 3f;
        if (health > midHealth)
        {
            healthIndicator.SetColor("dissolveColor", colors[0]);
        }
        else if (health <= midHealth && health > lowHealth){
            healthIndicator.SetColor("dissolveColor", colors[1]);
            healthIndicator.SetFloat("minimumRender", 0.45f);
        }
        else if (health <= lowHealth && health > 0)
        {
            healthIndicator.SetColor("dissolveColor", colors[2]);
            healthIndicator.SetFloat("minimumRender", 0.65f);
            this.AnimateEdgeWidth();
        }
    }

    /**
     * Animates the width of the edge for Player object's material.
     * Used when the player is at low health to give a pulsating effect.
     */
    private void AnimateEdgeWidth()
    {
        var speed = 0.7f;
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
        var intensity = 16f;
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
        healthIndicator.SetFloat("noiseScale", 67f);
        healthIndicator.SetFloat("minimumRender", 0.25f);
    }

}
