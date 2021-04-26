/********************************
 * 因为模型内部轴的原因，在做负度数运动时，关节度数是从360累加到0，所以需要校准 校准公式：实际角度=读取角度-360°
********************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint5Contol : MonoBehaviour
{
    // Start is called before the first frame update
    public float joint5Angle = 90;//关节5旋转角度

    public static bool joint5Automatic = false;//关节5自动运行判定，否停止，真开始

    public static  bool joint5MotionOver = false;//如果关节完成运动，其值为1，否则为0

    public float j5RotationSpeedX = 0;
    public float j5RotationSpeedY = 0;
    public float j5RotationSpeedZ = 30;//关节5旋转速度

    private float t5;
    public void Joint5Reset()//重置关节代码中的各项参数
    {
        joint5MotionOver = false;
        j5RotationSpeedZ = 30;//初始化速度
        if (joint5Angle >= 0 && t5 >= 0)//判断机械臂是否在做正数角度运动
        {
            if (joint5Angle <= this.transform.localEulerAngles.z + 0.5)//机械臂在做正数角度运动时，如果输入度数小于当前度数，则速度值为负
            {
                j5RotationSpeedZ = -j5RotationSpeedZ;
            }
        }
        else if (joint5Angle <= 0 && t5 <= 0)//判断机械臂是否在做负数角度运动
        {
            if ((this.transform.localEulerAngles.z - 360) >= joint5Angle)//先校准角度，机械臂在做负数角度运动时，如果输入度数小于当前度数，则速度值为负
            {
                j5RotationSpeedZ = -30;
            }
        }
    }

    public void Joint5AutomationContol()//关节5自动运动
    {
        transform.Rotate(new Vector3(j5RotationSpeedX, j5RotationSpeedY, j5RotationSpeedZ) * Time.deltaTime);//关节进行旋转
        if (joint5Angle >= 0)//当输入的角度为正数时，机械臂在正数角度运动
        {
            //print("旋转了：" + this.transform.localEulerAngles.z);
            if (j5RotationSpeedZ > 0)
            {
                if (this.transform.localEulerAngles.z >= joint5Angle - 0.5)
                {
                    j5RotationSpeedZ = 0;
                    joint5MotionOver = true;
                    joint5Automatic = false;
                }
            }
            else if (j5RotationSpeedZ < 0)
            {
                if (this.transform.localEulerAngles.z <= joint5Angle + 0.5)
                {
                    j5RotationSpeedZ = 0;
                    joint5MotionOver = true;
                    joint5Automatic = false;
                }
            }

        }
        else if (joint5Angle <= 0)//当输入的角度为负数时，机械臂在负数角度运动
        {
            //print("旋转了：" + (this.transform.localEulerAngles.z - 360));
            if (j5RotationSpeedZ > 0)
            {
                if ((this.transform.localEulerAngles.z - 360) >= joint5Angle - 0.5)//判断时需要校准旋转角度的度数，当校准角度大于设定角度时停止
                {
                    j5RotationSpeedZ = 0;
                    joint5MotionOver = true;
                    joint5Automatic = false;
                }
            }
            else if (j5RotationSpeedZ < 0)
            {
                if ((this.transform.localEulerAngles.z - 360) <= joint5Angle + 0.5)//判断时需要校准旋转角度的度数，当校准角度小于设定角度时停止
                {
                    j5RotationSpeedZ = 0;
                    joint5MotionOver = true;
                    joint5Automatic = false;
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //因为初始速度为正，如果初始输入角度为负数，需要让初始速度为负
        if (joint5Angle < 0)
            j5RotationSpeedZ = -30;
    }
    // Update is called once per frame
    void Update()
    {
        if (JointInitiaze.jointIntiazeBool == true)//初始化机械臂关节5角度
        {
            //joint5Angle = 0;
            transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        }
        if (Input.GetKey(KeyCode.Keypad5) || Input.GetKey(KeyCode.Return) || joint5Automatic == true)
        {
            t5 = joint5Angle;
            //print("旋转了" + this.transform.localEulerAngles.z);
            Joint5AutomationContol();
        }
        if (Input.GetKey(KeyCode.KeypadEnter))//对各项参数进行初始化
        {
            Joint5Reset();
        }
    }
}
