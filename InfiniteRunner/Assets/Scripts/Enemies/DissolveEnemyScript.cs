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
    Renderer[] renderers;
    public Material dissolveMaterial;

    // Start is called before the first frame update
    void Start()
    {
        collider = this.GetComponent<Collider>();
        renderers = this.GetComponentsInChildren<Renderer>();
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
            this.GetComponent<Rigidbody>().useGravity = false;
            this.GetComponent<ShootScript>().enabled = false;
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
        // change materials of each renderer on this object
        foreach (Renderer rend in renderers)
        {
            var mats = new Material[rend.materials.Length];
            for (var i = 0; i < rend.materials.Length; i++)
            {
                mats[i] = dissolveMaterial;
            }
            rend.materials = mats;
        }
    }
}
