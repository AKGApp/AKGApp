using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Main_Menu_Navigation : MonoBehaviour
{
    public void GoToMVPMenu()
    {
        SceneManager.LoadScene("MVP_CooseGames");
    }

    public void QuitGame()
    {
        Debug.Log("Quite the game");
        Application.Quit();
    }
}
