using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Rigidbody2D myRigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        //PrintingToOurConsole();
    }

    // Update is called once per frame
    void Update()
    {
        MovinOurCube();
        OutOfBoundsPrinter();
    }

    public string PrintingFromOutside(int value)
    {
        string printingSomething = "The value we were sent is " + value;
        return printingSomething;
    }
    private void OutOfBoundsPrinter()
    {
        if (transform.position.x > 9.4f)
        {
            Debug.LogWarning("Cube is out of bounds to the Right side!");
        }
        if (transform.position.x < -9.4f)
        {
            Debug.LogWarning("Cube is out of bounds to the Left side!");
        }
        if (transform.position.y > 5.5f)
        {
            Debug.LogWarning("Cube is out of bounds to the Top!");
        }
        if (transform.position.y < -5.5f)
        {
            Debug.LogWarning("Cube is out of bounds to the Bottom!");
        }
    }

    private void MovinOurCube()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            myRigidbody2d.velocity = new Vector2(0f, 10f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            myRigidbody2d.velocity = new Vector2(0f, -10f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            myRigidbody2d.velocity = new Vector2(10f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            myRigidbody2d.velocity = new Vector2(-10f, 0f);
        }
    }
    private static void PrintingToOurConsole()
    {
        Debug.Log("Hello I'm writting to the debug console");
        Debug.LogError("Logging an Error message");
        Debug.LogWarning("Logging a Warning message");
    }
}
