using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Gives the enemy the dissolve effect and destroys the enemy upon getting
 * hit by the player.
 * 
 * @author Maxfield Barden
 */
public class DissolveEnemyScript : MonoBehaviour
{

    private float shaderLifetime = 1f;
    Collider collider;
    Renderer renderer;
    public Material dissolveMaterial;

    // Start is called before the first frame update
    void Start()
    {
        collider = this.GetComponent<Collider>();
        renderer = this.GetComponentInChildren<Renderer>();
    }


    /**
     * Detects when the player has struck the enemy. The collider then disables
     * and the enemy sets to destroy itself.
     * 
     * @param collision The collision between the player and enemy.
     */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collider.enabled = false;
            this.Dissolve();
            this.Destruct();
        }
    }

    /**
     * Destroys the enemy model.
     */
    private void Destruct()
    {
        Destroy(this.gameObject, shaderLifetime);
    }

    /**
     * Creates the dissolve effect.
     */
    private void Dissolve()
    {
        // going to require a for loop and loop through game objects
        // changing each object's mat...must be a more efficient way
        // than that though
        renderer.material = dissolveMaterial;
    }
}
