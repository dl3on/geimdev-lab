using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LakitusCloud : MonoBehaviour
{
    public Rigidbody2D cloudBody;
    public float descendSpeed = 6.0f;
    public float lowestYPos = 2.0f;
    private bool isDescending = false;
    void Start()
    {
        cloudBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isDescending && transform.position.y > lowestYPos)
        {
            transform.position += Vector3.down * descendSpeed * Time.fixedDeltaTime;
        }
    }

    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.name == "Bowser")
    //     {
    //         isDescending = true;
    //     }
    // }

    // void OnCollisionExit2D(Collision2D collision)
    // {
    //     isDescending = false;
    //     cloudBody.linearVelocityY = 0f;
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Bowser")
        {
            isDescending = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isDescending = false;
        cloudBody.linearVelocityY = 0f;
    }
}