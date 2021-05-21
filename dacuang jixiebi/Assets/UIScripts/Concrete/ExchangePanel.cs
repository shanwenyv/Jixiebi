using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExchangePanel : BasePanel
{
    static readonly string path = "Perfabs/UI/Panel/ExchangePanel";

    public ExchangePanel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("UR5Button").onClick.AddListener(() =>
        {
            if (SceneManager.GetActiveScene().name == "SampleScene")
                SceneManager.LoadScene("SampleScene");
            GameRoot.Instance.SceneSystem.SetScene(new MainScene());
        });
        UITool.GetOrAddComponentInChildren<Button>("IRB4600Button").onClick.AddListener(() =>
        {
            if (SceneManager.GetActiveScene().name == "SampleScene2")
                SceneManager.LoadScene("SampleScene2");
            GameRoot.Instance.SceneSystem.SetScene(new SecondScene());
        });
        UITool.GetOrAddComponentInChildren<Button>("ExitButton").onClick.AddListener(() =>
        {
            PanelManager.Pop();
        });
    }
}
