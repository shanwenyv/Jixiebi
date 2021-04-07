
/*****************************************************
2021.4.7更新日志：完成了110°-180°的校准
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint3Contol : MonoBehaviour
{
    public float joint3Angle = 90;//关节3旋转角度
    // Start is called before the first frame update
    public Joint1Contol other1;

    void Start()
    {
        
        //joint3Angle = joint3Angle - 5 ;
        if(joint3Angle >= -90 && joint3Angle < 110)
        {
            //joint3Angle = joint3Angle + 70;
        }
        else if(joint3Angle >= 110 && joint3Angle <= 180)
        {
            joint3Angle = joint3Angle + 270;
        }
        else if(joint3Angle > 180 && joint3Angle <= 290)
        {
            //joint3Angle = joint3Angle + 170;
        }
        else if(joint3Angle > 290 && joint3Angle <=360)
        {
            joint3Angle = joint3Angle - 290;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad3) || Input.GetKey(KeyCode.Return))
        {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(joint3Angle, other1.joint1Angle, 0), 0.01f);///调用Joint2Contl中的旋转角度抵消臂1对臂2坐标的影响
        }
    }
}
