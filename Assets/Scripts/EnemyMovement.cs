using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private bool isTerrified = false;
    private Vector2 velocity;
    private Vector2 runVelocity;

    private Rigidbody2D enemyBody;
    public CharacterSwap characterSwap;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }
    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    // void RunVelocity()
    // {
    //     runVelocity = ;
    // }
    void RunAway()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * 2 * Time.fixedDeltaTime);
        float distanceFromBowser = Vector3.Distance(characterSwap.activeCharacter.transform.position, enemyBody.transform.position);

        if (distanceFromBowser < maxOffset)
        {
            // Vector2 runDirection = (enemyBody.position - (Vector2)characterSwap.activeCharacter.transform.position).normalized;
            Vector2 runVelocity = velocity * 2; // Increase speed factor as needed
            //enemyBody.MovePosition(enemyBody.position + runVelocity * Time.fixedDeltaTime);
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
        // else
        // {
        //     // while (Vector3.Distance(characterSwap.activeCharacter.transform.position, enemyBody.transform.position) < maxOffset)
        //     // {
        //     //     Debug.Log("ruun");
        //     //     RunAway();
        //     // }
        //     Debug.Log("ruun");
        //     RunAway();
        // }

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
}