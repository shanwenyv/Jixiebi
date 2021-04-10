using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始主面板
/// </summary>
public class StartPanel : BasePanel
{
    static readonly string path = "Perfabs/UI/Panel/StartPanel";
    public StartPanel():base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("Start").onClick.AddListener(() =>
        {
            //点击事件可以写在这里面
            Debug.Log("开始按钮被点了");
            PanelManager.Push(new PeoplePanel());
            //PanelManager.Push(new BasePanel);
        });
    }

}
