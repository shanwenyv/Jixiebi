using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint5Contol : MonoBehaviour
{
    // Start is called before the first frame update
    public float joint5Angle = 90;//关节5旋转角度
    //关节旋转速度
    public float j5RotationSpeedX = 0;
    public float j5RotationSpeedY = 0;
    public float j5RotationSpeedZ = 30;//关节5旋转速度

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
        if (Input.GetKey(KeyCode.Keypad5) || Input.GetKey(KeyCode.Return))
        {
            transform.Rotate(new Vector3(j5RotationSpeedX, j5RotationSpeedY, j5RotationSpeedZ) * Time.deltaTime);//关节进行旋转
            if (joint5Angle >= 0)//当输入的角度为正数时，机械臂在正数角度运动
            {
                print("旋转了：" + this.transform.localEulerAngles.z);
                if (j5RotationSpeedZ > 0)
                {
                    if (this.transform.localEulerAngles.z >= joint5Angle - 0.5)
                    {
                        j5RotationSpeedZ = 0;

                    }
                }
                else if (j5RotationSpeedZ < 0)
                {
                    if (this.transform.localEulerAngles.z <= joint5Angle + 0.5)
                    {
                        j5RotationSpeedZ = 0;

                    }
                }

            }
            else if (joint5Angle <= 0)//当输入的角度为负数时，机械臂在负数角度运动
            {
                print("旋转了：" + (this.transform.localEulerAngles.z - 360));
                if (j5RotationSpeedZ > 0)
                {
                    if ((this.transform.localEulerAngles.z - 360) >= joint5Angle - 0.5)//判断时需要校准旋转角度的度数，当校准角度大于设定角度时停止
                    {
                        j5RotationSpeedZ = 0;

                    }
                }
                else if (j5RotationSpeedZ < 0)
                {
                    if ((this.transform.localEulerAngles.z - 360) <= joint5Angle + 0.5)//判断时需要校准旋转角度的度数，当校准角度小于设定角度时停止
                    {
                        j5RotationSpeedZ = 0;

                    }
                }
            }
        }
        if (Input.GetKey(KeyCode.KeypadEnter))//对各项参数进行初始化
        {
            j5RotationSpeedZ = 30;//初始化速度
            if (joint5Angle > 0)//判断机械臂是否在做正数角度运动
            {
                if (joint5Angle < this.transform.localEulerAngles.z + 0.5)//机械臂在做正数角度运动时，如果输入度数小于当前度数，则速度值为负
                {
                    j5RotationSpeedZ = -j5RotationSpeedZ;
                }
            }
            else if (joint5Angle < 0)//判断机械臂是否在做负数角度运动
            {
                if ((this.transform.localEulerAngles.z - 360) >= joint5Angle)//先校准角度，机械臂在做负数角度运动时，如果输入度数小于当前度数，则速度值为负
                {
                    j5RotationSpeedZ = -30;
                }
            }
        }
    }
}
