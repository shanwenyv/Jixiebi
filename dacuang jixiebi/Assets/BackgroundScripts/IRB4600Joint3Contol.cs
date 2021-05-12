using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRB4600Joint3Contol : MonoBehaviour
{
    public float irb4600joint3Angle = 90;//关节1旋转角度

    public static bool irb4600joint3Automatic = false;//关节1自动运行判定，否停止，真开始

    public static bool irb4600joint3MotionOver = false;//如果关节完成运动，其值为1，否则为0

    //static public bool joint1InitizeReturn = false;
    //关节旋转角度
    public float irb4600j3RotationSpeedX = 0;
    public float irb4600j3RotationSpeedY = 0;
    public float irb4600j3RotationSpeedZ = 30;//关节1的旋转速度

    public float irb4600t3;
    // Start is called before the first frame update

    // Update is called once per frame
    public void IRB4600Joint3Reset()////重置关节代码中的各项参数
    {

        irb4600joint3MotionOver = false;
        irb4600j3RotationSpeedZ = 30;//初始化速度
        if (irb4600joint3Angle >= 0 && irb4600t3 >= 0)//判断机械臂是否在做正数角度运动
        {
            if (irb4600joint3Angle < this.transform.localEulerAngles.z + 0.5)//机械臂在做正数角度运动时，如果输入度数小于当前度数，则速度值为负
            {
                irb4600j3RotationSpeedZ = -irb4600j3RotationSpeedZ;
            }
        }
        else if (irb4600joint3Angle <= 0 && irb4600t3 <= 0)//判断机械臂是否在做负数角度运动
        {
            if ((this.transform.localEulerAngles.z - 360) >= irb4600joint3Angle)//先校准角度，机械臂在做负数角度运动时，如果输入度数小于当前度数，则速度值为负
            {
                irb4600j3RotationSpeedZ = -30;
            }
        }
    }

    public void IRB4600Joint3AutomationContol()//关节1自动运动
    {
        print("旋转了" + this.transform.localEulerAngles.z);
        transform.Rotate(new Vector3(irb4600j3RotationSpeedX, irb4600j3RotationSpeedY, irb4600j3RotationSpeedZ) * Time.deltaTime);//关节进行旋转
        if (irb4600joint3Angle >= 0 && irb4600t3 >= 0)//当输入的角度为正数时，机械臂在正数角度运动
        {
            //print("旋转了：" + this.transform.localEulerAngles.z);
            if (irb4600j3RotationSpeedZ > 0)
            {
                if (this.transform.localEulerAngles.z >= irb4600joint3Angle - 0.5)
                {
                    irb4600j3RotationSpeedZ = 0;
                    irb4600joint3MotionOver = true;
                    irb4600joint3Automatic = false;
                }
            }
            else if (irb4600j3RotationSpeedZ < 0)
            {
                if (this.transform.localEulerAngles.z <= irb4600joint3Angle + 0.5)
                {
                    irb4600j3RotationSpeedZ = 0;
                    irb4600joint3MotionOver = true;
                    irb4600joint3Automatic = false;
                }
            }

        }
        else if (irb4600joint3Angle <= 0 && irb4600t3 <= 0)//当输入的角度为负数时，机械臂在负数角度运动
        {
            //print("旋转了：" + (this.transform.localEulerAngles.z - 360));
            if (irb4600j3RotationSpeedZ > 0)
            {
                if ((this.transform.localEulerAngles.z - 360) >= irb4600joint3Angle - 0.5)//判断时需要校准旋转角度的度数，当校准角度大于设定角度时停止
                {
                    irb4600j3RotationSpeedZ = 0;
                    irb4600joint3MotionOver = true;
                    irb4600joint3Automatic = false;
                }
            }
            else if (irb4600j3RotationSpeedZ < 0)
            {
                if ((this.transform.localEulerAngles.z - 360) <= irb4600joint3Angle + 0.5)//判断时需要校准旋转角度的度数，当校准角度小于设定角度时停止
                {
                    irb4600j3RotationSpeedZ = 0;
                    irb4600joint3MotionOver = true;
                    irb4600joint3Automatic = false;
                }
            }
        }
    }

    void Start()
    {
        //因为初始速度为正，如果初始输入角度为负数，需要让初始速度为负
        if (irb4600joint3Angle < 0)
            irb4600j3RotationSpeedZ = -30;
    }
    void Update()
    {
        if (JointInitiaze.jointIntiazeBool == true)//初始化机械臂关节1角度
        {
            //irb4600joint3Angle = 0;
            transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            //joint1InitizeReturn = true;
            IRB4600Joint3Reset();

        }
        if (Input.GetKey(KeyCode.Keypad3) || Input.GetKey(KeyCode.Return) || irb4600joint3Automatic == true)
        {
            irb4600t3 = irb4600joint3Angle;
            IRB4600Joint3AutomationContol();
        }
        if (Input.GetKey(KeyCode.KeypadEnter))//对各项参数进行初始化
        {
            IRB4600Joint3Reset();
        }
    }
}
