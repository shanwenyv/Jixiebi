using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeoplePanel : BasePanel
{
    static readonly string path = "Perfabs/UI/Panel/PeoplePanel";
    public PeoplePanel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("BackButton").onClick.AddListener(() =>
        {
            //点击事件可以写在这里面
            PanelManager.Pop();
        });
    }

}
