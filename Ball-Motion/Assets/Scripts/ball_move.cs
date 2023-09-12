using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ball_move : MonoBehaviour
{
    public float speed = 15;
    private Rigidbody rbody;
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHoriz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHoriz, 0f, moveVert);
        rbody.AddForce(movement * speed);

        if (Input.GetButtonDown("Submit"))
        {
            LoadNextScene();
        }

    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
