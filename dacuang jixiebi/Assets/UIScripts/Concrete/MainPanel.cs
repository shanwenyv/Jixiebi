using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Main场景主面板
/// </summary>
public class MainPanel : BasePanel
{
    bool pauseAndResume = true;
    static readonly string path = "Perfabs/UI/Panel/MainPanel";
    public MainPanel() : base(new UIType(path)) { }

    Button exitButton;
    Button messageButton;
    Button setButton;
    Button pasueButton;
    
    public override void OnEnter()
    {
        pasueButton = UITool.GetOrAddComponentInChildren<Button>("PauseAndResumeButton");
        exitButton = UITool.GetOrAddComponentInChildren<Button>("ExitButton");
        messageButton = UITool.GetOrAddComponentInChildren<Button>("MessageButton");
        setButton = UITool.GetOrAddComponentInChildren<Button>("SetButton");

        UITool.GetOrAddComponentInChildren<Button>("ExitButton").onClick.AddListener(() =>
        {
            GameRoot.Instance.SceneSystem.SetScene(new StartScene());
            Debug.Log("MAINSCENE中Exit按钮被点击");
        });
        UITool.GetOrAddComponentInChildren<Button>("MessageButton").onClick.AddListener(() =>
        {
            PanelManager.Push(new MessagePanel());
        });
        UITool.GetOrAddComponentInChildren<Button>("SetButton").onClick.AddListener(() =>
        {
            PanelManager.Push(new SettingPanel());
        });
        UITool.GetOrAddComponentInChildren<Button>("ShowAndhideButton").onClick.AddListener(() =>
        {
            ShowAndHide();
        });
        UITool.GetOrAddComponentInChildren<Button>("PauseAndResumeButton").onClick.AddListener(() =>
        {
            PauseAndResume();
        });
    }

    /// <summary>
    /// 紧急暂停和继续执行
    /// </summary>
    private void PauseAndResume()
    {
        if (pauseAndResume)
        {
            AngleMessage.GetAngleSpeed();
            AngleMessage.SetSpeedZero();
            UITool.GetOrAddComponentInChildren<Text>("PauseAndResume").text = "恢复运行";
            pauseAndResume = false;
        }
        else
        {
            AngleMessage.SetAngleSpeed();
            UITool.GetOrAddComponentInChildren<Text>("PauseAndResume").text = "紧急停止";
            pauseAndResume = true;
        }
    }

    /// <summary>
    /// 隐藏或显示主页面的所有按钮
    /// </summary>
    private void ShowAndHide()
    {
        if (exitButton.IsActive())
        {
            exitButton.gameObject.SetActive(false);
            messageButton.gameObject.SetActive(false);
            setButton.gameObject.SetActive(false);
            pasueButton.gameObject.SetActive(false);
            UITool.GetOrAddComponentInChildren<Text>("ShowAndhide").text = "显示图标";
        }
        else
        {
            exitButton.gameObject.SetActive(true);
            messageButton.gameObject.SetActive(true);
            setButton.gameObject.SetActive(true);
            pasueButton.gameObject.SetActive(true);
            UITool.GetOrAddComponentInChildren<Text>("ShowAndhide").text = "隐藏图标";
        }
    }
}
