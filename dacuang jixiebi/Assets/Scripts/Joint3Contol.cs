using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint3Contol : MonoBehaviour
{
    public float joint3Angle = 180;//旋转角度
    // Start is called before the first frame update
    public Joint1Contol other1;

    void Start()
    {
        //joint3Angle = joint3Angle - 90;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(joint3Angle, other1.joint1Angle, 0), 0.01f);//other1.joint1Angle
        }
    }
}
