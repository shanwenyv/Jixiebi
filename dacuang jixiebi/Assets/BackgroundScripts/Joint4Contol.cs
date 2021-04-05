using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint4Contol : MonoBehaviour
{
    // Start is called before the first frame update
    public float joint4Angle = 90;//关节4旋转角度
    // Start is called before the first frame update
    public Joint1Contol other4;

    void Start()
    {
        joint4Angle = 180 - joint4Angle;
        joint4Angle = -joint4Angle;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad4) || Input.GetKey(KeyCode.Return))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(joint4Angle, other4.joint1Angle, 0), 0.01f);//用关节1的旋转角度修正轴的偏移
        }
    }
}
