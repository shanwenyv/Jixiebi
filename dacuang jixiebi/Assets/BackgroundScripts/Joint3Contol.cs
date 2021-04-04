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
        //joint3Angle = joint3Angle - 90;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad3))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(joint3Angle, other1.joint1Angle, 0), 0.01f);///调用Joint2Contl中的旋转角度抵消臂1对臂2坐标的影响
        }
    }
}
