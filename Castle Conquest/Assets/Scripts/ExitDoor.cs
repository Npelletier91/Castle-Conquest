using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    AudioSource audioSource;


    [SerializeField] float secondsToLoad = 2f;
    [SerializeField] AudioClip openingDoorSFX;
    [SerializeField] AudioClip closingDoorSFX;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Animator>().SetTrigger("Open");
    }

    public void StartLoadingNextScene()
    {
        GetComponent<Animator>().SetTrigger("Close");

        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(secondsToLoad);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    void PlayOpenDoorSFX()
    {
        audioSource.PlayOneShot(openingDoorSFX);
    }
    void PlayCloseDoorSFX()
    {
        audioSource.PlayOneShot(closingDoorSFX);
    }
}
