using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float radius = 3f;
    [SerializeField] Vector2 explosionForce = new Vector2(10000f, 5000f);

    Animator myAnimator;



    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void ExplodeBomb()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Player"));

        if (playerCollider)
        {
            
            playerCollider.GetComponent<Rigidbody2D>().AddForce(explosionForce);

            playerCollider.GetComponent<Player>().Hurt();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        myAnimator.SetTrigger("Burning");

    }

    void DestroyBomb()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
