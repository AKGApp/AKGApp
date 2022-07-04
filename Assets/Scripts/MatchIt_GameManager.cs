using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MatchIt_MatchItGameManager : MonoBehaviour
{
    // Add a way to determine the difficulty of the level
    public enum levelOfDifficulty { Level1, Level2, Level3, Level4, Level5 };
    // Define the Level of difficulty enumerator
    public levelOfDifficulty setupDifficulty = levelOfDifficulty.Level1;
    // Varaible to specify distance to match items
    private float _distanceToMatch;
    // Specifiy the number of items in the item slots and items options to match in the game
    private int _numberOfItemsInMatchSlot, _numberOfItemsToMatchinOptions;                   
    // desc itemsInMatchSlot the layout group that contains all item patterns to be matched
    // desc itemsToMatchPanel a panel that contains all the options of the items to be matched in the slot group 
    public GameObject matchItItemsSlotGroup, itemsToMatchPanel;
    // The items that will be in the items to match panel
    private MatchItItem[] _matchItItems;
    // The items that will be in the Items Slot Group
    private MatchItSlot[] _matchItSlots;
    // A varaible that registers the dropped location of the item that is currently selected and dragged
    private Vector2 _selectedMatchItemDroppedLocation;

    public CountDownTimer gameTimer;

    // TODO: Create an item randomizer method
    // This creates a random item each time a game starts

    // TODO: setup the game based on awake based on the the difficuulty setting
    // Salmeen
    private void Awake() {
        GameSetup(setupDifficulty);  
    }

    // TODO: Method that takes the difficulty settings and applies the right options to it
    // Abdulmohsen
    private void GameSetup(levelOfDifficulty _levelOfDifficulty)
    {
        // if condition
        // switch statment
        if (_levelOfDifficulty==levelOfDifficulty.Level1)
        {
            // setup the game variables
        }
        switch (_levelOfDifficulty)
        {
            case levelOfDifficulty.Level1:
            {
                // setup the game varaibles
                break;
            }
        }
    }
}
