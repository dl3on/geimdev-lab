using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JumpOverEnemy : MonoBehaviour
{
    // public Transform enemyLocation;
    // public TextMeshProUGUI scoreText;
    public Animator enemyAnimator;
    public Rigidbody2D enemyBody;
    public BoxCollider2D enemyCollider;
    // private bool onGroundState;

    // [System.NonSerialized]
    // public int score = 0; // we don't want this to show up in the inspector

    // private bool countScoreState = false;
    // public Vector3 boxSize;
    // public float maxDistance;
    // public LayerMask layerMask;
    // public CharacterSwap characterSwap;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        //enemyBody = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // void FixedUpdate()
    // {
    //     // mario jumps
    //     if (Input.GetKeyDown("space") && onGroundCheck())
    //     {
    //         onGroundState = false;
    //         countScoreState = true;
    //     }

    //     // when jumping, and Goomba is near Mario and we haven't registered our score
    //     if (!onGroundState && countScoreState)
    //     {
    //         if (Mathf.Abs(characterSwap.activeCharacter.transform.position.x - enemyLocation.position.x) < 0.2f)
    //         {
    //             countScoreState = false;
    //             gameManager.IncreaseScore(1);
    //         }
    //     }
    // }

    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("STOMPED BY: " + other.gameObject.name);
            //countScoreState = false;
            DestroyGoomba();
            gameManager.IncreaseScore(1); // Increase Score

            // Destroy the enemy
        }
    }

    void DestroyGoomba()
    {
        enemyAnimator.SetTrigger("onStomped");
        enemyBody.linearVelocity = Vector2.zero;  // Stop Movement
        GetComponentInParent<EnemyMovement>().enabled = false;  // Disable Movement Script
        GetComponent<BoxCollider2D>().enabled = false;
        enemyCollider.enabled = false;
        Destroy(transform.parent.gameObject, 1f);
    }

    // public void HandleStomp(GameObject enemyObject)
    // {
    //     if (true)
    //     {
    //         //countScoreState = false;

    //         gameManager.IncreaseScore(1);
    //         Destroy(enemyObject);
    //     }
    // }

    // private bool onGroundCheck()
    // {
    //     if (Physics2D.BoxCast(characterSwap.activeCharacter.transform.position, boxSize, 0, -characterSwap.activeCharacter.transform.up, maxDistance, layerMask))
    //     {
    //         Debug.Log("on ground");
    //         return true;
    //     }
    //     else
    //     {
    //         Debug.Log("not on ground");
    //         return false;
    //     }
    // }


    // // helper
    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawCube(characterSwap.activeCharacter.transform.position - characterSwap.activeCharacter.transform.up * maxDistance, boxSize);
    // }

}
