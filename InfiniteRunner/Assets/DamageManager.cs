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
    [HideInInspector]
    public float health;

    private void Start()
    {
        health = startHealth;
    }

    //Method executed if hit by bullet. Takes damage.
    public void TakeDamage()
    {
        health -= bulletDamage;
        HealthBar.fillAmount = health / startHealth;
        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
