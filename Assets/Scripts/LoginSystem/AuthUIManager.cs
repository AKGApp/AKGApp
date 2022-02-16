using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthUIManager : MonoBehaviour
{
    public static AuthUIManager instance;

    [Header("References")]
    [SerializeField]
    private GameObject checkingForAccountUI;
    // [SerializeField]
    // private GameObject loginUI;
    // [SerializeField]
    // private GameObject signupUI;
    [SerializeField]
    private GameObject verifyEmailUI;
    [SerializeField]
    private Text verifyEmailText;
    [SerializeField]
    private GameObject GameSelectUI;
    [SerializeField]
    private GameObject UserDataUI;
    [SerializeField]
    private GameObject GameMenuUI;
    [SerializeField]
    private GameObject LogoutOfFirebaseButton;


    [Header("New Auth UI System")]
    [SerializeField]
    private GameObject TitleScreen;
    [SerializeField]
    private GameObject LoginScreen;
    [SerializeField]
    private GameObject SignupScreen;

    // [SerializeField]
    // private GameObject SignUpDetailsScreen;
    // [SerializeField]
    // private GameObject ParentInfoScreen;
    // [SerializeField]
    // private GameObject ChildInfoScreen;
    [SerializeField]
    private GameObject ChildInfoAddScreen;
    [SerializeField]
    private GameObject PaymentPage;
    [SerializeField]
    private GameObject ChooseYourGameScreen;
    [SerializeField]
    private GameObject UserOptionsScreen;
    [Space(10f)]

    [Header("Child Inforamtion Pages")]
    [SerializeField]
    private GameObject AddYourChildScreen;
    [SerializeField]
    private GameObject EnterChildInformationScreen;
    [SerializeField]
    private GameObject ChildrenContainer;
    private Toggle FatherToggle, MotherToggle;


    [SerializeField]
    private Button UserProfileButton;
    [SerializeField]
    private Button GameMenuButton;


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

    //TODO: Update the clearUI and Screen switch to new system

    public void ClearUI()
    {
        FirebaseManager.instance.ClearOutputs();
        // loginUI.SetActive(false);
        // signupUI.SetActive(false);
        verifyEmailUI.SetActive(false);
        checkingForAccountUI.SetActive(false);

        // New UI for AKG
        TitleScreen.SetActive(false);
        SignupScreen.SetActive(false);
        LoginScreen.SetActive(false);
        // SignUpDetailsScreen.SetActive(false);
        // ParentInfoScreen.SetActive(false);
        // ChildInfoScreen.SetActive(false);
        // ChildInfoAddScreen.SetActive(false);
        PaymentPage.SetActive(false);
        ChooseYourGameScreen.SetActive(false);
        AddYourChildScreen.SetActive(false);
        EnterChildInformationScreen.SetActive(false);
        // ChildInfoAddScreen.SetActive(false);
        UserOptionsScreen.SetActive(false);
    }

    //TODO: Update the clearUI and Screen switch to new system
    public void ShowTitleScreen()
    {
        ClearUI();
        TitleScreen.SetActive(true);
        // Old UI system
        // loginUI.SetActive(true);
    }
    public void ShowLoginScreen()
    {
        ClearUI();
        LoginScreen.SetActive(true);
        // Old UI system
        // loginUI.SetActive(true);
    }
    public void ShowSignUpScreen()
    {
        ClearUI();
        SignupScreen.SetActive(true);
        //signupUI.SetActive(true);
    }

    // public void ShowDetailsScreen()
    // {
    //     ClearUI();
    //     SignUpDetailsScreen.SetActive(true);
    //     //signupUI.SetActive(true);
    // }
    // public void ShowParentInfoTab()
    // {
    //     ClearUI();
    //     ParentInfoScreen.SetActive(true);
    // }
    public void ShowAddYourChildrenScreen()
    {
        ClearUI();
        AddYourChildScreen.SetActive(true);
    }
    public void ShowEnterChildInformationScreen()
    {
        ClearUI();
        EnterChildInformationScreen.SetActive(true);
    }
    public void AddChildTab()
    {
        ClearUI();
        AddYourChildScreen.SetActive(true);
    }
    // public void AddChildInfoTab()
    // {
    //     ClearUI();
    //     ChildInfoAddScreen.SetActive(true);
    // }
    // public void ChildInfoTab()
    // {
    //     ClearUI();
    //     ChildInfoScreen.SetActive(true);
    // }
    // public void ChildInfoAddTab()
    // {
    //     ClearUI();
    //     ChildInfoAddScreen.SetActive(true);
    // }
    public void ShowPaymentTab()
    {
        ClearUI();
        PaymentPage.SetActive(true);
    }

    public void ShowMainPlayScreen()
    {
        ClearUI();
        ChooseYourGameScreen.SetActive(true);
    }

    public void ShowUserOptionsScreen()
    {
        ClearUI();
        UserOptionsScreen.SetActive(true);
    }
    // TODO: Delete the coded between the comments after you stablize the system
    //! Delete this after we get the main menu system is done
    //! deleting it now causes the system to crash
    private void ClearGameMenuUI()
    {
        Debug.Log("User data UI name: " + UserDataUI.name);
        UserDataUI.SetActive(false);
        GameSelectUI.SetActive(false);
    }
    public void UserDataScreen()
    {
        ClearGameMenuUI();
        UserDataUI.SetActive(true);
    }
    public void GameSelectScreen()
    {
        ClearGameMenuUI();
        GameSelectUI.SetActive(true);
    }
    //! Delet to Here


    public void AwaitVerification(bool _emailSent, string _email, string _output)
    {
        ClearUI();
        verifyEmailUI.SetActive(true);
        if (_emailSent)
        {
            verifyEmailText.text = $"sent email!\nPlease Verify";
        }
        else
        {
            verifyEmailText.text = $"Email not sent {_output}\nPlease Verify {_email}";
        }
    }
    private void Start()
    {
        //*Yay the below line works, I think it is safe to delete the commented code below it
        SceneManager.activeSceneChanged += ChangedToAnotherScene;

        //! to be deleted if issue with change scenes is fixed
        //TODO: delete this after system is stablized
        //? I think this method should be kept somewhere else
        // Debug.Log("On start started");
        // Debug.Log(SceneManager.GetActiveScene().name);
        // if (SceneManager.GetActiveScene().name == "Game_Menu")
        // {
        //     if (GameSelectUI == null)
        //     {
        //         GameSelectUI = GameObject.Find("GameMenuUI");
        //     }
        //     if (UserDataUI == null)
        //     {
        //         UserDataUI = GameObject.Find("PlayerDataUI");
        //     }
        //     UpdatePlayerDataButton = GameObject.Find("Logout");

        //     Button updatePlayerInformation = UpdatePlayerDataButton.GetComponent<Button>();
        //     updatePlayerInformation.onClick.AddListener(() => SignoutButton());
        // }
    }

    public void AddChild()
    {
        Debug.Log("Add child button pressed");
        FirebaseManager.instance.AddChild();
    }


    private void ChangedToAnotherScene(Scene currentScene, Scene nextScene)
    {
        string currentSceneName = currentScene.name;
        Debug.Log("Scene Changed: " + currentSceneName);
        Debug.Log("AUIM New Scene Changed: " + nextScene.name);

        //* Assign GameSelect UI & UserData UI Reffrence
        if (nextScene.name == "Game_Menu")
        {
            Debug.Log("We are in the game menu");
            if (GameSelectUI == null)
            {
                GameSelectUI = GameObject.Find("GameMenuUI");
                Debug.Log(message: "We assigned the game select UI: " + GameSelectUI.name);
            }
            if (UserDataUI == null)
            {
                // UserDataUI = GameObject.Find("PlayerDataUI");
                foreach (GameObject go in Resources.FindObjectsOfTypeAll<GameObject>())
                {
                    if (go.name == "PlayerDataUI")
                    {
                        UserDataUI = go;
                    }
                    if (go.name == "GameMenuUI")
                    {
                        GameSelectUI = go;
                    }
                }
                Debug.Log(message: "We assigned the Player data UI: " + UserDataUI.name);
            }

            LogoutOfFirebaseButton = GameObject.Find("Logout");

            //* Link logout button to logout code from firebase
            Button LogoutButton = LogoutOfFirebaseButton.GetComponent<Button>();
            Debug.Log(message: "We assigned the Button: " + LogoutOfFirebaseButton.name);
            LogoutButton.onClick.AddListener(call: SignoutButton);

            //* Link logout button to logout code from firebase
            // UserProfileButton = GameObject.Find("MyProfile").GetComponent<Button>();
            Button SwitchToUserData = GameObject.Find("MyProfile").GetComponent<Button>();
            SwitchToUserData.onClick.AddListener(call: UserDataScreen);
            Button SwitchToGameMenu = GameObject.Find("GoToGames").GetComponent<Button>();
            SwitchToGameMenu.onClick.AddListener(call: GameSelectScreen);
        }
    }
    private void SignoutButton()
    {
        //* Finally this works
        Debug.Log("Signout button pressed");
        FirebaseManager.instance.SignoutFromFirebase();
    }
    /// <summary>
    /// Toggles the father parent icon and sets it up in firebase
    /// </summary>
    private void OnFatherToggle()
    {
        if (FatherToggle.isOn)
        {
            MotherToggle.isOn = false;
        }
        else
        {
            FatherToggle.isOn = true;
            FirebaseManager.instance.SetParentToFather();
        }
    }
    /// <summary>
    /// Toggles the mother parent icons and sets it up in firebase
    /// </summary>
    private void OnMotherToggle()
    {
        if (MotherToggle.isOn)
        {
            FatherToggle.isOn = false;
        }
        else
        {
            MotherToggle.isOn = true;
            FirebaseManager.instance.SetParentToMother();
        }
    }

}
