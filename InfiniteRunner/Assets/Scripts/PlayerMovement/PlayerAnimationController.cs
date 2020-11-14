using UnityEngine;

/**
 * Controls the transitions for the heroAnimatorController. Takes in player
 * input and changes animation state accordingly. This script is required by
 * the PlayerController script which uses the different states of animation
 * to adjust player movement accordingly.
 * 
 * @author Maxfield Barden
 */
public class PlayerAnimationController : MonoBehaviour
{

    Animator _anim;

    /**
     * Called before first frame update and used to instantiate our variables.
     */
    void Start()
    {
        _anim = this.GetComponentInChildren<Animator>(); 
    }

    /**
     * Detects collision with the enemy and plays the attack animation
     * appropiately.
     * 
     * @param collision The collision we are detecting.
     */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.CompareTag("Enemy"))
        {
            _anim.SetTrigger("Attack");
        }
    }

    /**
     * Plays the Slide animation.
     */
    private void Slide()
    {
        if (Input.GetKeyDown(KeyCode.S) || SwipeInput.Instance.SwipeDown)
        {
            _anim.SetTrigger("Slide");
        }
    }

    /**
     * Sets the Attack animation trigger.
     */
    private void Attack()
    {
        // will need to add combo
        _anim.SetTrigger("Attack");
    }

    /**
     * Checks if the hero is currently sliding.
     * 
     * @return True if the hero is curently in the Slide state.
     */
    public bool IsSliding()
    {
        return _anim.GetCurrentAnimatorStateInfo(0).IsName("Slide");
    }


    /**
     * Checks if the hero is currently running.
     * 
     * @return True if the hero is currently in the Running state.
     */
    public bool IsRunning()
    {
        return _anim.GetCurrentAnimatorStateInfo(0).IsName("Running");
    }

    /**
     * Condenses our update logic to make our Update() method more readable.
     */
    private void UpdateAnimationState()
    {
        this.Slide();
    }

    /**
     * Called once per frame.
     */
    void Update()
    {
        this.UpdateAnimationState();
    }
}
