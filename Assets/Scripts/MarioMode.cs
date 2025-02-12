using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarioMode : PlayerMovement
{
    new
        // Start is called before the first frame update
        void Start()
    {
        base.Start();
        characterBody = GetComponent<Rigidbody2D>();
        characterSprite = GetComponent<SpriteRenderer>();
        speed = 10;
        maxSpeed = 20;
        upSpeed = 9;
    }
}