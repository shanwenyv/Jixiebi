using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint1Contol : MonoBehaviour
{
    public float joint1Angle = 90;//关节1旋转角度


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Return))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-90, 0, joint1Angle), 0.01f);
        }
    }
}
