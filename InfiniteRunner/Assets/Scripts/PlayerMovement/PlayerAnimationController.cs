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
    PlayerController player;
    SwordSlashSpawner slashSpawner;
    private int attacks = 0;
    private float attackTimer = 0f;
    private float attackReset = 2f;
    private int slashID;

    /**
     * Called before first frame update and used to instantiate our variables.
     */
    void Start()
    {
        _anim = this.GetComponentInChildren<Animator>();
        player = this.GetComponent<PlayerController>();
        slashSpawner = this.GetComponent<SwordSlashSpawner>();
    }

    /**
     * Makes the character start running once the game is started from the menu.
     */
    public void StartGame()
    {
        _anim.SetTrigger("Run");
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
            this.Attack();
            StartCoroutine(slashSpawner.Slash(this.GetSlashDelay(slashID)));
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
        // slide attack
        if (this.IsSliding())
        {
            _anim.SetTrigger("SlideAttack");
            player.setDeflects(0);
            slashID = -1;
            return;
        }
        if (attacks == 0)
        {
            _anim.SetTrigger("Attack");
            attacks++;
            slashID = 0;
        }
        else if (attacks == 1)
        {
            _anim.SetTrigger("AttackAgain");
            attacks++;
            slashID = 1;
        }
        else if (attacks == 2)
        {
            _anim.SetTrigger("Attack3");
            attacks = 0;
            slashID = 2;
        }
        player.setDeflects(0);
    }

    /**
     * Resets our attack counter if we haven't attacked in a few frames.
     */
    private void UpdateAttackCounter()
    {
       if (attacks > 0)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > attackReset)
            {
                attacks = 0;
                attackTimer = 0f;
            }
        }
    }

    /**
     * Gets how long of a delay we should have before playing our slash
     * particle effect.
     * 
     * @return The delay timer based on which attack animation we are playing.
     */
    private float GetSlashDelay(int attacks)
    {
        int attackNum = attacks;
        switch (attackNum)
        {
            // handles the slide case
            case -1:
                return 0.15f;

            // first attack
            case 0:
                return 0.15f;

            // second attack
            case 1:
                return 0.16f;

            default:
                return 0f;
        }
    }

    /**
     * Updates the number of deflects to loop through deflect
     * animation cycles.
     */
    public void UpdateDeflectCount()
    {
        _anim.SetInteger("deflects", player.getDeflects());   
    }

    /**
     * Updates our deflecting status.
     */
    public void UpdateDeflect()
    {
        if (_anim.GetBool("Deflecting"))
        {
            _anim.SetBool("Deflecting", false);
        }
        else
        {
            _anim.SetBool("Deflecting", true);
        }
    }

    /**
     * Checks if an attack animation is currently playing.
     * 
     * @return True if an attack animation is currently playing.
     */
    public bool IsAttacking()
    {
        return attacks > 0;
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
        this.UpdateDeflectCount();
        this.UpdateAttackCounter();
    }

    /**
     * Called once per frame.
     */
    void Update()
    {
        this.UpdateAnimationState();
    }
}
