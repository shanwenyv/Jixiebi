using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint1Contol : MonoBehaviour
{
    public float joint1Angle = 90;//关节1旋转角度

    //关节旋转角度
    public float j1RotationSpeedX = 0;
    public float j1RotationSpeedY = 0;
    public float j1RotationSpeedZ = 30;

    private int t = 0;
    private int a = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Return))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-90, 0, joint1Angle), 0.01f);
        }*/
        /*if (Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Return))
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90), 0.01f);
            if (joint1Angle >= 0 && joint1Angle <= 90)//到了指定角度就停止旋转
            {
                if (this.transform.localEulerAngles.z >= joint1Angle - 0.5)
                    j1RotationSpeedZ = 0;
                transform.Rotate(new Vector3(j1RotationSpeedX, j1RotationSpeedY, j1RotationSpeedZ) * Time.deltaTime);
                //print("旋转了：" + this.transform.localEulerAngles.x);
            }
            else if (joint1Angle > 90 && joint1Angle <= 180)
            {
                //print(90 - (joint3Angle - 90));
                transform.Rotate(new Vector3(j1RotationSpeedX, j1RotationSpeedY, j1RotationSpeedZ) * Time.deltaTime);
                print("旋转了：" + this.transform.localEulerAngles.z);
                if (this.transform.localEulerAngles.z > 89)//校准：当旋转角度大于90的时候，启动检测判定（因为此模型转轴度数的特殊性，特写此算法）
                {
                    t = 1;
                }

                if (this.transform.localEulerAngles.z < 90 - (joint1Angle - 90.5) && t == 1)
                {
                    //print("执行了233");
                    j1RotationSpeedZ = 0;
                }
            }
            else if (joint1Angle > 180 && joint1Angle <= 270)
            {
                transform.Rotate(new Vector3(j1RotationSpeedX, j1RotationSpeedY, j1RotationSpeedZ) * Time.deltaTime);
                //print("旋转了：" + this.transform.localEulerAngles.x); ;
                if (this.transform.localEulerAngles.z > 179.8)//校准：当旋转角度大于180的时候，启动检测判定（因为此模型转轴度数的特殊性，特写此算法）
                {
                    t = 1;
                }
                if (this.transform.localEulerAngles.z < 360.5 - (180 - (360 - joint1Angle)) && t == 1)
                {
                    //print("执行了233");
                    j1RotationSpeedZ = 0;
                }
            }
            else if (joint1Angle > 270 && joint1Angle <= 360)
            {

                transform.Rotate(new Vector3(j1RotationSpeedX, j1RotationSpeedY, j1RotationSpeedZ) * Time.deltaTime);
                //print("旋转了：" + this.transform.localEulerAngles.x);
                if (this.transform.localEulerAngles.z > 350.5)//校准：当旋转角度大于270的时候，启动检测判定（因为此模型转轴度数的特殊性，特写此算法）
                {
                    t = 1;
                }
                if (this.transform.localEulerAngles.z < 270.5 && t == 1)
                {
                    a = 1;
                }
                if (this.transform.localEulerAngles.z >= joint1Angle - 0.5 && a == 1)
                    j1RotationSpeedX = 0;
            }
        }*/
        if (Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Return))
        {
            if (joint1Angle >= 0)
            {
                //print("旋转了：" + this.transform.localEulerAngles.z);
                if (this.transform.localEulerAngles.z >= joint1Angle - 0.5)
                {
                    j1RotationSpeedZ = 0;
                }
                transform.Rotate(new Vector3(j1RotationSpeedX, j1RotationSpeedY, j1RotationSpeedZ) * Time.deltaTime);
            }
        }
            if (Input.GetKey(KeyCode.KeypadEnter))
        {
            j1RotationSpeedZ = 30;
            a = 0;
            t = 0;
        }

    }
}
