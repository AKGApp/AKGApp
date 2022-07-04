using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MatchItGameManager : MonoBehaviour
{
    public enum levelOfDifficulty { Level1, Level2, Level3, Level4, Level5 };
    [Header("Game Settings")]
    public levelOfDifficulty _levelOfDifficulty;
    public bool _isGameTimed;
    public float _gameTime = 10.0f, _distanceToMatch = 30.0f;
    public int _numberOfItems, _numberOfItemsToMatch;
    [Header("Gameplay Reffrences")]
    public CountDownTimer _timerScript;
    public GameObject _slotsLayoutGroup;
    public GameObject _winUIScreen;
    public GameObject _loseGameScreen;
    public GameObject _quiteGameScreen;
    public GameObject _itemsToMatchPanel;
    public TMP_Text _scoreBoard;

    [Header("Game Items")]
    public MatchItItem[] _matchItItems;
    public MatchItSlot[] _matchItSlots;
    private GridLayoutGroup _gridLayoutGroup;
    private Vector2 _selectedMatchItemDroppedLocation;
    private int _itemsMatched = 0;

    private void MatchItItemsRandomizer(int NumberOfItems)
    {
        // create class the matches the Item infromation list
        return;
    }
    private void Awake()
    {
        GameSetup(_levelOfDifficulty, _timerScript);
        MatchItItemsRandomizer(_numberOfItems);
        //! Score not updated
        _scoreBoard.text = SetScore(_numberOfItemsToMatch, _itemsMatched);
        //* add the ranmoziser function here
    }

    // TODO: Create the randomizer function for the game
    //* takes a prefab and pupulates it from a list based on the naming convetion.
    //* naming convention goes like this "Color Type Catagory Name"
    //* naming convetion function extracts the needed variables and popluates them in the Item information

    // Function to check whether the dropped item is matched based on the distance of the object
    public void DroppedItem()
    {
        Debug.Log("Plop");
        foreach (MatchItItem i in _matchItItems)
        {
            foreach (MatchItSlot s in _matchItSlots)
            {
                // Debug.Log(i.GetComponent<MatchItInformation>().itemName.ToString());
                if (i.GetComponent<MatchItInformation>().itemName.ToString() == s.GetComponent<MatchItInformation>().itemName.ToString())
                {
                    float _distance = Vector3.Distance(i.transform.position, s.transform.position);
                    Debug.Log(i.transform.position);
                    Debug.Log(s.transform.position);
                    Debug.Log(_distance);
                    if (_distance <= _distanceToMatch)
                    {
                        Debug.Log("Pleep");
                        Debug.Log("Matched");
                        //TODO: Play win sound
                        s.GetComponent<Image>().sprite = i.GetComponent<Image>().sprite;
                        s.GetComponent<Image>().color = i.GetComponent<Image>().color;
                        i._isMatched = true;
                        s._isMatched = true;
                    }
                }
            }
        }
        _scoreBoard.text = SetScore(_numberOfItemsToMatch, _itemsMatched);
        if (Array.TrueForAll(_matchItSlots, CheckIfAllItemsMatched))
        {
            StopTimer(ref _timerScript);
            FirebaseManager.instance.StartCoroutine(FirebaseManager.instance.UpdateMatchItPerfomance("Match It", _levelOfDifficulty.ToString(), "Mohammad", DateTime.Now.ToString(), DateTime.Now.ToString(), "Location", 0.82f, 1));
            Invoke("EnableWinningScreen", 1.0f);
        }
    }
    private string SetScore(int NumberOfItemsToMatch, int ItemsMatched)
    {
        string newScore = ItemsMatched.ToString() + "/" + NumberOfItemsToMatch.ToString();
        Debug.Log(newScore);
        return newScore;
    }
    private static bool CheckIfAllItemsMatched(MatchItSlot value)
    {
        return value._isMatched;
        throw new NotImplementedException();
    }
    public static void StopTimer(ref CountDownTimer  Timer)
    {
        Timer.isTimerPaused=true;
    }

    private void EnableWinningScreen()
    {
        _timerScript.isTimerPaused = true;
        _winUIScreen.SetActive(true);
    }

    // Editing of the code below warrents the fury of the orginal developer, if you occur his wrath you will lose fingers
    private void GameSetup(levelOfDifficulty SetupDifficulty, CountDownTimer Timer)
    {
        _gridLayoutGroup = _slotsLayoutGroup.GetComponent<GridLayoutGroup>();
        if (SetupDifficulty == levelOfDifficulty.Level1)
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _isGameTimed = false;
            Timer.isTimerActive = _isGameTimed;
            _gameTime = 999.0f;
            _distanceToMatch = 30.0f;
            Timer.timerValue = _gameTime;
            _numberOfItems = 4;
            _numberOfItemsToMatch = 1;
            _gridLayoutGroup.constraintCount = 2;
        }
        if (SetupDifficulty == levelOfDifficulty.Level2)
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _isGameTimed = true;
            Timer.isTimerActive = _isGameTimed;
            _gameTime = 180.0f;
            _distanceToMatch = 30.0f;
            Timer.timerValue = _gameTime;
            _numberOfItems = 6;
            _numberOfItemsToMatch = 2;
            _gridLayoutGroup.constraintCount = 3;
        }
        if (SetupDifficulty == levelOfDifficulty.Level3)
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _isGameTimed = true;
            Timer.isTimerActive = _isGameTimed;
            _gameTime = 120.0f;
            _distanceToMatch = 30.0f;
            Timer.timerValue = _gameTime;
            _numberOfItems = 6;
            _numberOfItemsToMatch = 3;
            _gridLayoutGroup.constraintCount = 3;
        }
        if (SetupDifficulty == levelOfDifficulty.Level4)
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _isGameTimed = true;
            Timer.isTimerActive = _isGameTimed;
            _gameTime = 180.0f;
            _distanceToMatch = 30.0f;
            Timer.timerValue = _gameTime;
            _numberOfItems = 8;
            _numberOfItemsToMatch = 4;
            _gridLayoutGroup.constraintCount = 4;
        }
        if (SetupDifficulty == levelOfDifficulty.Level5)
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _isGameTimed = true;
            Timer.isTimerActive = _isGameTimed;
            _gameTime = 120f;
            _distanceToMatch = 30.0f;
            Timer.timerValue = _gameTime;
            _numberOfItems = 12;
            _numberOfItemsToMatch = 4;
            _gridLayoutGroup.constraintCount = 4;
        }
    }
}
