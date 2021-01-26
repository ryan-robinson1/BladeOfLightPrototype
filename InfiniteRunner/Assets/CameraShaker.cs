using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Controls the camera shake functionality.
 * 
 * @author Maxfield Barden
 */
public class CameraShaker : MonoBehaviour
{
    private Animator _camAnim;

    /**
     * Start is called before the first frame update.
     */
    void Start()
    {
        _camAnim = this.GetComponentInChildren<Animator>();
    }

    /**
     * Plays the hit shake effect.
     */
    public void PlayHitShake()
    {
        _camAnim.SetTrigger("hitShake");
    }

    /**
     * Plays the attack shake effect.
     */
    public void PlayAttackShake()
    {
        _camAnim.SetTrigger("attackShake");
    }

    /**
     * Plays the deflect shake.
     */
    public void PlayDeflectShake()
    {
        _camAnim.SetTrigger("deflectShake");
    }
}
