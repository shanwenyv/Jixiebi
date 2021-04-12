using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Main场景主面板
/// </summary>
public class MainPanel : BasePanel
{
    static readonly string path = "Perfabs/UI/Panel/MainPanel";
    public MainPanel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("Exit").onClick.AddListener(() =>
        {
            GameRoot.Instance.SceneSystem.SetScene(new StartScene());
        });
        UITool.GetOrAddComponentInChildren<Button>("Message").onClick.AddListener(() =>
        {
            PanelManager.Push(new MessagePanel());
        });

    }
}
