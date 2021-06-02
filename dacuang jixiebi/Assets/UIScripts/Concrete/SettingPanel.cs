using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    float[] angle = new float[6];

    static readonly string path = "Perfabs/UI/Panel/SettingPanel";
    public SettingPanel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("ExitButton").onClick.AddListener(() =>
        {
            PanelManager.Pop();
        });
        UITool.GetOrAddComponentInChildren<Button>("ON").onClick.AddListener(() =>
        {
            MechanicalClawContol.Up();
        });
        UITool.GetOrAddComponentInChildren<Button>("OFF").onClick.AddListener(() =>
        {
            MechanicalClawContol.Down();
        });
        UITool.GetOrAddComponentInChildren<Button>("ApplyButton").onClick.AddListener(() =>
        {
            for (int i = 0; i < angle.Length; i++)
            {
                angle[i] = int.Parse(UITool.GetOrAddComponentInChildren<Text>($"InputJoint{i + 1}Angle").text);
            }
            //angle[0], angle[1],angle[2],angle[3],angle[4]
            JointInitiaze.jointIntiazeBool = false;
            JointControl.SetJointAngle(angle);
            JointControl.ResetAngles();
        });
    }
}
