using System.Collections;
using UnityEngine;

/**
 * Handles the playing of the Sword Slash effect. The Coroutine
 * waits a certain amount of time to play the slash effect
 * depending on the animation that is currently being played.
 * 
 * @author Maxfield Barden
 */

[RequireComponent(typeof(PlayerAnimationController))]
public class SwordSlashSpawner : MonoBehaviour
{
    public ParticleSystem slashEffect;

    /**
     * Instantiates a new SlashEffect at the current given position
     * of the sword.
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

