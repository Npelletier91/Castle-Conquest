using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string stringFromOutSide = FindObjectOfType<Cube>().PrintingFromOutside(5);
        Debug.Log(stringFromOutSide);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
