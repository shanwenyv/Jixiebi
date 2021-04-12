using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理全局的SomeThing
/// </summary>
public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance { get; private set; }
    /// <summary>
    /// 场景管理器
    /// </summary>
    public SceneSystem SceneSystem { get; private set; }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        SceneSystem = new SceneSystem();

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneSystem.SetScene(new StartScene());
    }
}
