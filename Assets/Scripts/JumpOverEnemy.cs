using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class JumpOverEnemy : MonoBehaviour
{
    public Animator enemyAnimator;
    public Rigidbody2D enemyBody;
    public BoxCollider2D enemyCollider;

    public int parameter;
    // Events invoked by enemy
    public UnityEvent<int> increaseScore;

    void Start()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Mario")
        {
            Debug.Log("STOMPED BY: " + other.gameObject.name);
            DestroyGoomba();
            increaseScore.Invoke(parameter); // Increase Score
        }
    }

    void DestroyGoomba()
    {
        enemyAnimator.SetTrigger("onStomped");
        enemyBody.linearVelocity = Vector2.zero;  // Stop Movement
        GetComponentInParent<EnemyMovement>().enabled = false;  // Disable Movement Script
        GetComponent<BoxCollider2D>().enabled = false;
        enemyCollider.enabled = false;

        // Disable after 1 second
        Invoke("DisableGoomba", 1.0f);
    }

    void DisableGoomba()
    {
        transform.parent.gameObject.SetActive(false);
    }

}
