using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public GameObject marioPrefab;
    public GameObject bowserPrefab;
    //public GameObject activeCharacter;
    private bool isBowser = false;
    public float speed = 10;
    public float upSpeed = 10;
    public float maxSpeed = 20;
    public Rigidbody2D characterBody;
    public SpriteRenderer characterSprite;
    private bool onGroundState = true;
    public bool faceLeftState = false;
    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;
    public GameOverScreen gameOverScreen;
    public CharacterSwap characterSwap;

    // Start is called before the first frame update
    protected void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        characterBody = GetComponent<Rigidbody2D>();
        characterSprite = GetComponent<SpriteRenderer>();

        if (isBowser == false)
        {
            bowserPrefab.SetActive(false);
            characterSwap.activeCharacter = marioPrefab;
        }
        else
        {
            marioPrefab.SetActive(false);
            characterSwap.activeCharacter = bowserPrefab;
        }
        characterSprite.flipX = true;
    }

    // Update is called once per frame
    protected void Update()
    {
        // toggle state
        if (Input.GetKeyDown("a") && !faceLeftState)
        {
            faceLeftState = true;
            characterSprite.flipX = false;
        }

        if (Input.GetKeyDown("d") && faceLeftState)
        {
            faceLeftState = false;
            characterSprite.flipX = true;
        }


    }

    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }

    // FixedUpdate is called 50 times a second
    protected void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            // check if it doesn't go beyond maxSpeed
            if (characterBody.linearVelocity.magnitude < maxSpeed)
                characterBody.AddForce(movement * speed);
        }

        // stop
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            // stop
            characterBody.linearVelocity = Vector2.zero;
        }

        // jump
        if (Input.GetKeyDown("space") && onGroundState)
        {
            characterBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with goomba!");
            Debug.Log(characterSwap.activeCharacter.name);
            if (characterSwap.activeCharacter == marioPrefab)
            {
                Time.timeScale = 0.0f;
                GameOver();
            }
        }
    }

    // public void RestartButtonCallback(int input)
    // {
    //     Debug.Log("Restart!");
    //     // reset everything
    //     ResetGame();
    //     // resume time
    //     Time.timeScale = 1.0f;
    // }

    // protected void ResetGame()
    // {
    //     // reset position
    //     characterBody.transform.position = new Vector3(0.02f, 1.57f, 0.0f);
    //     // reset sprite direction
    //     faceLeftState = true;
    //     characterSprite.flipX = true;
    //     // reset score
    //     scoreText.text = "Score: 0";
    //     // reset Goomba
    //     foreach (Transform eachChild in enemies.transform)
    //     {
    //         eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().transform.position;
    //     }

    //     // reset score
    //     jumpOverGoomba.score = 0;

    //     // reset velocity to remove residual momentum
    //     characterBody.linearVelocity = Vector2.zero;

    // }

    protected void GameOver()
    {
        gameOverScreen.Setup(jumpOverGoomba.score);
    }

}