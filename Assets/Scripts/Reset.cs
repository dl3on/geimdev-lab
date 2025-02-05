using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Reset : MonoBehaviour
{
    public GameObject marioPrefab;
    public GameObject bowserPrefab;
    public GameObject activeCharacter;
    // private Rigidbody2D characterBody;
    // private SpriteRenderer characterSprite;
    private bool faceLeftState;
    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;
    public MarioMode mario;
    public BowserMode bowser;

    // Start is called before the first frame update
    protected void Start()
    {
        // characterBody = GetComponent<Rigidbody2D>();
        // characterSprite = GetComponent<SpriteRenderer>();
    }

    public void ResetButtonCallback(int input)
    {
        Debug.Log("Restart!");
        // reset everything
        ResetGame();
        // resume time
        Time.timeScale = 1.0f;
    }

    protected void ResetGame()
    {
        // reset position
        mario.characterBody.transform.position = new Vector3(0.02f, 1.57f, 0.0f);
        bowser.characterBody.transform.position = new Vector3(0.02f, 1.57f, 0.0f);
        // reset sprite direction
        mario.faceLeftState = false;
        bowser.faceLeftState = false;
        mario.characterSprite.flipX = true;
        bowser.characterSprite.flipX = true;
        // reset score
        scoreText.text = "Score: 0";
        // reset Goomba
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().transform.position;
        }

        // reset score
        jumpOverGoomba.score = 0;

        // reset velocity to remove residual momentum
        mario.characterBody.linearVelocity = Vector2.zero;
        bowser.characterBody.linearVelocity = Vector2.zero;
    }
}