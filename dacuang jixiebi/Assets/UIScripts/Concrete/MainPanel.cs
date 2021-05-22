using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    Button guideButton;
    Button exchangeButton;
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
        guideButton = UITool.GetOrAddComponentInChildren<Button>("GuideButton");
        exchangeButton = UITool.GetOrAddComponentInChildren<Button>("ExchangeButton");

        UITool.GetOrAddComponentInChildren<Text>("GuideMessage").text = 
            "视角控制：\n" +
            "\u3000\u3000按下“W”“A”“S”“D”控制摄像机位\u3000\u3000置\n" +
            "\u3000\u3000滑动鼠标控制视角方向\n" +
            "\u3000\u3000按“F”锁定视角，按“T”解除锁定\n" +
            "机械臂控制：\n" +
            "\u3000\u3000按下小键盘数字键可让对应的关\u3000\u3000节独立运动\n" +
            "\u3000\u3000按下大键盘回车键可以让五个关\u3000\u3000节同时运动\n" +
            "\u3000\u3000按“F1”键可以开启机械臂末端控\u3000\u3000件，按“F2”可以关闭机械臂末端\u3000\u3000控件\n" +
            "\u3000\u3000按“Q”键可对机械臂关节角度进行\u3000\u3000初始化\n" +
            "\u3000\u3000关节设定角度更改完后需按下小\u3000\u3000键盘回车键才能继续操作\n";

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
        UITool.GetOrAddComponentInChildren<Button>("ExchangeButton").onClick.AddListener(() =>
        {
            PanelManager.Push(new ExchangePanel());
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
            guideButton.gameObject.SetActive(false);
            exchangeButton.gameObject.SetActive(false);
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
            guideButton.gameObject.SetActive(true);
            exchangeButton.gameObject.SetActive(true);
            UITool.GetOrAddComponentInChildren<Text>("ShowAndhide").text = "隐藏图标";
        }
    }

}
