using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class MagicMushroomPowerup : BasePowerup
{
    private Vector3 originalPosition;

    void Awake()
    {
        GameManager.instance.gameRestart.AddListener(GameStart);
        originalPosition = transform.localPosition;
    }

    // setup this object's type
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = IPowerup.PowerupType.MagicMushroom;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && spawned)
        {
            // TODO: do something when colliding with Player

            // then destroy powerup (optional)
            DestroyPowerup();

        }
        else if (col.gameObject.layer == 7) // else if hitting Pipe, flip travel direction
        {
            if (spawned)
            {
                goRight = !goRight;
                rigidBody.AddForce(Vector2.right * 3 * (goRight ? 1 : -1), ForceMode2D.Impulse);

            }
        }
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        spawned = true;
        StartCoroutine(ToDynamic());
        // GetComponent<BoxCollider2D>().enabled = true;
        // rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        // rigidBody.AddForce(Vector2.right * 3, ForceMode2D.Impulse); // move to the right
    }

    private IEnumerator ToDynamic()
    {
        yield return null;

        GetComponent<BoxCollider2D>().enabled = true;
        rigidBody.bodyType = RigidbodyType2D.Dynamic;

        rigidBody.AddForce(Vector2.right * 3, ForceMode2D.Impulse);
    }

    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        // TODO: do something with the object

    }

    public void GameStart()
    {
        rigidBody.bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;
        transform.localPosition = originalPosition;
        spawned = false;
    }
}