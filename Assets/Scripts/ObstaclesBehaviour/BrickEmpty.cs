using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BrickEmpty : MonoBehaviour
{
    public Animator blockAnimator;

    void Awake()
    {
        //GameManager.instance.gameRestart.AddListener(GameStart);
    }

    void Start()
    {
        blockAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            blockAnimator.SetTrigger("hit");
        }
    }

    public void GameStart()
    {
        // nothing as of now
    }
}