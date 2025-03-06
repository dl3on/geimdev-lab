using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class CoinPowerup : BasePowerup
{
    private Vector3 originalPosition;

    void Awake()
    {
        //GameManager.instance.gameRestart.AddListener(GameStart);
        originalPosition = transform.localPosition;
    }

    // setup this object's type
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = IPowerup.PowerupType.Coin;
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        spawned = true;
    }

    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        // TODO: increase score

    }

    public void GameStart()
    {
        rigidBody.bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;
        transform.localPosition = originalPosition;
        spawned = false;
    }
}