﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNPC : NonPlayerCharacter
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Triggered");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetInteger("attack", 1);
        }
    }
}
