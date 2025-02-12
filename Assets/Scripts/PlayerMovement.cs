using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public bool onGroundState = true;
    public bool faceLeftState = false;
    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;
    public GameOverScreen gameOverScreen;
    public CharacterSwap characterSwap;
    public Animator marioAnimator;

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
        marioAnimator = GetComponent<Animator>();
        marioAnimator.SetBool("onGround", onGroundState);

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
        // Vector3 charSpriteScale = characterSprite.transform.localScale;
        // charSpriteScale.x = -1;
        // characterSprite.transform.localScale = charSpriteScale;
    }

    // Update is called once per frame
    protected void Update()
    {
        // toggle state
        if (Input.GetKeyDown("a") && !faceLeftState)
        {
            faceLeftState = true;
            characterSprite.flipX = true;

            if (characterBody.linearVelocity.x > 0.1f)
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }

        if (Input.GetKeyDown("d") && faceLeftState)
        {
            faceLeftState = false;
            characterSprite.flipX = false;

            if (characterBody.linearVelocity.x < -0.1f)
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(characterBody.linearVelocity.x));
    }

    int collisionLayerMask = (1 << 3) | (1 << 6) | (1 << 7);
    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) && !onGroundState)
        {
            onGroundState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    // FixedUpdate is called 50 times a second
    protected void FixedUpdate()
    {
        if (alive)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            if (Mathf.Abs(moveHorizontal) > 0)
            {
                Vector2 movement = new Vector2(moveHorizontal, 0);
                // check if it doesn't go beyond maxSpeed
                if (characterBody.linearVelocity.magnitude < maxSpeed)
                {
                    characterBody.AddForce(movement * speed);
                    // Update animation state
                    marioAnimator.SetFloat("xSpeed", Mathf.Abs(characterBody.linearVelocity.x));
                }
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
                marioAnimator.SetBool("onGround", onGroundState);
            }
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
                // marioAnimator.Play("mario-die");
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
        characterBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
        marioAnimator.Play("mario-die");
        charaAudio.PlayOneShot(charaDeath);
    }

    protected void GameOver()
    {
        gameOverScreen.Setup(jumpOverGoomba.score);
    }

}