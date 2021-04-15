using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 用于设置交接角度
/// </summary>
public class JointControl : MonoBehaviour
{

    public static GameObject[] joint = new GameObject[5];
    //public static GameObject joint1;
    //public static GameObject joint2;
    //public static GameObject joint3;
    //public static GameObject joint4;
    //public static GameObject joint5;

    private void Awake()
    {
        for (int i = 0; i < joint.Length; i++)
        {
            joint[i] = GameObject.FindGameObjectWithTag($"Joint{i + 1}");
        }
        //joint1 = GameObject.FindGameObjectWithTag("Joint1");
        //joint2 = GameObject.FindGameObjectWithTag("Joint2");
        //joint3 = GameObject.FindGameObjectWithTag("Joint3");
        //joint4 = GameObject.FindGameObjectWithTag("Joint4");
        //joint5 = GameObject.FindGameObjectWithTag("Joint5");
    }


    //int jointAngle1, int jointAngle2, int jointAngle3, int jointAngle4, int jointAngle5
    static public void SetJointAngle(int[] jointAngle)
    {

        //joint1.GetComponent<Joint1Contol>().joint1Angle = jointAngle1;
        //joint2.GetComponent<Joint2Contol>().joint2Angle = jointAngle2;
        //joint3.GetComponent<Joint3Contol>().joint3Angle = jointAngle3;
        //joint4.GetComponent<Joint4Contol>().joint4Angle = jointAngle4;
        //joint5.GetComponent<Joint5Contol>().joint5Angle = jointAngle5;

        joint[0].GetComponent<Joint1Contol>().joint1Angle = jointAngle[0];
        joint[1].GetComponent<Joint2Contol>().joint2Angle = jointAngle[1];
        joint[2].GetComponent<Joint3Contol>().joint3Angle = jointAngle[2];
        joint[3].GetComponent<Joint4Contol>().joint4Angle = jointAngle[3];
        joint[4].GetComponent<Joint5Contol>().joint5Angle = jointAngle[4];
    }
}

