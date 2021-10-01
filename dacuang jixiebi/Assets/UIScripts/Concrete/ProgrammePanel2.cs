using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgrammePanel2 : BasePanel
{
    public static int state = 0;
    /// <summary>
    /// 用于存储各个状态的的关节设定值
    /// </summary>
    public static float[,] stateAngle = new float[6, 5]{ {1, 1, 1, 1, 1 } , {2, 2, 2, 2, 2} , {3, 3, 3, 3, 3},
                                                         {4, 4, 4, 4, 4 } , {5, 5, 5, 5, 5} , {6, 6, 6, 6, 6} };
    public static float[] textAngle = new float[5];
    public static bool[] claw = new bool[6] { false, false, false, false, false, false };


    static readonly string path = "Perfabs/UI/Panel/ProgrammePanel2";
    public ProgrammePanel2() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("ExitButton").onClick.AddListener(() =>
        {
            PanelManager.Pop();
        });
        UITool.GetOrAddComponentInChildren<Button>("ApplyButton").onClick.AddListener(() =>
        {
            GetTextAngle();
            ApplyStateAngle();
            ProJointAngle.ShowStateAngle();
        });
        UITool.GetOrAddComponentInChildren<Button>("DeleteButton").onClick.AddListener(() =>
        {
            DeleteStateAnlge();
            ProJointAngle.ShowStateAngle();
        });
        UITool.GetOrAddComponentInChildren<Button>("Angle1Button").onClick.AddListener(() =>
        {
            state = 0;
            ProJointAngle.ShowStateAngle();
        });
        UITool.GetOrAddComponentInChildren<Button>("Angle2Button").onClick.AddListener(() =>
        {
            state = 1;
            ProJointAngle.ShowStateAngle();
        });
        UITool.GetOrAddComponentInChildren<Button>("Angle3Button").onClick.AddListener(() =>
        {
            state = 2;
            ProJointAngle.ShowStateAngle();
        });
        UITool.GetOrAddComponentInChildren<Button>("Angle4Button").onClick.AddListener(() =>
        {
            state = 3;
            ProJointAngle.ShowStateAngle();
        });
        UITool.GetOrAddComponentInChildren<Button>("Angle5Button").onClick.AddListener(() =>
        {
            state = 4;
            ProJointAngle.ShowStateAngle();
        });
        UITool.GetOrAddComponentInChildren<Button>("Angle6Button").onClick.AddListener(() =>
        {
            state = 5;
            ProJointAngle.ShowStateAngle();
        });
        UITool.GetOrAddComponentInChildren<Button>("ON").onClick.AddListener(() =>
        {
            claw[state] = true;
            ProJointAngle.ShowClawState();
        });
        UITool.GetOrAddComponentInChildren<Button>("OFF").onClick.AddListener(() =>
        {
            claw[state] = false;
            ProJointAngle.ShowClawState();
        });
    }

    /// <summary>
    /// 获取文本设置关节的值
    /// </summary>
    public void GetTextAngle()
    {
        for (int i = 0; i < textAngle.Length; i++)
        {
            float.TryParse(UITool.GetOrAddComponentInChildren<Text>($"InputJoint{i + 1}Angle").text, out textAngle[i]);
        }
    }

    /// <summary>
    /// 保存当前状态储存的关节值
    /// </summary>
    public void ApplyStateAngle()

    {
        for (int i = 0; i < 5; i++)
        {
            stateAngle[state, i] = textAngle[i];
        }
    }

    /// <summary>
    /// 删除当前状态储存的关节值
    /// </summary>
    public void DeleteStateAnlge()
    {
        for (int i = 0; i < 5; i++)
        {
            stateAngle[state, i] = 0;
        }
        claw[state] = false;
    }

}
