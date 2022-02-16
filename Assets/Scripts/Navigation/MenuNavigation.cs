using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    public GameObject matchItUpLevels, puzzleItUpLevels, sortItLevels, levelSelect;
    public MatchItSetGame.levelOfDifficulty difficulty;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);

    }

    public void ReloadGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    private void ClearGamesUI()
    {
        matchItUpLevels.SetActive(false);
        puzzleItUpLevels.SetActive(false);
        sortItLevels.SetActive(false);
        levelSelect.SetActive(false);
    }
    public void ShowMatchItUpLevels()
    {
        ClearGamesUI();
        AuthUIManager.instance.ClearUI();
        levelSelect.SetActive(true);
        matchItUpLevels.SetActive(true);
    }
    public void ShowPuzzleItUpLevels()
    {
        ClearGamesUI();
        AuthUIManager.instance.ClearUI();
        levelSelect.SetActive(true);
        puzzleItUpLevels.SetActive(true);
    }
    public void ShowSortItLevels()
    {
        ClearGamesUI();
        AuthUIManager.instance.ClearUI();
        levelSelect.SetActive(true);
        sortItLevels.SetActive(true);
    }
    public void ChooseYourGameScreen()
    {
        ClearGamesUI();
        AuthUIManager.instance.ShowMainPlayScreen();
    }
    public void MatchItLevelMVP()
    {
        SceneManager.LoadScene(1);
    }
    public void MatchItLevel1()
    {
        difficulty = MatchItSetGame.levelOfDifficulty.Level1;
    }
    public void MatchItLevel2()
    {
        difficulty = MatchItSetGame.levelOfDifficulty.Level2;
    }
    public void MatchItLevel3()
    {
        difficulty = MatchItSetGame.levelOfDifficulty.Level3;
    }
    public void MatchItLevel4()
    {
        difficulty = MatchItSetGame.levelOfDifficulty.Level4;
    }
    public void MatchItLevel5()
    {
        difficulty = MatchItSetGame.levelOfDifficulty.Level5;
    }

}
