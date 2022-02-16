using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchItGameMode : MonoBehaviour
{
    public enum levelOfDifficulty { Level1, Level2, Level3, Level4, Level5 };
    public bool isGameTimed;
    public float gameTime;
    public int numberOfItems;
    public GameObject matchGridSPace;
    public GameObject prefabalist;
    public levelOfDifficulty difficultyLevel;
    private int gridColumn;
    private int gridRow = 2;

    // TODO: Get game diffculty from AuthUIManager system to streamline the diffculty of the level in buttons instead of coding
    
    private void SetGameDiffiuclty()
    {
        GridLayoutGroup glgl;
        glgl = matchGridSPace.GetComponent<GridLayoutGroup>();
        if (difficultyLevel == levelOfDifficulty.Level1)
        {

            glgl.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            isGameTimed = false;
            gameTime = 1f;
            gridColumn = 2;
            glgl.constraintCount = gridColumn;
        }
        if (difficultyLevel == levelOfDifficulty.Level2)
        {
            glgl.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            isGameTimed = true;
            gameTime = 180f;
            gridColumn = 2;
            glgl.constraintCount = gridColumn;
        }
        if (difficultyLevel == levelOfDifficulty.Level3)
        {
            glgl.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            isGameTimed = true;
            gameTime = 180f;
            gridColumn = 3;
            glgl.constraintCount = gridColumn;
        }
        if (difficultyLevel == levelOfDifficulty.Level4)
        {
            glgl.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            isGameTimed = true;
            gameTime = 180f;
            gridColumn = 4;
            glgl.constraintCount = gridColumn;
        }
        if (difficultyLevel == levelOfDifficulty.Level5)
        {
            glgl.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            isGameTimed = true;
            gameTime = 120f;
            gridColumn = 4;
            glgl.constraintCount = gridColumn;
        }
        // for (int i = 0; i < glgl.transform.childCount; i++)
        // {
        //     Destroy(glgl.transform.GetChild(i).gameObject);
        // }
        // PopulateGridLayout(glgl, gridColumn);
    }
    
    //* Code to autopupolate the grid, will need to update this in the todo lates
    // TODO: Populate grid via prefab, to be done at a later stage to add randmoness to the game

    // private void PopulateGridLayout(GridLayoutGroup glgl, int numberOfItems)
    // {
    //     // TODO: multiply with the number of rows to populate the grid
    //     // TODO: add the empty place holder prefab
    //     for (int i = 0; i < numberOfItems; i++)
    //     {
    //         Debug.Log("Created item {numberOfItems}");
    //         GameObject matchItem = (GameObject)Instantiate(prefabalist);
    //         matchItem.transform.SetParent(glgl.transform);

    //     }
    // }
    private void Awake()
    {
        SetGameDiffiuclty();
    }

}
