using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVP_UIManager : MonoBehaviour
{
    //TODO: Create the Constant instance
    public static MVP_UIManager instance;

    //TODO: Add the public Pages and Scenes List
    //* All first letters are capital in game reference
    [Header("Welcome & Signup")]
    [SerializeField]
    private GameObject gameTitleScreen;
    [SerializeField]
    private GameObject signInScreen;
    [SerializeField]
    private GameObject signUpScreens;

    [Space(10f)]
    [Header("Signup")]
    [SerializeField]
    private GameObject welcomeMessageScreen;
    [SerializeField]
    private GameObject addUserInfoScreen;
    [SerializeField]
    private GameObject chooseGuardianRoleScreen;
    [SerializeField]
    private GameObject userAdditionalInfoScreen;
    [SerializeField]
    private GameObject setYourGuardianPasswordScreen;
    [SerializeField]
    private GameObject letsAddYourKidsScreen; //the (k) in kids is small caps in game reference 

    [Space(10f)]
    [Header("Add Children")]
    [SerializeField]
    private GameObject addChildrenScreens;
    [SerializeField]
    private GameObject addChildrenScreen;
    [SerializeField]
    private GameObject yourChildInformationScreen; //the (r) in your isn't typed in game reference
    [SerializeField]
    private GameObject childAddedSuccessfullyMessageScreen; // spelling error in word successfully
    [SerializeField]

    private GameObject userEntryScreen; // spelling error in the word Entry
    [SerializeField]
    private GameObject gameMenuScreen;






    //TODO: Add the UI componenets Needed
    



    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void ClearUI()
    {
        //TODO: Set All UI elements visibility to false
        gameTitleScreen.SetActive(false);

    }

    //TODO: Write Methods to activate each scene
    public void ShowGameTitleScreen()
    {
        ClearUI();
        gameTitleScreen.SetActive(true);
    }


}
