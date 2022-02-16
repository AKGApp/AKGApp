using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }

    public void BackToLevelSelect()
    { 
        SceneManager.LoadScene("MVP_CooseGames");
    }
}
