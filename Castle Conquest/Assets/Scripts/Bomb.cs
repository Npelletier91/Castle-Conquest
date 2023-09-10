using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float radius = 3f;
    [SerializeField] Vector2 explosionForce = new Vector2(10000f, 5000f);
    [SerializeField] AudioClip burningSFX, explodingSFX;


    Animator myAnimator;
    AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
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

    void PlayBurningSound()
    {
        audioSource.PlayOneShot(burningSFX);
    }
    void PlayExplodingSound()
    {
        audioSource.PlayOneShot(explodingSFX);
    }
}
