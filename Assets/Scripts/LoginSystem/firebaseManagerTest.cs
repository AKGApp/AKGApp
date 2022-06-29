using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Firestore;
using TMPro;

//TODO: Link the parent information properly (username, email, contact information, etc)
//TODO: create the questioneer in the game
//TODO: cleanup child registration


public class firebaseManagerTest : MonoBehaviour
{
    #region Header Varaibales
    // This sets the Firabase instance which will be used for the game to connect 
    public static firebaseManagerTest instance;

    private List<ChildInfortmationDataClass> userChildren;

    private string parentDesignation;

    // This is used to reffrence UI and Input elements for the login system
    [Header("Firebase")]
    public FirebaseAuth auth;
    public FirebaseUser user;
    public DatabaseReference DBRefrence;
    [Space(10f)]

    // TODO: Delete the Login Refrence for the old system if the new system works
    //! Delete from here
    [Header("Login Reffrences")]
    [SerializeField]
    private TMP_InputField loginEmail;
    [SerializeField]
    private TMP_InputField loginPassword;
    [SerializeField]
    private TMP_Text loginOutputText;
    [Space(10f)]
    //! To here

    [Header("New Login Reffrences")]
    [SerializeField]
    private InputField UserLoginEmail;
    [SerializeField]
    private InputField UserLoginPasswrod;
    [SerializeField]
    // TODO: Create error feedback popup menu solution
    // For now text wil be displayed as plain text
    private Text UserLoginAttemptMessage;
    [Space(10f)]

    // TODO: Delete old login system headers after new system works
    //! From here
    [Header("Register References")]
    [SerializeField]
    private TMP_InputField registerUsername;
    [SerializeField]
    private TMP_InputField registerEmail;
    [SerializeField]
    private TMP_InputField registerPassword;
    [SerializeField]
    private TMP_InputField registerConfirmPassword;
    [SerializeField]
    private TMP_Text registerOutputText;
    [Space(10f)]
    //! To here

    [Header("New Signup References")]
    [SerializeField]
    private InputField UserSignupName;
    [SerializeField]
    private InputField UserSignupEmail;
    [SerializeField]
    private InputField UserSignupPassword;
    [SerializeField]
    private InputField UserSignupConfirmPassword;
    [SerializeField]
    private InputField UserLocation;
    [SerializeField]
    private InputField UserDateOfBirth_Day;
    [SerializeField]
    private InputField UserDateOfBirth_Month;
    [SerializeField]
    private InputField UserDateOfBirth_Year;
    [SerializeField]
    private InputField UserContact;
    [SerializeField]
    private Dropdown UserDesignation;
    [SerializeField]
    // TODO: Create error feedback popup menu solution
    // For now text wil be displayed as plain text
    private Text UserSignupAttemptMessage;
    [Space(10f)]

    [Header("Child Information")]
    [SerializeField]
    private InputField ChildNameField;
    [SerializeField]
    private InputField ChildDOBFieldDay;
    [SerializeField]
    private InputField ChildDOBFieldMonth;
    [SerializeField]
    private InputField ChildDOBFieldYear;
    [SerializeField]
    private Dropdown ChildGenderField;
    [Space(10f)]

    [Header("UI Manager")]
    [SerializeField]
    private AuthUIManager UIManagerReffrence;
    [Space(10f)]


    //TODO: Work on User Data after the information system has been updated
    [Header("User Data")]
    [SerializeField]
    private TMP_InputField userNameReffrence;
    [SerializeField]
    private TMP_InputField subscriptionTypeReffrence;
    [SerializeField]
    private TMP_InputField userDesignationReffrence;
    [SerializeField]
    private TMP_InputField userDateOfBirthReffrence;
    [SerializeField]
    private TMP_InputField userGenderReffrence;
    [SerializeField]
    public ChildInfortmationDataClass[] childData;

    // Execute this code when the system starts "Awake"
    #endregion
    #region Validators for the game
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
            instance = this;
        }
    }

    //* Defualt Start function of Unity, we start a coroutine to check for dependancies for the authentication 
    private void Start()
    {
        //? Code that executes to check if player loaded a new level (aka "Scene")
        //? Not sure why I added it here
        SceneManager.activeSceneChanged += ChangedToAnotherScene;
        StartCoroutine(CheckAndFixDependencies());
        userChildren = new List<ChildInfortmationDataClass>();

    }

    //* Tricky part here, basically checking if all firebase dependencies are available
    private IEnumerator CheckAndFixDependencies()
    {
        var CheckAndFixDependenciesTask = FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(predicate: () => CheckAndFixDependenciesTask.IsCompleted);
        var dependancyResault = CheckAndFixDependenciesTask.Result;
        if (dependancyResault == DependencyStatus.Available)
        {
            Debug.Log("Dependecy is available");
            InitializeFirebase();
        }
        else
        {
            Debug.LogError($"Could not resolve all Firebase dependancies: {dependancyResault}");
        }
    }

    // Initialize Firebase and check for login and Aoutherization changes
    private void InitializeFirebase()
    {
        Debug.Log("Initiialize Firebase");
        auth = FirebaseAuth.DefaultInstance;
        StartCoroutine(CheckAutoLogin());
        // Checks if the authentication state has changed, this means
        // it will check if a user logged out or a new one logged in
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
        // Reffrence the firebase database (might need to switch to Firestore later)
        //! Don't forget to fix security with the database
        DBRefrence = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Auto login method, uses coroutines and calls for Autologin on success (Basically allow the user to see the menu)
    private IEnumerator CheckAutoLogin()
    {
        yield return new WaitForEndOfFrame();
        if (user != null)
        {
            var reloadUserTask = user.ReloadAsync();
            yield return new WaitUntil(predicate: () => reloadUserTask.IsCompleted);
            if (user.IsEmailVerified)
            {
                AutoLogin();
                StartCoroutine(CheckIfUserHasChildren(user));
            }
            else
            {
                StartCoroutine(SendVerificationEmail());
            }
        }
        else
        {
            // We redirect the use to the login screen if the user has not logged in or is signed out
            AuthUIManager.instance.ShowLoginScreen();
        }
    }

    private void AutoLogin()
    {
        if (user != null)
        {
            //* Checks if ths users verify email address?
            //TODO: In firebase create an email template for the verfication
            if (user.IsEmailVerified)
            {
                // TODO: check if user has filled up child/s information
                StartCoroutine(CheckIfUserHasChildren(user));
            }
            else
            {
                //* If email was not verified then we resend the verification email
                //* this might be inefficant down the line as it will continue to ask for verification
                //TODO: Find a more efficiant way to resend email verification depending on user request, maybe send to a page wher user click a button
                StartCoroutine(SendVerificationEmail());
            }
        }
        else
        {
            // AuthUIManager.instance.ShowLoginScreen();
        }
    }

    /// <summary>
    /// Checks if user has any children
    /// </summary>
    /// <param name="user">The Firebase User</param>
    /// <returns></returns>
    private IEnumerator CheckIfUserHasChildren(FirebaseUser User)
    {
        var DBTask = DBRefrence.Child("users").Child(User.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        // check if the database connection faces any issues during the connection.
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Database: AKG App Failed to recive data with task {DBTask.Exception}, in getting user information");
        }
        // If we find the valuse but they are all empty we need to cleat all information in the user profile.
        else if (DBTask.Result.Value == null)
        {
            // clear data
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;
            Debug.Log("Snapshot has " + snapshot.ChildrenCount.ToString() + " children");
            if (snapshot.ChildrenCount > 8)
            {
                //TODO: If user has children puluate the userchildren class list
                //TODO: add method to populate children page
                AuthUIManager.instance.ShowMainPlayScreen();
            }
            else
            {
                // If the user has no children invoke the add children page
                AuthUIManager.instance.ShowAddYourChildrenScreen();
            }
        }
    }

    // Auto login method, uses coroutines and calls for Autologin on success (Basically allow the user to see the menu)
    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                // add signout code
                Debug.Log("Signed Out");
            }

            user = auth.CurrentUser;

            if (signedIn)
            {
                Debug.Log($"Signed in: {user.DisplayName}");
            }
        }
    }

    public void ClearOutputs()
    {
        loginOutputText.text = "";
        registerOutputText.text = "";
    }

    public void LoginButton()
    {
        //* I think the problem is because the loginemail & Passowrd text is not being reassigned
        //TODO: Fix auto assign in Auth UI Manager and Firebase Manger
        Debug.Log("Login Button pressed");

        StartCoroutine(LoginLogic(UserLoginEmail.text, UserLoginPasswrod.text));

        // StartCoroutine(LoginLogic(loginEmail.text, loginPassword.text));
    }

    public void RegisterButton()
    {
        StartCoroutine(RegistererLogic(UserSignupName.text, UserSignupEmail.text, UserSignupPassword.text, UserSignupConfirmPassword.text));
        // StartCoroutine(RegistererLogic(registerUsername.text, registerEmail.text, registerPassword.text, registerConfirmPassword.text));

    }

    public void SignoutFromFirebase()
    {
        Debug.Log("Signout from firebase");
        auth.SignOut();
        UIManagerReffrence.ShowTitleScreen();
        // GameManager.instance.ChangeScene(4);
    }

    public void UpdateUserInnformationButton()
    {
        // TODO: Update the courortine to update the user information "Name"
        // StartCoroutine(UpdateUserNameAuth(userNameReffrence.text));
        // StartCoroutine(UpdateUserNameDatabase(userNameReffrence.text));
    }

    //! Having an issue of login here, apparently the information is not being passed.
    //! Creditail is apparently a null reffence, maybe a firebase issue
    private IEnumerator LoginLogic(string _Email, string _Password)
    {
        Debug.Log(_Email);
        Debug.Log(_Password);

        Credential credential = EmailAuthProvider.GetCredential(_Email, _Password);
        var loginTask = auth.SignInWithCredentialAsync(credential);

        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            FirebaseException firebaseException = (FirebaseException)loginTask.Exception.GetBaseException();
            AuthError error = (AuthError)firebaseException.ErrorCode;
            string output = "Unkown error";
            switch (error)
            {
                case AuthError.MissingEmail:
                    output = "Please enter your email";
                    break;

                case AuthError.MissingPassword:
                    output = "Please enter your Password";
                    break;
                case AuthError.InvalidEmail:
                    output = "Invalid Email";
                    break;

                case AuthError.WrongPassword:
                    output = "Wrong Password";
                    break;
                case AuthError.UserNotFound:
                    output = "User does not exist";
                    break;
            }
            UserLoginAttemptMessage.text = output;
        }
        else
        {
            if (user.IsEmailVerified)
            {
                yield return new WaitForSeconds(1f);
                UIManagerReffrence.ShowMainPlayScreen();
                //! The code below changes to a new scene, this is from the old system, delete it if the new system
                //! works
                // GameManager.instance.ChangeScene(3);
            }
            else
            {
                StartCoroutine(SendVerificationEmail());
            }
        }
    }

    // Allows user to register their account, there is magic going on here and hated this part the most
    // I now remmber why I stopped programming, 
    private IEnumerator RegistererLogic(string _UserName, string _Email, string _Password, string _ConfrimPassword)
    {
        // AndroidGeoLocation location = new AndroidGeoLocation();
        // string userLocation = location.GetGeoLocation();
        if (_UserName == "")
        {
            registerOutputText.text = "Please enter your username";

        }
        else if (_UserName.ToLower() == "Bad word")
        {
            registerOutputText.text = "That username is inappropriate!";
        }
        else if (_Password != _ConfrimPassword)
        {
            registerOutputText.text = "Passwords do not match!";
        }
        else
        {
            // // //TODO: Link user profile properly and update username with authnticator
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(_Email, _Password);
            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);
            if (registerTask.Exception != null)
            {
                user.DeleteAsync();
                FirebaseException firebaseException = (FirebaseException)registerTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;
                string output = "Unkown error";
                switch (error)
                {
                    case AuthError.Cancelled:
                        output = "Update user canceled";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        output = "Email has already been used";
                        break;
                    case AuthError.SessionExpired:
                        output = "Session Expired";
                        break;
                }
                loginOutputText.text = output;
            }
            else
            {
                UserProfile profile = new UserProfile
                {
                    DisplayName = _UserName
                    //TODO: give profile a default photo
                };
                user.UpdateUserProfileAsync(profile);
                var defaultUserTask = user.UpdateUserProfileAsync(profile);
                yield return new WaitUntil(predicate: () => defaultUserTask.IsCompleted);
                StartCoroutine(UpdateUserNameAuth(_UserName));

                if (defaultUserTask.Exception != null)
                {
                    FirebaseException firebaseException = (FirebaseException)defaultUserTask.Exception.GetBaseException();
                    AuthError error = (AuthError)firebaseException.ErrorCode;
                    string output = "Unkown error";
                    switch (error)
                    {
                        case AuthError.InvalidEmail:
                            output = "The Email provided was Invalid";
                            break;
                        case AuthError.EmailAlreadyInUse:
                            output = "This email has already been registred";
                            break;
                        case AuthError.WeakPassword:
                            output = "Weak Password";
                            break;
                        case AuthError.MissingPassword:
                            output = "Missing password";
                            break;
                    }
                    loginOutputText.text = output;
                }
                else
                {
                    Debug.Log($"Firebase User created succesfully: {user.DisplayName} ({user.UserId})");
                    loginOutputText.text = "user created";
                    StartCoroutine(UpdateUserProfile(user, _UserName, GetUserDateOfBirth(UserDateOfBirth_Day.text, UserDateOfBirth_Month.text, UserDateOfBirth_Year.text), GetUserDesignation(UserDesignation), _Email, UserContact.text, UserLocation.text));
                    StartCoroutine(SendVerificationEmail());
                }
            }
        }
    }

    private IEnumerator SendVerificationEmail()
    {
        if (user != null)
        {
            var emailTask = user.SendEmailVerificationAsync();
            yield return new WaitUntil(predicate: () => emailTask.IsCompleted);

            if (emailTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)emailTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;

                string output = "Unknown Error, Try Again!";

                switch (error)
                {
                    case AuthError.Cancelled:
                        output = "verification task was cancelled";
                        break;

                    case AuthError.InvalidRecipientEmail:
                        output = "Invalid Email";
                        break;
                    case AuthError.TooManyRequests:
                        output = "Too many requests";
                        break;
                }

                AuthUIManager.instance.AwaitVerification(false, user.Email, output);
            }
            else
            {
                AuthUIManager.instance.AwaitVerification(true, user.Email, null);
                Debug.Log("Email sent successfully");
            }

        }
    }

    #endregion
    private IEnumerator UpdateUserNameAuth(string _UserName)
    {
        // Create user profile and set the user name
        UserProfile profile = new UserProfile { DisplayName = _UserName };

        // Call the firebase auth update user profile fundtion passing the profile with the user name
        var ProfileTask = user.UpdateUserProfileAsync(profile);
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        // Check if profile update worked and if not catch the error
        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"User Profile: AKG App Failed to register with task {ProfileTask.Exception}, in updating user name in profile");
        }
        else
        {
            // User profile name updated. we can add extra functionality such as a notice message that the name has been updated
        }
    }

    private IEnumerator UpdateUserNameDatabase(string _UserName)
    {

        // Call the firebase auth update user profile fundtion passing the profile with the user name
        var DBTask = DBRefrence.Child("users").Child(user.UserId).Child("username").SetValueAsync(_UserName);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        // Check if profile update worked and if not catch the error
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Database: AKG App Failed to register with task {DBTask.Exception}, in updating user name in profile");
        }
        else
        {
            // User database username updated. we can add extra functionality such as a notice message that the name has been updated
        }
    }
    private IEnumerator UpdateUserGenderDatabase(string _Gender)
    {
        // Call the firebase auth update user profile fundtion passing the profile with the user name
        var DBTask = DBRefrence.Child("users").Child(user.UserId).Child("gender").SetValueAsync(_Gender);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        // Check if profile update worked and if not catch the error
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Database: AKG App Failed to register with task {DBTask.Exception}, in updating user name in profile");
        }
        else
        {
            // User database username updated. we can add extra functionality such as a notice message that the name has been updated
        }
    }
    private IEnumerator UpdateUserAgeDatabase(string _Age)
    {
        // Call the firebase auth update user profile fundtion passing the profile with the user name
        var DBTask = DBRefrence.Child("users").Child(user.UserId).Child("age").SetValueAsync(_Age);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        // Check if profile update worked and if not catch the error
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Database: AKG App Failed to register with task {DBTask.Exception}, in updating user name in profile");
        }
        else
        {
            // User database username updated. we can add extra functionality such as a notice message that the name has been updated
        }
    }

    private IEnumerator UpdateUserSubscriptionTypeDatabase(string _SubscriptionType)
    {
        // Call the firebase auth update user profile fundtion passing the profile with the user name
        var DBTask = DBRefrence.Child("users").Child(user.UserId).Child("Subscription type").SetValueAsync(_SubscriptionType);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        // Check if profile update worked and if not catch the error
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Database: AKG App Failed to register with task {DBTask.Exception}, in updating user name in profile");
        }
        else
        {
            // User database username updated. we can add extra functionality such as a notice message that the name has been updated
        }
    }

    private IEnumerator UpdateUserUserDesignationDatabase(string _UserDesignation)
    {
        // Call the firebase auth update user profile fundtion passing the profile with the user name
        var DBTask = DBRefrence.Child("users").Child(user.UserId).Child("User designation").SetValueAsync(_UserDesignation);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        // Check if profile update worked and if not catch the error
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Database: AKG App Failed to register with task {DBTask.Exception}, in updating user name in profile");
        }
        else
        {
            // User database username updated. we can add extra functionality such as a notice message that the name has been updated
        }
    }

    private IEnumerator updateParentInformation(string _UserDesignation)
    {
        // Call the firebase auth update user profile fundtion passing the profile with the user name
        var DBTask = DBRefrence.Child("users").Child(user.UserId).Child("User designation").SetValueAsync(_UserDesignation);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        // Check if profile update worked and if not catch the error
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Database: AKG App Failed to register with task {DBTask.Exception}, in updating user name in profile");
        }
        else
        {
            // User database username updated. we can add extra functionality such as a notice message that the name has been updated
        }
    }

    /// <summary>
    /// Add game data to player
    /// </summary>
    public IEnumerator UpdateMatchItPerfomance(string gameName, string diffculty, string userChild, string startDateTime, string endDateTime, string geoLocation, float score, int failedAttempt)
    {
        string key = DBRefrence.Child("game_resualts_log").Child("matchitup").Child(user.UserId).Push().Key;
        MatchItLogData entry = new MatchItLogData(key, gameName, diffculty, user.UserId, userChild, startDateTime, endDateTime, geoLocation, score, failedAttempt);
        string json = JsonUtility.ToJson(entry);
        // Call the firebase auth update user profile fundtion passing the profile with the user name
        var DBTask = DBRefrence.Child("game_resualts_log").Child("matchitup").Child(user.UserId).Child(key).SetRawJsonValueAsync(json);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        // Check if profile update worked and if not catch the error
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Database: AKG App Failed to register with task {DBTask.Exception}, in updating user name in profile");
        }
        else
        {
            // User database username updated. we can add extra functionality such as a notice message that the name has been updated
        }
    }

    public IEnumerator UpdateUserProfile(FirebaseUser userID, string userName, string dateOfBirth, string paretDesignation, string parentEmail, string parentNumber, string userLocation)
    {
        UserParentInformation entry = new UserParentInformation(userID.UserId, userName, DateTime.UtcNow.ToString(), userLocation, dateOfBirth, paretDesignation, parentEmail, parentNumber);
        string json = JsonUtility.ToJson(entry);
        // Call the firebase auth update user profile fundtion passing the profile with the user name
        var DBTask = DBRefrence.Child("users").Child(user.UserId).SetRawJsonValueAsync(json);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        StartCoroutine(CreateChildrenReffrenceHolder(userID));
        // Check if profile update worked and if not catch the error
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Database: AKG App Failed to register with task {DBTask.Exception}, in updating user name in profile");
        }
        else
        {
            // User database username updated. we can add extra functionality such as a notice message that the name has been updated
        }
    }
    public IEnumerator CreateChildrenReffrenceHolder(FirebaseUser User)
    {
        AddChildrenToUsersDataClass entery = new AddChildrenToUsersDataClass("", "", "");
        string json = JsonUtility.ToJson(entery);
        var DBTask = DBRefrence.Child("usersChildren").Child(user.UserId).SetRawJsonValueAsync(json);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Database: AKG App Failed to register with task {DBTask.Exception}, in CreateChildrenReffrenceHolder() method.");
        }
        else
        {
        }
    }
    public IEnumerator CheckIfUserHasKids(FirebaseUser User)
    {
        Debug.Log("Children reffrence holder checked");

        // Check if a user has any registred children
        var DBTask = DBRefrence.Child("usersChildren").Child(user.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        if (DBTask.Exception != null)
        {
            Debug.Log("Exception");

            Debug.LogWarning(message: $"Database: AKG App Failed to recive data with task {DBTask.Exception}, in getting user information");
        }
        else if (DBTask.Result.Value == null)
        {
            Debug.Log("null value");

            StartCoroutine(CreateChildrenReffrenceHolder(User));
        }
        else
        {
            Debug.Log("snapshot");
            DataSnapshot snapshot = DBTask.Result;
            int i = 0;
            foreach (DataSnapshot data in snapshot.Children)
            {
                Debug.Log(data.GetValue(true));
                if (!string.IsNullOrEmpty(data.Value.ToString()))
                {
                    Debug.Log("Hello");
                    i++;
                }
            }
            string _childGender = GetChildDesignation(ChildGenderField);
            if (i > 2)
            {
                // User has capped his child limit and cannot add more}
            }
            else if (i <= 2)
            {
                switch (i)
                {
                    case 0:
                        StartCoroutine(UpdateChildInformation(ChildNameField.text, user, GetUserDateOfBirth(ChildDOBFieldDay.text, ChildDOBFieldMonth.text, ChildDOBFieldYear.text), _childGender, "", "firstChildID"));
                        break;
                    case 1:
                        StartCoroutine(UpdateChildInformation(ChildNameField.text, user, GetUserDateOfBirth(ChildDOBFieldDay.text, ChildDOBFieldMonth.text, ChildDOBFieldYear.text), _childGender, "", "secondChildID"));

                        break;
                    case 2:
                        StartCoroutine(UpdateChildInformation(ChildNameField.text, user, GetUserDateOfBirth(ChildDOBFieldDay.text, ChildDOBFieldMonth.text, ChildDOBFieldYear.text), _childGender, "", "thirdChildID"));
                        break;
                }
            }
        }
    }


    public IEnumerator UpdateChildInformation(string childName, FirebaseUser userID, string childDOB, string childGender, string childImage, string childNumber)
    {

        //TODO: check if user has existing children first
        string childID = DBRefrence.Child("users").Child(userID.UserId).Push().Key;
        ChildInfortmationDataClass entry = new ChildInfortmationDataClass(childName, childID, userID.UserId, childDOB, DateTime.UtcNow.ToString(), childGender);
        string json = JsonUtility.ToJson(entry);
        // Call the firebase auth update user profile fundtion passing the profile with the user name
        var DBTask = DBRefrence.Child("users").Child(user.UserId).Child(childID).SetRawJsonValueAsync(json);
        StartCoroutine(AddChildReffrence(userID, childNumber, childID));
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        // Check if profile update worked and if not catch the error
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Database: AKG App Failed to register with task {DBTask.Exception}, in updating user name in profile");
        }
        else
        {
            // Lets do something here once the information has been added
            // Getback to the child add screen
            AuthUIManager.instance.ShowAddYourChildrenScreen();
        }
    }
    public IEnumerator AddChildReffrence(FirebaseUser User, string ChildPosition, string childID)
    {
        var DBTask = DBRefrence.Child("usersChildren").Child(user.UserId).Child(ChildPosition).SetValueAsync(childID);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Database: AKG App Failed to register with task {DBTask.Exception}, in updating user name in profile");
        }
        else
        {
        }
    }
    // public IEnumerator AddChildToUser(FirebaseUser User, string childNumber, string childID)
    // {
    //     AddChildrenToUsersDataClass entery = new AddChildrenToUsersDataClass(ChildID);
    //     string json = JsonUtility.ToJson(entery);
    //     var DBTask = DBRefrence.Child("usersChildren").Child(user.UserId).SetRawJsonValueAsync(json);
    //     yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
    //     // Check if profile update worked and if not catch the error
    //     if (DBTask.Exception != null)
    //     {
    //         Debug.LogWarning(message: $"Database: AKG App Failed to register with task {DBTask.Exception}, in updating user name in profile");
    //     }
    //     else
    //     {

    //     }
    // }




    //TODO:AddMethods to remove a child from the users and userchildren database


    // public IEnumerator UpdateParentDesignation(string )
    // {
    //     AndroidGeoLocation geoLocation = new AndroidGeoLocation();
    //     geoLocation.GetGeoLocation();
    //     UserParentInformation entry = new UserParentInformation(user.UserId, ));
    //     string json = JsonUtility.ToJson(entry);
    //     var DBTask = DBRefrence.Child("users").Child(user.UserId).SetRawJsonValueAsync(json);
    //     yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
    //     if (DBTask.Exception != null)
    //     {
    //         Debug.LogWarning(message: $"Database: AKG App Failed to register with task {DBTask.Exception}, in updating user name in profile");
    //     }
    //     else
    //     {
    //         // What do we want to do on success
    //     }
    // }

    private IEnumerator LoadUserData()
    {
        var DBTask = DBRefrence.Child("users").Child(user.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        // check if the database connection faces any issues during the connection.
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Database: AKG App Failed to recive data with task {DBTask.Exception}, in getting user information");
        }
        // If we find the valuse but they are all empty we need to cleat all information in the user profile.
        else if (DBTask.Result.Value == null)
        {
            // clear data
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            // The data we want to populate
            // i.e. username.text = snapshot.Child("name").Value.ToString();
        }
    }




    private IEnumerator GetChildInfroamtion()
    {
        var DBTask = DBRefrence.Child("users").Child(user.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        // check if the database connection faces any issues during the connection.
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Database: AKG App Failed to recive data with task {DBTask.Exception}, in getting user information");
        }
        // If we find the valuse but they are all empty we need to clean all information in the user profile.
        else if (DBTask.Result.Value == null)
        {
            // clear data
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;
            ChildInfortmationDataClass info = new ChildInfortmationDataClass();
            foreach (var children in snapshot.Children)
            {
                info.childName = children.Child("childName").Value.ToString();
                info.childID = children.Child("childID").Value.ToString();
                info.childParentID = children.Child("childParentID").Value.ToString();
                info.childDOB = children.Child("childDOB").Value.ToString();
                info.childEnnroledDate = children.Child("ChildEnroledDate").Value.ToString();
                info.childGender = children.Child("ChildGender").Value.ToString();

                userChildren.Add(info);
            }
            //snapshot.Child("childName").Value.ToString();
            // The data we want to populate
            // i.e. username.text = snapshot.Child("name").Value.ToString();
        }
    }

    private void ChangedToAnotherScene(Scene currentScene, Scene nextScene)
    {
        string currentSceneName = currentScene.name;
        Debug.Log("Scene Changed: " + currentSceneName);
        Debug.Log("FBM New Scene Changed: " + nextScene.name);

        //* Assign GameSelect UI & UserData UI Reffrence
        if (nextScene.name == "MainMenu_Login")
        {
            Debug.Log("We are in the game menu");
            if (loginEmail == null)
            {
                Debug.Log("We assigned game select");
                loginEmail = GameObject.Find("User Email").GetComponent<TMP_InputField>();
                Debug.Log(message: "We assigned the game select UI: " + loginEmail.text);
            }
            if (loginPassword == null)
            {
                Debug.Log("We assigned game select");
                loginPassword = GameObject.Find("Password").GetComponent<TMP_InputField>();
                Debug.Log(message: "We assigned the game select UI: " + loginPassword.text);
            }
        }

        // Link User profile information to the Firebase manaager
        if (nextScene.name == "Game_Menu")
        {
            // TODO: Enable this after you fix the UI Navigation problem

            // Debug.Log("Getting the User Profile information");
            // if (userNameReffrence == null)
            // {
            //     userNameReffrence = GameObject.Find("Name").GetComponent<TMP_InputField>();
            // }
            // if (subscriptionTypeReffrence == null)
            // {
            //     subscriptionTypeReffrence = GameObject.Find("Subscription").GetComponent<TMP_InputField>();
            // }
            // if (userDesignationReffrence == null)
            // {
            //     userDesignationReffrence = GameObject.Find("Designation").GetComponent<TMP_InputField>();
            // }
            // if (userDateOfBirthReffrence == null)
            // {
            //     userDateOfBirthReffrence = GameObject.Find("Age").GetComponent<TMP_InputField>();
            // }
            // if (userGenderReffrence == null)
            // {
            //     userGenderReffrence = GameObject.Find("Gender").GetComponent<TMP_InputField>();
            // }
        }
    }
    // private IEnumerator PlayerGameResaults(string _date, string _userName, string _childIDName, string _gameName, string _gameScore, string _gamePlayTime)
    // {
    //     var DBTask = DBRefrence.Child("users").Child(user.UserId).GetValueAsync();
    //     yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

    //     // check if the database connection faces any issues during the connection.
    //     if (DBTask.Exception != null)
    //     {
    //         Debug.LogWarning(message: $"Database: AKG App Failed to recive data with task {DBTask.Exception}, in getting user information");
    //     }
    //     // If we find the valuse but they are all empty we need to cleat all information in the user profile.
    //     else if (DBTask.Result.Value == null)
    //     {
    //         // clear data
    //     }
    //     else
    //     {
    //         DataSnapshot snapshot = DBTask.Result;

    //         // The data we want to populate
    //         // i.e. username.text = snapshot.Child("name").Value.ToString();
    //     }
    // }
    // public bool CheckIfChildrenExist(FirebaseUser User)
    // {
    //     bool DoesUserHaveChildren = false;
    //     var DBTask = DBRefrence.Child("users").Child(User.UserId).GetValueAsync();
    //     // check if the database connection faces any issues during the connection.
    //     if (DBTask.Exception != null)
    //     {
    //         Debug.LogWarning(message: $"Database: AKG App Failed to recive data with task {DBTask.Exception}, in getting user information");
    //     }
    //     // If we find the valuse but they are all empty we need to cleat all information in the user profile.
    //     else if (DBTask.Result.Value == null)
    //     {
    //         // clear data
    //     }
    //     else
    //     {
    //         DataSnapshot snapshot = DBTask.Result;
    //         if (snapshot.HasChildren)
    //         {
    //             //If user has children puluate the userchildren class list
    //             DoesUserHaveChildren = true;
    //         }
    //         else
    //         {
    //             // If the user has no children invoke the add children page
    //             DoesUserHaveChildren = false;
    //         }
    //     }
    //     return DoesUserHaveChildren;
    // }

    public void SetParentToFather()
    {
        parentDesignation = "Father";
        // StartCoroutine(UpdateParentDesignation(parentDesignation));
    }
    public void SetParentToMother()
    {
        parentDesignation = "Mother";
        // StartCoroutine(UpdateParentDesignation(parentDesignation));
    }
    public string GetUserDateOfBirth(string _day, string _month, string _year)
    {
        string _dateOfBirth;
        _dateOfBirth = _year + "-" + _month + "-" + _day + "T00:00:00Z";
        return _dateOfBirth;
    }
    public string GetUserDesignation(Dropdown _userDesigantion)
    {
        Dropdown ddl = _userDesigantion;
        string designation = "";
        if (ddl.options[ddl.value].text == "الأب")
        {
            designation = "Father";
        }
        if (ddl.options[ddl.value].text == "الأم")
        {
            designation = "Mother";
        }
        return designation;
    }

    public string GetChildDesignation(Dropdown _childDesigantion)
    {
        Dropdown ddl = _childDesigantion;
        string designation = "";
        if (ddl.options[ddl.value].text == "الأبن")
        {
            designation = "Son";
        }
        if (ddl.options[ddl.value].text == "البنت")
        {
            designation = "Daughter";
        }
        return designation;
    }

    internal void AddChild()
    {
        StartCoroutine(CheckIfUserHasKids(user));
    }
}
