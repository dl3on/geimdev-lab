using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BowserMode : PlayerMovement
{
    new
        // Start is called before the first frame update
        void Start()
    {
        base.Start();
        characterBody = GetComponent<Rigidbody2D>();
        characterSprite = GetComponent<SpriteRenderer>();
        speed = 5;
        maxSpeed = 10;
        upSpeed = 12;
    }

    private void Roar() { }
}