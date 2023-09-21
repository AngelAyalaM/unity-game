using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


/**
1)menu
2)InGame
3)GameOver
4)Pausa
*/

 
public enum GameState{
    Menu,
    InGame,
    GameOver,
    Pausa
}
public class GameManager : MonoBehaviour
{   
    public GameState currentGameStates= GameState.Menu;
    private static GameManager  sharedInstance;
    public Canvas mainMenu; 
    public Canvas gameMenu; 
    public Canvas gameOverMenu; 
    int collectedCoins = 0;
    private void Awake() {
        sharedInstance = this;
    }

    public static GameManager GetInstance(){
        return sharedInstance;
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        LevelGenerator.sharedInstance.createInitialBlocks();
        PlayerController.GetInstance().StartGame();
        ChangeGameState(GameState.InGame);
        ViewInGame.GetInstance().ShowHighestScore();
    }

    private void Start(){
        //StartGame();
        currentGameStates = GameState.Menu;
         mainMenu.enabled = true;
         gameMenu.enabled = false;
         gameOverMenu.enabled = false;  
    }
    private void Update() {
        if (currentGameStates != GameState.InGame && Input.GetButtonDown("s"))
        {
            ChangeGameState(GameState.InGame);
            StartGame();
        }
    }
    // Update is called once per frame
    public void GameOver()
    {
        LevelGenerator.sharedInstance.RemoveAllBlocks();
        ChangeGameState(GameState.GameOver);
        GameOverView.GetInstance().UpdateGui();   
    }
    public void BackToMainMenu(){
        ChangeGameState(GameState.Menu);
    }
    void ChangeGameState(GameState newGameState)
    {

        switch (newGameState)
        {
            case GameState.Menu:
                //let's load Mainmenu Scene
                mainMenu.enabled = true;
                gameMenu.enabled = false;
                gameOverMenu.enabled = false;
                break;
            case GameState.InGame:
                //Unity Scene Must show the real game
                mainMenu.enabled = false;
                gameMenu.enabled = true;
                gameOverMenu.enabled = false;
                break;
            case GameState.GameOver:
                mainMenu.enabled = false;
                gameMenu.enabled = false;
                gameOverMenu.enabled = true;
                //let's load end of the game scene
                break;    
            default:
                newGameState = GameState.Menu;
                break;
        }

        currentGameStates = newGameState;

    } 

    public void CollectCoin()
    {
        collectedCoins++;
        ViewInGame.GetInstance().UpdateCoin();
    }

    public int GetCollectedCoin()
    {
        return collectedCoins;  
    }
}
