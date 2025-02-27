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
    private Vector3 startPosition;

    void Awake()
    {
        GameManager.instance.gameRestart.AddListener(GameStart);
    }

    void Start()
    {
        cloudBody = GetComponent<Rigidbody2D>();
        startPosition = transform.localPosition;
    }

    void FixedUpdate()
    {
        if (isDescending && transform.position.y > lowestYPos)
        {
            transform.position += Vector3.down * descendSpeed * Time.fixedDeltaTime;
        }
    }

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

    public void GameStart()
    {
        transform.localPosition = startPosition;
    }
}