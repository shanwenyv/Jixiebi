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
    #region
    Button exitButton;
    Button messageButton;
    Button setButton;
    Button pasueButton;
    Button programmeButton;
    Button automaticButton;
    Button initializeButton;
    #endregion


    public override void OnEnter()
    {
        pasueButton = UITool.GetOrAddComponentInChildren<Button>("PauseAndResumeButton");
        exitButton = UITool.GetOrAddComponentInChildren<Button>("ExitButton");
        messageButton = UITool.GetOrAddComponentInChildren<Button>("MessageButton");
        setButton = UITool.GetOrAddComponentInChildren<Button>("SetButton");
        programmeButton = UITool.GetOrAddComponentInChildren<Button>("ProgrammeButton");
        automaticButton = UITool.GetOrAddComponentInChildren<Button>("AutomaticButton");
        initializeButton = UITool.GetOrAddComponentInChildren<Button>("InitializeButton");

        UITool.GetOrAddComponentInChildren<Button>("ExitButton").onClick.AddListener(() =>
        {
            GameRoot.Instance.SceneSystem.SetScene(new StartScene());
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
        UITool.GetOrAddComponentInChildren<Button>("ProgrammeButton").onClick.AddListener(() =>
        {
            PanelManager.Push(new ProgrammePanel());
        });
        UITool.GetOrAddComponentInChildren<Button>("InitializeButton").onClick.AddListener(() =>
        {
            JointInitiaze.Initiaze();
            if (JointControl.Angles)
                JointInitiaze.InitiazeReturn();
            JointControl.ResetAngles();
        });
        UITool.GetOrAddComponentInChildren<Button>("AutomaticButton").onClick.AddListener(() =>
        {
            JointControl.automatic = true;
        });
        UITool.GetOrAddComponentInChildren<Button>("GuideButton").onClick.AddListener(() =>
        {
            if (JointControl.guide.activeInHierarchy == false)
                JointControl.guide.SetActive(true);
            else
                JointControl.guide.SetActive(false);
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
            programmeButton.gameObject.SetActive(false);
            automaticButton.gameObject.SetActive(false);
            initializeButton.gameObject.SetActive(false);
            UITool.GetOrAddComponentInChildren<Text>("ShowAndhide").text = "显示图标";
        }
        else
        {
            exitButton.gameObject.SetActive(true);
            messageButton.gameObject.SetActive(true);
            setButton.gameObject.SetActive(true);
            pasueButton.gameObject.SetActive(true);
            programmeButton.gameObject.SetActive(true);
            automaticButton.gameObject.SetActive(true);
            initializeButton.gameObject.SetActive(true);
            UITool.GetOrAddComponentInChildren<Text>("ShowAndhide").text = "隐藏图标";
        }
    }

}
