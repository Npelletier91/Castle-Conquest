using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemyRunSpeed = 5f;


    Rigidbody2D enemyRigidBody2D;
    BoxCollider2D enemyBoxCollider2D;
    PolygonCollider2D enemyPolCollider2D;
    Animator myAnimator;




    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
        enemyBoxCollider2D = GetComponent<BoxCollider2D>();
        enemyPolCollider2D = GetComponent<PolygonCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Run();
        enemyTouching();

    }

    public void Dying()
    {
        myAnimator.SetTrigger("Die");

        GetComponent<CapsuleCollider2D>().enabled = false;
        enemyBoxCollider2D.enabled = false;
        enemyPolCollider2D.enabled = false;

        enemyRigidBody2D.velocity = Vector2.zero;
        enemyRigidBody2D.bodyType = RigidbodyType2D.Static;

        StartCoroutine(EnemyDeath());
    }

    private void Run()
    {
        if (transform.localScale.x > 0)
        {
            enemyRigidBody2D.velocity = new Vector2(-enemyRunSpeed, 0f);
        }
        else
        {
            enemyRigidBody2D.velocity = new Vector2(enemyRunSpeed, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        FlipSprite();

    }

    private void enemyTouching()
    {
        if (enemyPolCollider2D.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            enemyRunSpeed = 0f;
            StartCoroutine(EnemyPause());
        }
    }

    IEnumerator EnemyDeath()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

    }
    IEnumerator EnemyPause()
    {
        yield return new WaitForSeconds(2f);
        enemyRunSpeed = 5f;

    }



    private void FlipSprite()
    {
            transform.localScale = new Vector2(Mathf.Sign(enemyRigidBody2D.velocity.x), 1f);
    }

}
