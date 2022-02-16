using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject square, circle, circleBlack, SquareBlack;
    bool squarCorrect, circleCorrect = false;

    // Add a function to make the game easier evertytime the player misses the 
    // the target by increasing the dsitance and reseting once it is done
    public float distanceToObject = 50.0f;

    Vector2 squareInitailPos, circleInitialPos;

    public AudioSource source;
    public AudioClip[] correct;
    public AudioClip incorrect;

    void Start()
    {
        squareInitailPos = square.transform.position;
        circleInitialPos = circle.transform.position;
    }
    public void DragSquare()
    {
        square.transform.position = Input.mousePosition;
    }

    public void DragCircle()
    {
        circle.transform.position = Input.mousePosition;
    }

    public void DropSquare()
    {
        float Distance = Vector3.Distance(square.transform.position, SquareBlack.transform.position);
        if (Distance < distanceToObject)
        {
            square.transform.position = SquareBlack.transform.position;
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
            squarCorrect = true;
        }
        else
        {
            square.transform.position = squareInitailPos;
            source.clip = incorrect;
            source.Play();
        }
        CheckIfGameOver();
    }

    public void DropCircle()
    {
        float Distance = Vector3.Distance(circle.transform.position, circleBlack.transform.position);
        if (Distance < distanceToObject)
        {
            circle.transform.position = circleBlack.transform.position;
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
            circleCorrect = true;
        }
        else
        {
            circle.transform.position = circleInitialPos;
            source.clip = incorrect;
            source.Play();
        }
        CheckIfGameOver();
    }

    public void CheckIfGameOver()
    {
        if(squarCorrect == true & circleCorrect == true)
        {
            squarCorrect = true;
        }
    }
}
