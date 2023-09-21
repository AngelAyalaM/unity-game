using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameOverView : MonoBehaviour
{
    public TMP_Text coinsLabel;
    public TMP_Text scoreText;
    private static GameOverView sharedInstance;
    // Start is called before the first frame update
    public static GameOverView GetInstance()
    {
       return sharedInstance; 
    }
    private void Awake() {
        sharedInstance = this;

    }
   

    // Update is called once per frame
    public void UpdateGui()
    {
        if (GameManager.GetInstance().currentGameStates == GameState.GameOver )
        {
            
            coinsLabel.text = GameManager.GetInstance().GetCollectedCoin().ToString();
            scoreText.text = PlayerController.GetInstance().GetDistance().ToString();
            
        }
        
    }
}
