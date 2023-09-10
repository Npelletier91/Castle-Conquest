using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDoor : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip openingDoorSFX;
    [SerializeField] AudioClip closingDoorSFX;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<Animator>().SetTrigger("Open");
    }

    void PlayOpenDoorSFX()
    {
        audioSource.PlayOneShot(openingDoorSFX);
    }
}
