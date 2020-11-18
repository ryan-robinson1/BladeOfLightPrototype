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
    private float noiseReset = 30f;
    private float noiseCutoff = 200f;
    private float animationSpeed = 0.05f;
    [HideInInspector]
    public float health;

    private float min = 30f;
    private float max = 200f;
    private float t = 0f;


    /**
     * Called before any of the Update methods are called for the first time.
     */
    private void Start()
    {
        health = startHealth;
        noiseScale = healthIndicator.GetFloat("noiseScale");
        this.ResetNoise();
    }

    /**
     * Called every fixed frame rate.
     */
    private void FixedUpdate()
    {
        this.AnimateNoise();

    }

    //Method executed if hit by bullet. Takes damage.
    public void TakeDamage()
    {
        health -= bulletDamage;
        HealthBar.fillAmount = health / startHealth;
        if(health <= 0)
        {
            this.ResetNoise();
        }
    }

    /**
     * Animates the noise on the hero model to give the hero a more lively
     * look and feel.
     */
    private void AnimateNoise()
    {
        noiseScale = Mathf.Lerp(min, max, t);
        healthIndicator.SetFloat("noiseScale", noiseScale);

        t += animationSpeed * Time.deltaTime;

        if (t > 1f)
        {
            float temp = max;
            max = min;
            min = temp;
            t = 0f;
        }
        
    }

    /**
     * Resets the noise to keep it from going too high.
     */
    private void ResetNoise()
    {
        // this is a lil hacky might need to fix it later
        noiseScale = noiseReset;
    }

}
