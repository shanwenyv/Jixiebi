using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 用于设置交接角度
/// </summary>
public class JointControl : MonoBehaviour
{
    public static GameObject guide;
    public static bool automatic = false;
    public static bool Angles = false;

    public static GameObject[] joint = new GameObject[5];

    private void Awake()
    {
            for (int i = 0; i < joint.Length; i++)
            {
                joint[i] = GameObject.FindGameObjectWithTag($"Joint{i + 1}");
            }
            guide = GameObject.FindGameObjectWithTag("Guide");               
    }

    /// <summary>
    /// 用于设置面板的设置角度
    /// </summary>
    /// <param name="jointAngle"></param>
    static public void SetJointAngle(float[] jointAngle)
    {
        joint[0].GetComponent<Joint1Contol>().joint1Angle = jointAngle[0];
        joint[1].GetComponent<Joint2Contol>().joint2Angle = jointAngle[1];
        joint[2].GetComponent<Joint3Contol>().joint3Angle = jointAngle[2];
        joint[3].GetComponent<Joint4Contol>().joint4Angle = jointAngle[3];
        joint[4].GetComponent<Joint5Contol>().joint5Angle = jointAngle[4];
    }

    private void Update()
    {
        SetAngles();
        if (automatic == true)
        {
            Automatic();
            ClawControl();
            joint[0].GetComponent<Joint1Contol>().Joint1AutomationContol();
            joint[1].GetComponent<Joint2Contol>().Joint2AutomationContol();
            joint[2].GetComponent<Joint3Contol>().Joint3AutomationContol();
            joint[3].GetComponent<Joint4Contol>().Joint4AutomationContol();
            joint[4].GetComponent<Joint5Contol>().Joint5AutomationContol();
        }
    }

    /// <summary>
    /// 自动运行
    /// </summary>
    static public void Automatic()
    {
        if (ProgrammePanel.state <= 5&& Angles == true)
        {
            float[] joint = new float[5];
            for (int i = 0; i < joint.Length; i++)
            {
                joint[i] = ProgrammePanel.stateAngle[ProgrammePanel.state, i];
            }
            SetJointAngle(joint);
            ResetAngles();
            ProgrammePanel.state++;
        }
        else if(ProgrammePanel.state > 5&& Angles == true)
        {
            automatic = false;
            ProgrammePanel.state = 0;
        }
    }

    /// <summary>
    /// 设置控制自动化的布尔变量
    /// </summary>
    public static void SetAngles()
    {
        if (Joint1Contol.joint1MotionOver == true && Joint2Contol.joint2MotionOver == true && Joint3Contol.joint3MotionOver == true &&
             Joint4Contol.joint4MotionOver == true  && Joint5Contol.joint5MotionOver == true  )
        {
            Angles = true;
        }
        else
        {
            Angles = false;
        }
    }

    /// <summary>
    /// 重置所有设置角度
    /// </summary>
    public static void ResetAngles()
    {
        joint[0].GetComponent<Joint1Contol>().Joint1Reset();
        joint[1].GetComponent<Joint2Contol>().Joint2Reset();
        joint[2].GetComponent<Joint3Contol>().Joint3Reset();
        joint[3].GetComponent<Joint4Contol>().Joint4Reset();
        joint[4].GetComponent<Joint5Contol>().Joint5Reset();
    }

    /// <summary>
    /// 爪子控制
    /// </summary>
    public static void ClawControl()
    {
        if (ProgrammePanel.state < 6)
        {
            if (ProgrammePanel.claw[ProgrammePanel.state])
            {
                MechanicalClawContol.Up();
            }
            else
            {
                MechanicalClawContol.Down();
            }
        }
    }
}

