using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public const int levelOneGrids = 20;
    public const int levelOneMoves = 64;
    public const int levelOneHints = 3;

    public const int levelTwoGrids = 20;
    public const int levelTwoMoves = 54;
    public const int levelTwoHints = 3;

    public const int levelThreeGrids = 20;
    public const int levelThreeMoves = 50;
    public const int levelThreeHints = 2;

    public const int levelFourGrids = 24;
    public const int levelFourMoves = 54;
    public const int levelFourHints = 2;

    public const int levelFiveGrids = 24;
    public const int levelFiveMoves = 50;
    public const int levelFiveHints = 1;

    public static bool levelOne, levelTwo, levelThree, levelFour, levelFive;

    [SerializeField]
    private GameObject mainMenuPanel, levelsPanel, creditsPanel;


    void Awake(){
        levelOne = levelTwo = levelThree = levelFour = levelFive = false;
   }
   public void quit(){
       Application.Quit();
   }

    public void BackToMenu()
    {
        mainMenuPanel.SetActive(true);
        creditsPanel.SetActive(false);
        levelsPanel.SetActive(false);
    }

    public void openLevels()
    {
        mainMenuPanel.SetActive(false);
        levelsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void openCreditPanel()
    {
        creditsPanel.SetActive(true);
        levelsPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        StartCoroutine(creditPanelAnim());
    }

    IEnumerator creditPanelAnim()
    {
        yield return new WaitForSeconds(3f);
        BackToMenu();
    }

    public void levelOneFunc()
    {
        levelOne = true;
        levelTwo = false;
        levelThree = false;
        levelFour = false;
        levelFive = false;
        LoadGame();
    }

    public void levelTwoFunc()
    {
        levelOne = false;
        levelTwo = true;
        levelThree = false;
        levelFour = false;
        levelFive = false;
        LoadGame();
    }

    public void levelThreeFunc()
    {
        levelOne = false;
        levelTwo = false;
        levelThree = true;
        levelFour = false;
        levelFive = false;
        LoadGame();
    }

    public void levelFourFunc()
    {
        levelOne = false;
        levelTwo = false;
        levelThree = false;
        levelFour = true;
        levelFive = false;
        LoadGame();
    }

    public void levelFiveFunc()
    {
        levelOne = false;
        levelTwo = false;
        levelThree = false;
        levelFour = false;
        levelFive = true;
        LoadGame();
    }

    private void LoadGame()
    {
        Application.LoadLevel("Game");
    }
}
