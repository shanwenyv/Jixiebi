﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContol : MonoBehaviour
{
    private CharacterController cc;//

    public float moveSpeed;//角色移动速度

    private float horizontalMove, verticalMove;//按键值变量

    private Vector3 dir;//定义三维变量,dir控制方向

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject.Find("Camera").GetComponent<PlayerCameraControl>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject.Find("Camera").GetComponent<PlayerCameraControl>().enabled = true;
        }
        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;
        verticalMove = Input.GetAxis("Vertical") * moveSpeed;

        //dir储存移动的方向
        dir = transform.forward * verticalMove + transform.right * horizontalMove;
        cc.Move(dir * Time.deltaTime);
    
    
    
    }
}
