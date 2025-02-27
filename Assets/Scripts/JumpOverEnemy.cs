using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JumpOverEnemy : MonoBehaviour
{
    public Animator enemyAnimator;
    public Rigidbody2D enemyBody;
    public BoxCollider2D enemyCollider;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
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
            GameManager.instance.IncreaseScore(1); // Increase Score

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
