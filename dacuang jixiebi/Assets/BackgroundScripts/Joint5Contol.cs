using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint5Contol : MonoBehaviour
{
    // Start is called before the first frame update
    public float joint5Angle = 90;//关节5旋转角度
    //关节旋转速度
    public float j5RotationSpeedX = 0;
    public float j5RotationSpeedY = 0;
    public float j5RotationSpeedZ = 30;

    // Start is called before the first frame update
    void Start()
    {
        //joint5Angle = joint5Angle - 90;
        if (joint5Angle <= 0)
        {
            j5RotationSpeedZ = -j5RotationSpeedZ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Keypad5) || Input.GetKey(KeyCode.Return))
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90), 0.01f);
            if(joint5Angle >= 0)
            {
                if (j5RotationSpeedZ > 0)
                {
                    if (this.transform.localEulerAngles.z >= joint5Angle - 0.5)
                    {
                        j5RotationSpeedZ = 0;
                    }
                }
                else if (j5RotationSpeedZ < 0)
                {
                    if (this.transform.localEulerAngles.z <= joint5Angle + 0.5)
                    {
                        j5RotationSpeedZ = 0;

                    }
                }
                transform.Rotate(new Vector3(j5RotationSpeedX, j5RotationSpeedY, j5RotationSpeedZ) * Time.deltaTime);
            }
            
            //print("旋转" + this.transform.localEulerAngles.z);
            

        }
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            j5RotationSpeedZ = 30;
            if (joint5Angle < this.transform.localEulerAngles.z + 0.5)
            {
                j5RotationSpeedZ = -j5RotationSpeedZ;
            }
        }
    }
}
