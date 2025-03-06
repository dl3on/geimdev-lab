using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionBlock : MonoBehaviour
{
    public Animator blockAnimator;
    public Coin coin;
    public bool claimed;

    void Awake()
    {
        //GameManager.instance.gameRestart.AddListener(GameStart);
    }

    void Start()
    {
        blockAnimator = GetComponent<Animator>();
        claimed = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !claimed)
        {
            claimed = true;
            blockAnimator.SetTrigger("hit");
            coin.PopCoin();
        }
    }

    public void GameStart()
    {
        claimed = false;

        blockAnimator.Rebind();
        blockAnimator.Update(0);
    }
}