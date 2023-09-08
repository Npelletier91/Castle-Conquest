using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 8f;
    [SerializeField] Vector2 hurtSpeed = new Vector2(10f, 10f);

    Rigidbody2D myRigidBody2D;
    Animator myAnimator;
    BoxCollider2D myBoxCollider2D;
    PolygonCollider2D myPlayersFeet;

    bool isHurtting = false;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        myPlayersFeet = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHurtting)
        {
            Run();
            Jump();
            Climb();

            Hurt();
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
            }
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

    private void FlipSprite()
    {
        bool isRunning = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;

        if (isRunning)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody2D.velocity.x) ,1f);
        }

    }

    private void Hurt()
    {
        if (myRigidBody2D.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            Vector2 hitVelocity = hurtSpeed * new Vector2(-transform.localScale.x, 1f);
            myRigidBody2D.velocity = hitVelocity;

            myAnimator.SetTrigger("IsHurt");
            isHurtting = true;

            StartCoroutine(StopHurting());
        }

    }
    IEnumerator StopHurting()
    {
        yield return new WaitForSeconds(0.75f);
        isHurtting = false;
    }
}
