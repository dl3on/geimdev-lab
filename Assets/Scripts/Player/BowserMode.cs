using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BowserMode : PlayerMovement
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        characterBody = GetComponent<Rigidbody2D>();
        characterSprite = GetComponent<SpriteRenderer>();
        charaAnimator = GetComponent<Animator>();
        charaAnimator.keepAnimatorStateOnDisable = true;
        speed = 5;
        maxSpeed = 10;
        upSpeed = 12;
    }

    private void Roar() { }

    private void OnDisable()
    {
        charaAnimator.Play("bowser-idle", 0, 0f);
    }
}