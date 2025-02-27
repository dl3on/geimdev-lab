using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameConstants gameConstants;
    private Vector3 startPosition;
    private float originalX;
    float maxOffset;
    float enemyPatroltime;
    private int moveRight = -1;
    private bool isTerrified = false;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;
    public CharacterManager characterManager;
    public BoxCollider2D childCollider;

    // Audio
    public AudioSource enemyStomp;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        startPosition = transform.localPosition;
        originalX = transform.position.x;
        maxOffset = gameConstants.maxOffset;
        enemyPatroltime = gameConstants.enemyPatroltime;
        ComputeVelocity();
    }
    void ComputeVelocity()
    {
        Debug.Log(maxOffset);
        Debug.Log(enemyPatroltime);
        velocity = new Vector2(moveRight * maxOffset / enemyPatroltime, 0);
        Debug.Log(velocity);
    }
    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void RunAway()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * 2 * Time.fixedDeltaTime);
        float distanceFromBowser = Vector3.Distance(characterManager.activeCharacter.transform.position, enemyBody.transform.position);

        if (distanceFromBowser < maxOffset)
        {
            Vector2 runVelocity = velocity * 2; // Increase speed factor as needed
            enemyBody.linearVelocity = runVelocity;
        }
        else
        {
            enemyBody.linearVelocity = Vector2.zero;
            isTerrified = false;
            originalX = enemyBody.position.x;
        }
    }

    void Update()
    {
        if (!isTerrified)
        {
            if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
            {// move goomba
                Movegoomba();
            }
            else
            {
                // change direction
                moveRight *= -1;
                ComputeVelocity();
                Movegoomba();
            }
        }
    }

    void FixedUpdate()
    {
        if (isTerrified)
        {
            RunAway();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Bowser")
        {
            isTerrified = true;
        }
    }

    void PlayStompedSound()
    {
        enemyStomp.PlayOneShot(enemyStomp.clip);
    }

    public void GameRestart()
    {
        gameObject.SetActive(true);
        transform.localPosition = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        isTerrified = false;
        gameObject.GetComponent<EnemyMovement>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        childCollider.enabled = true;
        ComputeVelocity();
    }

}