using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 用于设置交接角度
/// </summary>
public class IRBJointControl : MonoBehaviour
{
    public static GameObject guide;
    public static bool automatic = false;
    public static bool Angles = false;

    public static GameObject[] rJoint = new GameObject[4];

    private void Awake()
    {
        for (int i = 0; i < rJoint.Length; i++)
        {
            rJoint[i] = GameObject.FindGameObjectWithTag($"RJoint{i + 1}");
        }
        guide = GameObject.FindGameObjectWithTag("Guide");
    }

    /// <summary>
    /// 用于设置面板的设置角度
    /// </summary>
    /// <param name="jointAngle"></param>
    static public void SetJointAngle(float[] jointAngle)
    {
        rJoint[0].GetComponent<IRB4600Joint1Contol>().irb4600joint1Angle = jointAngle[0];
        rJoint[1].GetComponent<IRB4600Joint2Contol>().irb4600joint2Angle = jointAngle[1];
        rJoint[2].GetComponent<IRB4600Joint3Contol>().irb4600joint3Angle = jointAngle[2];
        rJoint[3].GetComponent<IRB4600Joint4Contol>().irb4600joint4Angle = jointAngle[3];
    }

    private void Update()
    {
        SetAngles();
        if (automatic == true)
        {
            Automatic();
            ClawControl();
            rJoint[0].GetComponent<IRB4600Joint1Contol>().IRB4600Joint1AutomationContol();
            rJoint[1].GetComponent<IRB4600Joint2Contol>().IRB4600Joint2AutomationContol();
            rJoint[2].GetComponent<IRB4600Joint3Contol>().IRB4600Joint3AutomationContol();
            rJoint[3].GetComponent<IRB4600Joint4Contol>().IRB4600Joint4AutomationContol();
        }
    }

    /// <summary>
    /// 自动运行
    /// </summary>
    static public void Automatic()
    {
        if (ProgrammePanel.state <= 5 && Angles == true)
        {
            float[] joint = new float[4];
            for (int i = 0; i < joint.Length; i++)
            {
                joint[i] = ProgrammePanel.stateAngle[ProgrammePanel.state, i];
            }
            SetJointAngle(joint);
            ResetAngles();
            ProgrammePanel.state++;
        }
        else if (ProgrammePanel.state > 5 && Angles == true)
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
        if (IRB4600Joint1Contol.irb4600joint1MotionOver == true && IRB4600Joint2Contol.irb4600joint2MotionOver == true &&
            IRB4600Joint3Contol.irb4600joint3MotionOver == true && IRB4600Joint4Contol.irb4600joint4MotionOver == true )
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
        rJoint[0].GetComponent<IRB4600Joint1Contol>().IRB4600Joint1Reset();
        rJoint[1].GetComponent<IRB4600Joint2Contol>().IRB4600Joint2Reset();
        rJoint[2].GetComponent<IRB4600Joint3Contol>().IRB4600Joint3Reset();
        rJoint[3].GetComponent<IRB4600Joint4Contol>().IRB4600Joint4Reset();
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

