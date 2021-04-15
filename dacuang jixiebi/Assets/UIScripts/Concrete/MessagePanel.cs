using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 显示各个关节信息的面板
/// </summary>
public class MessagePanel : BasePanel
{

    static readonly string path = "Perfabs/UI/Panel/MessagePanel";
    public MessagePanel() : base(new UIType(path)) { }
        
    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("ExitButton").onClick.AddListener(() =>
        {
            PanelManager.Pop();
        });
    }
}
