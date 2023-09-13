using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    [SerializeField] AudioClip heartPickUpSFX;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(heartPickUpSFX, Camera.main.transform.position, .4f);
        FindObjectOfType<GameSession>().IncreaseLife();
        Destroy(gameObject);
    }

}
