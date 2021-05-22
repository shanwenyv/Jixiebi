using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 显示各个关节信息的面板
/// </summary>
public class MessagePanel2 : BasePanel
{

    static readonly string path = "Perfabs/UI/Panel/MessagePanel2";
    public MessagePanel2() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("ExitButton").onClick.AddListener(() =>
        {
            PanelManager.Pop();
        });
    }
}
