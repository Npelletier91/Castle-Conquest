using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamonds : MonoBehaviour
{
    [SerializeField] AudioClip diamondPickUpSFX;
    [SerializeField] int diamondValue = 150;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        AudioSource.PlayClipAtPoint(diamondPickUpSFX, Camera.main.transform.position, .4f);

        FindObjectOfType<GameSession>().IncreaseScore(diamondValue);
        Destroy(gameObject);
    }
}
