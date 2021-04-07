﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint5Contol : MonoBehaviour
{
    // Start is called before the first frame update
    public float joint5Angle = 90;//关节5旋转角度
    public Joint1Contol other5;

    // Start is called before the first frame update
    void Start()
    {
        joint5Angle = joint5Angle - 90;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad5) || Input.GetKey(KeyCode.Return))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, joint5Angle, 0), 0.01f);
        }
    }
}
