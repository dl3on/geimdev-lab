using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    public Animator coinAnimator;
    public AudioSource coinAudio;

    void Awake()
    {
        GameManager.instance.gameRestart.AddListener(GameStart);
    }

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

        // disable coin upon collection
        StartCoroutine(DisableAfterSound());
    }

    IEnumerator DisableAfterSound()
    {
        yield return new WaitForSeconds(coinAudio.clip.length);  // Wait for sound to finish
        gameObject.SetActive(false);
    }

    public void GameStart()
    {
        coinAnimator.Rebind();
        coinAnimator.Update(0);
        gameObject.SetActive(false);
    }
}