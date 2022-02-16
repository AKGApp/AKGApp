using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchItSetGame : MonoBehaviour
{
    // Class created to pass values in between scencnes for the matchit game
    // these are effected 
    public static MatchItSetGame instance;

    public enum levelOfDifficulty { Level1, Level2, Level3, Level4, Level5 };
    public levelOfDifficulty difficulty;
    public int numberOfItems, numberOfItemsToMatch, constraintCount;
    public float gameTime, distanceToMatch;
    public bool isGameTimed, isTimerPaused;

     private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        
    }

}
