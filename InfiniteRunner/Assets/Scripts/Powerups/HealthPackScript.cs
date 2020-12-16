using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackScript : MonoBehaviour
{
    private float healAmount = 25f;
    public Material canisterColor;
    DamageManager _dm;
    // Start is called before the first frame update
    void Start()
    {
        canisterColor.SetColor("emissionColor", ColorDataBase.GetCurrentHeroColor());
        canisterColor.SetColor("baseColor", ColorDataBase.GetHealthPackColor());
    }

    /**
     * Heals the player object upon being collided with.
     * 
     * @param collision The collision box we are detecting.
     */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.GetComponent<Collider>().enabled = false;
            _dm = collision.gameObject.GetComponent<DamageManager>();
            _dm.Heal(healAmount);
            this.Destruct();
        }
    }

    /**
     * Destroys the health pack on collision with hero.
     */
    private void Destruct()
    {
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
