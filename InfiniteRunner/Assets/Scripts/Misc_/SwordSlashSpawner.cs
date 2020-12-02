using System.Collections;
using UnityEngine;

/**
 * Handles the playing of the Sword Slash effect. Updates the particle
 * system's position and rotation based on the location of the sword
 * object.
 * 
 * @author Maxfield Barden
 */

[RequireComponent(typeof(PlayerAnimationController))]
public class SwordSlashSpawner : MonoBehaviour
{
    public ParticleSystem slashEffect;

    /**
     * Instantiates a new SlashEffect at the current given position
     * of the sword. Updates the position and rotation of our slash.
     * 
     * @param delayTimer The amount of time to delay before playing slash
     * animation.
     */
    public IEnumerator Slash(float delayTimer)
     {
        yield return new WaitForSeconds(delayTimer);
        slashEffect.Play();
     }
}

