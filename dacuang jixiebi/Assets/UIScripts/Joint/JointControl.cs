using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 用于设置交接角度
/// </summary>
public class JointControl : MonoBehaviour
{

    public static GameObject[] joint = new GameObject[5];

    private void Awake()
    {
        for (int i = 0; i < joint.Length; i++)
        {
            joint[i] = GameObject.FindGameObjectWithTag($"Joint{i + 1}");
        }
    }

    /// <summary>
    /// 用于设置面板的设置角度
    /// </summary>
    /// <param name="jointAngle"></param>
    static public void SetJointAngle(int[] jointAngle)
    {
        joint[0].GetComponent<Joint1Contol>().joint1Angle = jointAngle[0];
        joint[1].GetComponent<Joint2Contol>().joint2Angle = jointAngle[1];
        joint[2].GetComponent<Joint3Contol>().joint3Angle = jointAngle[2];
        joint[3].GetComponent<Joint4Contol>().joint4Angle = jointAngle[3];
        joint[4].GetComponent<Joint5Contol>().joint5Angle = jointAngle[4];
    }
 }

