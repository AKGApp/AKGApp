using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVP_UIManager : MonoBehaviour
{
    //TODO: Create the Constant instance
    public static MVP_UIManager instance;

    //TODO: Add the public Pages and Scenes List
    //* All first letters are capital in game reference
    [Header("Welcome & SignIn")]
    [SerializeField]
    private GameObject gameTitleScreen;
    [SerializeField]
    private GameObject signInScreen;

    [Space(10f)]
    [Header("Signup")]
    [SerializeField]
    private GameObject signUpScreens;
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

    [Space(10f)]
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
        signInScreen.SetActive(false);
        signUpScreens.SetActive(false);
        welcomeMessageScreen.SetActive(false);
        addUserInfoScreen.SetActive(false);
        chooseGuardianRoleScreen.SetActive(false);
        userAdditionalInfoScreen.SetActive(false);
        setYourGuardianPasswordScreen.SetActive(false);
        letsAddYourKidsScreen.SetActive(false);
        addChildrenScreens.SetActive(false);
        addChildrenScreen.SetActive(false);
        yourChildInformationScreen.SetActive(false);
        childAddedSuccessfullyMessageScreen.SetActive(false);
        userEntryScreen.SetActive(false);
        gameMenuScreen.SetActive(false);


    }

    //TODO: Write Methods to activate each scene
    public void ShowGameTitleScreen()
    {
        ClearUI();
        gameTitleScreen.SetActive(true);
    }

    public void ShowSignInScreen()
    {
        ClearUI();
        signInScreen.SetActive(true);
    }

    public void ShowSignUpScreens()
    {
        ClearUI();
        signUpScreens.SetActive(true);
    }

    public void ShowWelcomeMessageScreen()
    {
        ClearUI();
        welcomeMessageScreen.SetActive(true);
    }

    public void ShowAddUserInfoScreen()
    {
        ClearUI();
        addUserInfoScreen.SetActive(true);
    }

    public void ShowChooseGuardianRoleScreen()
    {
        ClearUI();
        chooseGuardianRoleScreen.SetActive(true);
    }

    public void ShowUserAdditionalInfoScreen()
    {
        ClearUI();
        userAdditionalInfoScreen.SetActive(true);
    }

    public void ShowSetYourGuardianPasswordScreen()
    {
        ClearUI();
        setYourGuardianPasswordScreen.SetActive(true);
    }

    public void ShowLetsAddYourKidsScreen()
    {
        ClearUI();
        letsAddYourKidsScreen.SetActive(true);
    }

    public void ShowAddChildrenScreens()
    {
        ClearUI();
        addChildrenScreens.SetActive(true);
    }

    public void ShowAddChildrenScreen()
    {
        ClearUI();
        addChildrenScreen.SetActive(true);
    }

    public void ShowYourChildInformationScreen()
    {
        ClearUI();
        yourChildInformationScreen.SetActive(true);
    }

    public void ShowChildAddedSuccessfullyMessageScreen()
    {
        ClearUI();
        childAddedSuccessfullyMessageScreen.SetActive(true);
    }

    public void ShowUserEntryScreen()
    {
        ClearUI();
        userEntryScreen.SetActive(true);
    }

    public void ShowGameMenuScreen()
    {
        ClearUI();
        gameMenuScreen.SetActive(true);
    }

}
