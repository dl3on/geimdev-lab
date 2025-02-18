using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public GameObject mario;
    public GameObject bowser;
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
    public GameManager gameManager;

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
    }

    // Update is called once per frame
    protected void Update()
    {
        charaAnimator.SetFloat("xSpeed", Mathf.Abs(characterBody.linearVelocity.x));
    }

    // FixedUpdate is called 50 times a second
    protected void FixedUpdate()
    {
        if (alive && moving)
        {
            Move(faceLeftState == true ? -1 : 1);
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
        Vector2 movement = new Vector2(value, 0);
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
            characterBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
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
            characterBody.AddForce(5 * upSpeed * Vector2.up, ForceMode2D.Force);
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
            if (characterSwap.activeCharacter == mario)
            {
                PlayDeathImpulse();
                alive = false;
                Time.timeScale = 0.0f;
                gameManager.GameOver();
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
        charaAnimator.Play("mario-die");
        charaAudio.PlayOneShot(charaDeath);
    }

    public void GameRestart()
    {
        characterBody.transform.position = new Vector3(0.02f, 1.57f, 0.0f);
        characterBody.linearVelocity = Vector2.zero;
        faceLeftState = false;
        characterSprite.flipX = false;
        charaAnimator.SetTrigger("gameRestart");
        alive = true;
    }
}