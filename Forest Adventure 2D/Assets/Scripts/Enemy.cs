using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemyRunSpeed = 5f;


    Rigidbody2D enemyRigidBody2D;
    BoxCollider2D enemyBoxCollider2D;
    PolygonCollider2D enemyPolCollider2D;




    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
        enemyBoxCollider2D = GetComponent<BoxCollider2D>();
        enemyPolCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Run();
        enemyTouching();


    }



    private void Run()
    {
        if (IsFacingLeft())
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
        if (enemyPolCollider2D.IsTouchingLayers(LayerMask.GetMask("Player"))){

            enemyRunSpeed = 0f;
            StartCoroutine(EnemyPause());
            FlipSprite();
        }
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

    private bool IsFacingLeft()
    {
        return transform.localScale.x > 0;
    }
}
