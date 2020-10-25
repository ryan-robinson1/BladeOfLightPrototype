using UnityEngine;

/**
 * Gives the enemy the dissolve effect and destroys the enemy upon getting
 * hit by the player.
 * 
 * @author Maxfield Barden
 */
public class DissolveEnemyScript : MonoBehaviour
{

    Collider collider;
    Renderer[] renderers;
    public Material offSetDissolveMaterial;
    public Material armorMat;
    public Material armorStrip;

    private float shaderLifetime = 1f;
    private bool dissolving = false;
    private float minRender;
    private float dissolveStrength = 3f;
    private string[] matNames;

    /**
     * Called before the first frame update.
     */
    void Start()
    {
        collider = this.GetComponent<Collider>();
        renderers = this.GetComponentsInChildren<Renderer>();
        minRender = armorMat.GetFloat("minimumRender");
        matNames = this.generateMatNames();
    }

    /**
     * Called every fixed framerate frame and updates the game state.
     */
    private void FixedUpdate()
    {
        if (dissolving)
        {
            minRender += Time.deltaTime * dissolveStrength;
            foreach (Renderer rend in renderers)
            {
                foreach (Material mat in rend.materials)
                {
                    mat.SetFloat("minimumRender", minRender); 
                }
            }
        }
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
            // can delete following line of code to make enemy bounce back
            // slightly upon getting hit
            this.GetComponent<Rigidbody>().isKinematic = true;

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
                // set the new dissolve material according to what 
                // this material is
                if (rend.materials[i].name == matNames[0])
                {
                    mats[i] = armorMat;
                }
                else if (rend.materials[i].name == matNames[1])
                {
                    mats[i] = offSetDissolveMaterial;
                }
                else
                {
                    mats[i] = armorMat;
                }
            }
            rend.materials = mats;
        }

        dissolving = true;
    }

    /**
     * Helper method that generates the names of our materials in the form
     * of strings in order to allow for comparison.
     * 
     * @return The names of the materials in an array of strings.
     */
    private string[] generateMatNames()
    {
        // should only ever have two mats, can adjust later if necessary
        string[] names = new string[2];
        // first entry is main material, second entry is armor strip
        names[0] = $"{armorMat} (Instance)";
        names[0] = names[0].Replace(" (UnityEngine.Material)", "");
        names[1] = $"{armorStrip} (Instance)";
        names[1] = names[1].Replace(" (UnityEngine.Material)", "");
        return names;
    }
}
