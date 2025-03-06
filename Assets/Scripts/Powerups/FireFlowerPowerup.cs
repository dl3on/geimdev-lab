using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class FireFlowerPowerup : BasePowerup
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
        this.type = IPowerup.PowerupType.FireFlower;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && spawned)
        {
            Debug.Log("TOUCHEDE");
            ApplyPowerup(col.gameObject.GetComponent<MonoBehaviour>());

            // then destroy powerup (optional)
            DestroyPowerup();

        }
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        Debug.Log("FF SPAWNED");
        spawned = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    // private IEnumerator ToDynamic()
    // {
    //     yield return null;

    //     GetComponent<BoxCollider2D>().enabled = true;
    //     rigidBody.bodyType = RigidbodyType2D.Dynamic;
    //     rigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
    // }

    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        //base.ApplyPowerup(i);
        // try
        MarioStateController mario;
        bool result = i.TryGetComponent<MarioStateController>(out mario);
        Debug.Log(result);
        if (result)
        {
            mario.SetPowerup((PowerupType)this.type);
        }

    }

    public void GameStart()
    {
        rigidBody.bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;
        transform.localPosition = originalPosition;
        spawned = false;
    }
}