using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevels : MonoBehaviour
{
    public void OpenMatchItLevel1()
    {
        SceneManager.LoadScene("MtachItUp_Level_1");
    }
}
