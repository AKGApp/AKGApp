using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("References")]
    [SerializeField]
    private GameObject GameSelectPanel;
    [SerializeField]
    private GameObject UserData;
    [SerializeField]
    // Start is called before the first frame update
    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

}
