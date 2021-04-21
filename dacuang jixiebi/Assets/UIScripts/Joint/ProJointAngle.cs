using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProJointAngle : MonoBehaviour
{
    public GameObject proui;
    public static GameObject[] anglePro = new GameObject[5];

    private void Awake()
    {
        proui = GameObject.FindGameObjectWithTag("ProgrammePanel");
        for (int i = 0; i < anglePro.Length; i++)
        {
            anglePro[i] = FindChildGameObject($"InputJointAngle{i + 1}");
        }
    }
    /// <summary>
    /// 展示当前状态的关节值
    /// </summary>
    public static void ShowStateAngle()
    {
        for (int i = 0; i < 5; i++)
        {
            anglePro[i].GetComponent<Text>().text = $"{ProgrammePanel.stateAngle[ProgrammePanel.state, i]}";
        }
    }

    public GameObject FindChildGameObject(string name)
    {
        Transform[] trans = proui.GetComponentsInChildren<Transform>();

        foreach (Transform item in trans)
        {
            if (item.name == name)
            {
                return item.gameObject;
            }
        }

        Debug.LogWarning($"{proui.name}里找不到名为{name}的子对象");
        return null;
    }
}
