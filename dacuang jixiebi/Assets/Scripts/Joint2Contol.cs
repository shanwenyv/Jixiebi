using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint2Contol : MonoBehaviour
{
    public float joint2Angle = 90;//旋转角度

    public Joint1Contol other;
    // Start is called before the first frame update
    void Start()
    {
        joint2Angle = joint2Angle - 90;//旋转角度校准
        //joint2Angle = joint2Angle / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad2))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(joint2Angle, other.joint1Angle, 0), 0.01f);//调用Joint2Contl中的旋转角度抵消臂1对臂2坐标的影响
        }
    }
}
