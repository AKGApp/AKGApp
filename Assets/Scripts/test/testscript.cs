using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HelloWorld();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HelloWorld()
    {
        string messageHelloWorld = "Hello World !!!";
        Debug.Log(messageHelloWorld);
    }
}
