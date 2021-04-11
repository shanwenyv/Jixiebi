/*********************
 角度为模型相对于地面的夹角

**********************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint4Contol : MonoBehaviour
{
    // Start is called before the first frame update
    public float joint4Angle = 90;//关节4旋转角度
    // 关节旋转速度
    public float j4RotationSpeedX = 30;
    public float j4RotationSpeedY = 0;
    public float j4RotationSpeedZ = 0;

    private int t = 0;
    private int a = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad4) || Input.GetKey(KeyCode.Return))
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90), 0.01f);
            if (joint4Angle >= 0 && joint4Angle <= 90)//到了指定角度就停止旋转
            {
                if (this.transform.localEulerAngles.x >= joint4Angle - 0.5)
                    j4RotationSpeedX = 0;
                transform.Rotate(new Vector3(j4RotationSpeedX, j4RotationSpeedY, j4RotationSpeedZ) * Time.deltaTime);
                //print("旋转了：" + this.transform.localEulerAngles.x);
            }
            else if (joint4Angle > 90 && joint4Angle <= 180)
            {
                //print(90 - (joint3Angle - 90));
                transform.Rotate(new Vector3(j4RotationSpeedX, j4RotationSpeedY, j4RotationSpeedZ) * Time.deltaTime);
                //print("旋转了：" + this.transform.localEulerAngles.x);;
                if (this.transform.localEulerAngles.x > 89.5)//校准：当旋转角度大于90的时候，启动检测判定（因为此模型转轴度数的特殊性，特写此算法）
                {
                    t = 1;
                }

                if (this.transform.localEulerAngles.x < 90 - (joint4Angle - 90.5) && t == 1)
                {
                    //print("执行了233");
                    j4RotationSpeedX = 0;
                }
            }
            else if (joint4Angle > 180 && joint4Angle <= 270)
            {
                transform.Rotate(new Vector3(j4RotationSpeedX, j4RotationSpeedY, j4RotationSpeedZ) * Time.deltaTime);
                //print("旋转了：" + this.transform.localEulerAngles.x); ;
                if (this.transform.localEulerAngles.x > 179.8)//校准：当旋转角度大于180的时候，启动检测判定（因为此模型转轴度数的特殊性，特写此算法）
                {
                    t = 1;
                }
                if (this.transform.localEulerAngles.x < 360.5 - (180 - (360 - joint4Angle)) && t == 1)
                {
                    //print("执行了233");
                    j4RotationSpeedX = 0;
                }
            }
            else if (joint4Angle > 270 && joint4Angle <= 360)
            {

                transform.Rotate(new Vector3(j4RotationSpeedX, j4RotationSpeedY, j4RotationSpeedZ) * Time.deltaTime);
                //print("旋转了：" + this.transform.localEulerAngles.x);
                if (this.transform.localEulerAngles.x > 350.5)//校准：当旋转角度大于270的时候，启动检测判定（因为此模型转轴度数的特殊性，特写此算法）
                {
                    t = 1;
                }
                if (this.transform.localEulerAngles.x < 270.5 && t == 1)
                {
                    a = 1;
                }
                if (this.transform.localEulerAngles.x >= joint4Angle - 0.5 && a == 1)
                    j4RotationSpeedX = 0;
            }
        }
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            j4RotationSpeedX = 30;
            a = 0;
            t = 0;
        }
    }
}
