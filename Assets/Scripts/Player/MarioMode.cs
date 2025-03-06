using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarioMode : PlayerMovement
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        characterBody = GetComponent<Rigidbody2D>();
        characterSprite = GetComponent<SpriteRenderer>();
        charaAnimator = GetComponent<Animator>();
        charaAnimator.keepAnimatorStateOnDisable = true;
        speed = 10;
        maxSpeed = 20;
        upSpeed = 9;
    }

    private void OnDisable()
    {
        charaAnimator.Play("mario-idle", 0, 0f);
    }
}