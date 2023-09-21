using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rgb2d;
    public float thrust = 20.0f;
    public LayerMask groundLayerMask;
    public Animator animator;
    public float runSpeed = 1.5f;
    // Start is called before the first frame update
    private static PlayerController sharedInstance;

    private Vector3 initialPosition;
    private Vector2 initialVelocity;
    private float initialGravity;
    private const string HIGHEST_SCORE_KEY = "highestScore";

    private void Awake() {
        sharedInstance = this;
        rgb2d = GetComponent<Rigidbody2D>();
        
        initialPosition = transform.position;
        initialVelocity = rgb2d.velocity;
        animator.SetBool("isAlive", true);
        initialGravity = rgb2d.gravityScale;
    }
    public static PlayerController GetInstance(){
        return sharedInstance; 
    }
    public void StartGame()
    {
        animator.SetBool("isAlive", true);
        transform.position = initialPosition;
        rgb2d.velocity = initialVelocity;
        rgb2d.gravityScale = initialGravity;
    }
    private void FixedUpdate() {

        GameState currenState = GameManager.GetInstance().currentGameStates;
        if (currenState == GameState.InGame){
            if (rgb2d.velocity.x < runSpeed)
            {
                rgb2d.velocity = new Vector2(runSpeed,rgb2d.velocity.y);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        bool canJump = GameManager.GetInstance().currentGameStates == GameState.InGame;

        bool isOnTheGround = IsOnTheGround();
        animator.SetBool("isGrounded",isOnTheGround);
        if(canJump && (Input.GetMouseButtonDown(0)||
         Input.GetKeyDown(KeyCode.Space)||
         Input.GetKeyDown(KeyCode.W))
          && isOnTheGround ) 
        {
         Jump();
        } 
    }

    void Jump(){
        rgb2d.AddForce(Vector2.up*thrust,ForceMode2D.Impulse);
    }
    bool IsOnTheGround(){
        return Physics2D.Raycast(this.transform.position,Vector2.down,2.0f,groundLayerMask.value);
    }

    public void KillPlayer(){
        animator.SetBool("isAlive", false);
        GameManager.GetInstance().GameOver();
        int highestScore = PlayerPrefs.GetInt(HIGHEST_SCORE_KEY);
        int currentScore = GetDistance();
        if (currentScore > highestScore)
        {
            PlayerPrefs.SetInt(HIGHEST_SCORE_KEY, currentScore);
        }
        rgb2d.gravityScale = 0f;
        rgb2d.velocity = Vector2.zero;
        GameManager.GetInstance().GameOver();
    }

    public int GetDistance()
    {
        var distance = (int) Vector2.Distance(initialPosition,transform.position);
        print("distance = "+distance); 
        return distance;
    }

    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt(HIGHEST_SCORE_KEY);
    }

}
