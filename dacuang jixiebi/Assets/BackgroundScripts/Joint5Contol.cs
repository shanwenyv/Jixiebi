﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint5Contol : MonoBehaviour
{
    public float joint5Angle = 90;//关节5旋转角度


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad5))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, joint5Angle), 0.01f);
        }
    }
}
