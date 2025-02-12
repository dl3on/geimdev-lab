using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    public Animator coinAnimator;
    public AudioSource coinAudio;
    void Start()
    {
        coinAnimator = GetComponent<Animator>();
        // disable object
        gameObject.SetActive(false);
    }

    public void PopCoin()
    {
        gameObject.SetActive(true);
        coinAnimator.SetTrigger("pop");
    }

    private void PlayCoinSound()
    {
        coinAudio.PlayOneShot(coinAudio.clip);
    }
}