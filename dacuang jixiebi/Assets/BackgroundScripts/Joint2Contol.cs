using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint2Contol : MonoBehaviour
{
    public float joint2Angle = 90;//关节2旋转角度
    //关节旋转角度
    public float j2RotationSpeedX = 30;
    public float j2RotationSpeedY = 0;
    public float j2RotationSpeedZ = 0;


    private int t = 0;
    private int a = 0;
    // Start is called before the first frame update
    void Start()
    {
        //joint2Angle = -joint2Angle+20;
        //joint2Angle = joint2Angle / 2;
        //joint2Angle = joint2Angle - 90;//旋转角度校准
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Return))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(joint2Angle, other.joint1Angle, 0), 0.01f);//调用Joint2Contl中的旋转角度抵消臂1对臂2坐标的影响
        }*/
        if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Return))
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90), 0.01f);
            if (joint2Angle >= 0 && joint2Angle <= 90)//到了指定角度就停止旋转
            {
                if(j2RotationSpeedX > 0)
                {
                    if(this.transform.localEulerAngles.x >= joint2Angle - 0.5)
                    {
                        j2RotationSpeedX = 0;
                    }
                }
                if (j2RotationSpeedX < 0)
                {
                    if (this.transform.localEulerAngles.x <= joint2Angle + 0.5)
                    {
                        j2RotationSpeedX = 0;
                    }
                } 
                transform.Rotate(new Vector3(j2RotationSpeedX, j2RotationSpeedY, j2RotationSpeedZ) * Time.deltaTime);
                //print("旋转了：" + this.transform.localEulerAngles.x);
            }
            else if (joint2Angle > 90 && joint2Angle <= 180)
            {
                //print(90 - (joint3Angle - 90));
                transform.Rotate(new Vector3(j2RotationSpeedX, j2RotationSpeedY, j2RotationSpeedZ) * Time.deltaTime);
                //print("旋转了：" + this.transform.localEulerAngles.x);;
                if (this.transform.localEulerAngles.x > 89.5)//校准：当旋转角度大于90的时候，启动检测判定（因为此模型转轴度数的特殊性，特写此算法）
                {
                    t = 1;
                }

                if (this.transform.localEulerAngles.x < 90 - (joint2Angle - 90.5) && t == 1)
                {
                    //print("执行了233");
                    j2RotationSpeedX = 0;
                }
            }
            else if (joint2Angle > 180 && joint2Angle <= 270)
            {
                transform.Rotate(new Vector3(j2RotationSpeedX, j2RotationSpeedY, j2RotationSpeedZ) * Time.deltaTime);
                //print("旋转了：" + this.transform.localEulerAngles.x); ;
                if (this.transform.localEulerAngles.x > 179.8)//校准：当旋转角度大于180的时候，启动检测判定（因为此模型转轴度数的特殊性，特写此算法）
                {
                    t = 1;
                }
                if (this.transform.localEulerAngles.x < 360.5 - (180 - (360 - joint2Angle)) && t == 1)
                {
                    //print("执行了233");
                    j2RotationSpeedX = 0;
                }
            }
            else if (joint2Angle > 270 && joint2Angle <= 360)
            {

                transform.Rotate(new Vector3(j2RotationSpeedX, j2RotationSpeedY, j2RotationSpeedZ) * Time.deltaTime);
                //print("旋转了：" + this.transform.localEulerAngles.x);
                if (this.transform.localEulerAngles.x > 350.5)//校准：当旋转角度大于270的时候，启动检测判定（因为此模型转轴度数的特殊性，特写此算法）
                {
                    t = 1;
                }
                if (this.transform.localEulerAngles.x < 270.5 && t == 1)
                {
                    a = 1;
                }
                if (this.transform.localEulerAngles.x >= joint2Angle - 0.5 && a == 1)
                    j2RotationSpeedX = 0;
            }
        }
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            j2RotationSpeedX = 30;
            a = 0;
            t = 0;
            if (joint2Angle < this.transform.localEulerAngles.x + 0.5)
            {
                j2RotationSpeedX = -j2RotationSpeedX;
            }
        }
    }
}
