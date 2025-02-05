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
        speed = 10;
        maxSpeed = 20;
        upSpeed = 9;
        Debug.Log("Mario mode!");
    }
}