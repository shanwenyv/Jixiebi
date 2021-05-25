﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRB4600Joint1Contol : MonoBehaviour
{
    public float irb4600joint1Angle = 90;//关节1旋转角度

    public static bool irb4600joint1Automatic = false;//关节1自动运行判定，否停止，真开始

    public static bool irb4600joint1MotionOver = false;//如果关节完成运动，其值为1，否则为0

    //static public bool joint1InitizeReturn = false;
    //关节旋转角度
    public float irb4600j1RotationSpeedX = 0;
    public float irb4600j1RotationSpeedY = 30;
    public float j1RotationSpeedZ = 0;//关节1的旋转速度

    public float irb4600t1;
    // Start is called before the first frame update

    // Update is called once per frame
    public void IRB4600Joint1Reset()////重置关节代码中的各项参数
    {

        irb4600joint1MotionOver = false;
        irb4600j1RotationSpeedY = 30;//初始化速度
        if (irb4600joint1Angle >= 0 && irb4600t1 >= 0)//判断机械臂是否在做正数角度运动
        {
            if (irb4600joint1Angle < this.transform.localEulerAngles.y + 0.5)//机械臂在做正数角度运动时，如果输入度数小于当前度数，则速度值为负
            {
                irb4600j1RotationSpeedY = -irb4600j1RotationSpeedY;
            }
        }
        else if (irb4600joint1Angle <= 0 && irb4600t1 <= 0)//判断机械臂是否在做负数角度运动
        {
            if ((this.transform.localEulerAngles.y - 360) >= irb4600joint1Angle)//先校准角度，机械臂在做负数角度运动时，如果输入度数小于当前度数，则速度值为负
            {
                irb4600j1RotationSpeedY = -30;
            }
        }
    }

    public void IRB4600Joint1AutomationContol()//关节1自动运动
    {
        transform.Rotate(new Vector3(irb4600j1RotationSpeedX, irb4600j1RotationSpeedY, j1RotationSpeedZ) * Time.deltaTime);//关节进行旋转
        if (irb4600joint1Angle >= 0 && irb4600t1 >= 0)//当输入的角度为正数时，机械臂在正数角度运动
        {
            //print("旋转了：" + this.transform.localEulerAngles.y);
            if (irb4600j1RotationSpeedY > 0)
            {
                if (this.transform.localEulerAngles.y >= irb4600joint1Angle - 0.5)
                {
                    irb4600j1RotationSpeedY = 0;
                    irb4600joint1MotionOver = true;
                    irb4600joint1Automatic = false;
                }
            }
            else if (irb4600j1RotationSpeedY < 0)
            {
                if (this.transform.localEulerAngles.y <= irb4600joint1Angle + 0.5)
                {
                    irb4600j1RotationSpeedY = 0;
                    irb4600joint1MotionOver = true;
                    irb4600joint1Automatic = false;
                }
            }

        }
        else if (irb4600joint1Angle <= 0 && irb4600t1 <= 0)//当输入的角度为负数时，机械臂在负数角度运动
        {
            //print("旋转了：" + (this.transform.localEulerAngles.y - 360));
            if (irb4600j1RotationSpeedY > 0)
            {
                if ((this.transform.localEulerAngles.y - 360) >= irb4600joint1Angle - 0.5)//判断时需要校准旋转角度的度数，当校准角度大于设定角度时停止
                {
                    irb4600j1RotationSpeedY = 0;
                    irb4600joint1MotionOver = true;
                    irb4600joint1Automatic = false;
                }
            }
            else if (irb4600j1RotationSpeedY < 0)
            {
                if ((this.transform.localEulerAngles.y - 360) <= irb4600joint1Angle + 0.5)//判断时需要校准旋转角度的度数，当校准角度小于设定角度时停止
                {
                    irb4600j1RotationSpeedY = 0;
                    irb4600joint1MotionOver = true;
                    irb4600joint1Automatic = false;
                }
            }
        }
    }

    void Start()
    {
        //因为初始速度为正，如果初始输入角度为负数，需要让初始速度为负
        if (irb4600joint1Angle < 0)
            irb4600j1RotationSpeedY = -30;
    }
    void Update()
    {
        if (JointInitiaze.jointIntiazeBool == true)//初始化机械臂关节1角度
        {
            //irb4600joint1Angle = 0;
            transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            //joint1InitizeReturn = true;
            IRB4600Joint1Reset();
        }
        if (Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Return) || irb4600joint1Automatic == true)
        {
            irb4600t1 = irb4600joint1Angle;
            IRB4600Joint1AutomationContol();
        }
        if (Input.GetKey(KeyCode.KeypadEnter))//对各项参数进行初始化
        {
            IRB4600Joint1Reset();
        }
    }
}