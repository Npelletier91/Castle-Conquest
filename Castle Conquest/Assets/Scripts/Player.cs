using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 8f;
    [SerializeField] float attackRadius = 3f;

    [SerializeField] Vector2 hurtSpeed = new Vector2(10f, 10f);
    [SerializeField] Transform hurtBox;

    [SerializeField] AudioClip jumpingSFX, attackingSFX, gettingHitSFX, walking;

    Rigidbody2D myRigidBody2D;
    Animator myAnimator;
    BoxCollider2D myBoxCollider2D;
    PolygonCollider2D myPlayersFeet;
    AudioSource audioSource;

    bool isHurtting = false;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        myPlayersFeet = GetComponent<PolygonCollider2D>();
        audioSource = GetComponent<AudioSource>();

        myAnimator.SetTrigger("Appearing");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHurtting)
        {
            Run();
            Jump();
            Climb();
            Attack();
            ExitLevel();
            FallDeath();

            if (myRigidBody2D.IsTouchingLayers(LayerMask.GetMask("Enemy")))
            {
                Hurt();
            }
        }

    }

    private void ExitLevel()
    {
        if (!myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Door"))) { return; }

        if (CrossPlatformInputManager.GetButtonDown("Vertical"))
        {
            myAnimator.SetTrigger("Disappearing");
        }
    }

    public void LoadNextLevel()
    {
        FindObjectOfType<ExitDoor>().StartLoadingNextScene();
        TurnOffRenderer();
    }

    public void TurnOffRenderer()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Attack()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            myAnimator.SetTrigger("isAttacking");

            Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(hurtBox.position, attackRadius, LayerMask.GetMask("Enemy"));

            foreach (Collider2D enemy in enemiesToHit)
            {
                enemy.GetComponent<Enemy>().Dying();
            }

            audioSource.PlayOneShot(attackingSFX, 0.5f);
        }
    }

    private void Climb()
    {
        bool canClimb = myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"));

        if (canClimb)
        {
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 playerVelocity = new Vector2(myRigidBody2D.velocity.x, controlThrow * runSpeed);
            myRigidBody2D.velocity = playerVelocity;

            myRigidBody2D.gravityScale = 0f;

            myAnimator.SetBool("isClimbing", true);
        }
        else
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidBody2D.gravityScale = 1f;

        }

    }

    private void Jump()
    {

        bool canJump = myPlayersFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        bool isJumping = CrossPlatformInputManager.GetButtonDown("Jump");

        if (canJump)
        {
            if (isJumping)
            {
                Vector2 jumpVelocity = new Vector2(myRigidBody2D.velocity.x, jumpSpeed);
                myRigidBody2D.velocity = jumpVelocity;

                audioSource.PlayOneShot(jumpingSFX);

            }
        }
        
    }

    private void FallDeath()
    {
        bool fell = myPlayersFeet.IsTouchingLayers(LayerMask.GetMask("DeathTiles"));

        if (fell)
        {
            audioSource.PlayOneShot(gettingHitSFX);
            StartCoroutine(StopHurting());

            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private void Run()
    {

        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody2D.velocity.y);
        myRigidBody2D.velocity = playerVelocity;


        FlipSprite();


        // get the running state to activate isRunning boolean
        if (Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon)
        {
            myAnimator.SetBool("isRunning", true);
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }
    }

    void RunningSound()
    {
        bool playerIsMoving = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;

        if (playerIsMoving)
        {
            if (myPlayersFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                audioSource.PlayOneShot(walking);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void FlipSprite()
    {
        bool isRunning = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;

        if (isRunning)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody2D.velocity.x) ,1f);
        }

    }

    public void Hurt()
    {

        Vector2 hitVelocity = hurtSpeed * new Vector2(-transform.localScale.x, 1f);
        myRigidBody2D.velocity = hitVelocity;

        myAnimator.SetTrigger("IsHurt");
        audioSource.PlayOneShot(gettingHitSFX);
        isHurtting = true;

        StartCoroutine(StopHurting());

        FindObjectOfType<GameSession>().ProcessPlayerDeath();

    }
    IEnumerator StopHurting()
    {
        yield return new WaitForSeconds(0.75f);
        isHurtting = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(hurtBox.position, attackRadius);
    }

}
