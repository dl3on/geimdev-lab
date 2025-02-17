using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Numerics;

public class PlayerMovement : MonoBehaviour
{
    public GameObject marioPrefab;
    public GameObject bowserPrefab;
    private bool isBowser = false;
    public float speed = 10;
    public float upSpeed = 10;
    public float maxSpeed = 20;
    public Rigidbody2D characterBody;
    public SpriteRenderer characterSprite;
    public Animator charaAnimator;
    public bool onGroundState = true;
    public bool faceLeftState = false;
    private bool moving = false;
    private bool jumpedState = false;
    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;
    public GameOverScreen gameOverScreen;
    public CharacterSwap characterSwap;

    // for audio
    public AudioSource charaAudio;
    public AudioClip charaDeath;
    public float deathImpulse = 15;

    // state
    [System.NonSerialized]
    public bool alive = true;

    // Start is called before the first frame update
    protected void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        charaAnimator.SetBool("onGround", onGroundState);

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
    }

    // Update is called once per frame
    protected void Update()
    {
        // toggle state
        // if (Input.GetKeyDown("a") && !faceLeftState)
        // {
        //     faceLeftState = true;
        //     characterSprite.flipX = true;

        //     if (characterBody.linearVelocity.x > 0.1f)
        //     {
        //         charaAnimator.SetTrigger("onSkid");
        //     }
        // }

        // if (Input.GetKeyDown("d") && faceLeftState)
        // {
        //     faceLeftState = false;
        //     characterSprite.flipX = false;

        //     if (characterBody.linearVelocity.x < -0.1f)
        //     {
        //         charaAnimator.SetTrigger("onSkid");
        //     }
        // }

        charaAnimator.SetFloat("xSpeed", Mathf.Abs(characterBody.linearVelocity.x));
    }

    // FixedUpdate is called 50 times a second
    protected void FixedUpdate()
    {
        if (alive && moving)
        {
            Move(faceLeftState == true ? -1 : 1);
            // float moveHorizontal = Input.GetAxisRaw("Horizontal");
            // if (Mathf.Abs(moveHorizontal) > 0)
            // {
            //     Vector2 movement = new Vector2(moveHorizontal, 0);
            //     // check if it doesn't go beyond maxSpeed
            //     if (characterBody.linearVelocity.magnitude < maxSpeed)
            //     {
            //         characterBody.AddForce(movement * speed);
            //         // Update animation state
            //         charaAnimator.SetFloat("xSpeed", Mathf.Abs(characterBody.linearVelocity.x));
            //     }
            // }

            // stop
            // if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
            // {
            //     // stop
            //     characterBody.linearVelocity = Vector2.zero;
            // }

            // // jump
            // if (Input.GetKeyDown("space") && onGroundState)
            // {
            //     characterBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            //     onGroundState = false;
            //     charaAnimator.SetBool("onGround", onGroundState);
            // }
        }
    }

    public void UpdateCharacterReferences(GameObject newCharacter)
    {
        characterBody = newCharacter.GetComponent<Rigidbody2D>();
        characterSprite = newCharacter.GetComponent<SpriteRenderer>();
        charaAnimator = newCharacter.GetComponent<Animator>();
    }

    void FlipSprite(int value)
    {
        if (value == -1 && !faceLeftState)
        {
            faceLeftState = true;
            characterSprite.flipX = true;
            if (characterBody.linearVelocity.x > 0.05f)
            {
                charaAnimator.SetTrigger("onSkid");
            }
        }
        else if (value == 1 && faceLeftState)
        {
            faceLeftState = false;
            characterSprite.flipX = false;

            if (characterBody.linearVelocity.x < -0.05f)
            {
                charaAnimator.SetTrigger("onSkid");
            }
        }
    }

    private void Move(int value)
    {
        UnityEngine.Vector2 movement = new UnityEngine.Vector2(value, 0);
        // check if it doesn't go beyond maxSpeed
        if (characterBody.linearVelocity.magnitude < maxSpeed)
        {
            characterBody.AddForce(movement * speed);
            // Update animation state
            charaAnimator.SetFloat("xSpeed", Mathf.Abs(characterBody.linearVelocity.x));
        }
    }

    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipSprite(value);
            moving = true;
            Move(value);
        }
    }

    public void Jump()
    {
        if (alive && onGroundState)
        {
            // jump
            characterBody.AddForce(UnityEngine.Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            charaAnimator.SetBool("onGround", onGroundState);
        }
    }

    public void JumpHold()
    {
        if (alive && jumpedState)
        {
            // jump higher
            characterBody.AddForce(UnityEngine.Vector2.up * upSpeed * 5, ForceMode2D.Force);
            jumpedState = false;

        }
    }

    int collisionLayerMask = (1 << 3) | (1 << 6) | (1 << 7);
    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) && !onGroundState)
        {
            onGroundState = true;
            // update animator state
            charaAnimator.SetBool("onGround", onGroundState);
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && alive)
        {
            Debug.Log("Collided with goomba!");
            Debug.Log(characterSwap.activeCharacter.name);
            if (characterSwap.activeCharacter == marioPrefab)
            {
                // charaAnimator.Play("mario-die");
                // charaAudio.PlayOneShot(charaDeath);
                PlayDeathImpulse();
                alive = false;
                Time.timeScale = 0.0f;
                GameOver();
            }
        }
    }

    void PlayJumpSound()
    {
        // play jump sound
        charaAudio.PlayOneShot(charaAudio.clip);
    }

    void PlayDeathImpulse()
    {
        characterBody.AddForce(UnityEngine.Vector2.up * deathImpulse, ForceMode2D.Impulse);
        charaAnimator.Play("mario-die");
        charaAudio.PlayOneShot(charaDeath);
    }

    protected void GameOver()
    {
        gameOverScreen.Setup(jumpOverGoomba.score);
    }

}