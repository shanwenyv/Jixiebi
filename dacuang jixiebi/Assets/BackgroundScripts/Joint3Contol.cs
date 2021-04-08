
/*****************************************************
2021.4.7更新日志：完成了110°~180°的校准
2021.4.8更新日志：完成了-180°~100°的校准
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint3Contol : MonoBehaviour
{
    public float joint3Angle = 90;//关节3旋转角度
   
    //定义关节旋转角度
    public float j3RotationSpeedX = 30;
    public float j3RotationSpeedY = 0;
    public float j3RotationSpeedZ = 0;

    void Start()
    {

        //joint3Angle = joint3Angle - 5 ;
        /*if(joint3Angle >= -180 && joint3Angle < 110)
        {
            joint3Angle = joint3Angle - 90;
        }
        else if(joint3Angle >= 110 && joint3Angle <= 180)
        {
            joint3Angle = joint3Angle + 270;
        }
        else if(joint3Angle > 180 && joint3Angle <= 290)
        {
            joint3Angle = joint3Angle + 270;
        }
        else if(joint3Angle > 290 && joint3Angle <=360)
        {
            joint3Angle = joint3Angle - 290;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.Keypad3) || Input.GetKey(KeyCode.Return))
        {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(joint3Angle, 0, 0), 0.01f);///调用Joint2Contl中的旋转角度抵消臂1对臂2坐标的影响
        }*/
        if (Input.GetKey(KeyCode.Keypad3) || Input.GetKey(KeyCode.Return))
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90), 0.01f);
            if (joint3Angle >= 0 && joint3Angle <= 90)//到了指定角度就停止旋转
            {
                if (this.transform.localEulerAngles.x >= joint3Angle -0.5)
                    j3RotationSpeedX = 0;
                transform.Rotate(new Vector3(j3RotationSpeedX, j3RotationSpeedY, j3RotationSpeedZ) * Time.deltaTime);
                print("旋转了：" + this.transform.localEulerAngles.x);
            }
            else if (joint3Angle > 90 && joint3Angle <= 180)//到了指定角度就停止旋转
            {
                if (this.transform.localEulerAngles.x >= joint3Angle - (joint3Angle - 90))
                    j3RotationSpeedX = 0;
                transform.Rotate(new Vector3(j3RotationSpeedX, j3RotationSpeedY, j3RotationSpeedZ) * Time.deltaTime);
                print("旋转了：" + this.transform.localEulerAngles.x);
            }
        }
    }
}
