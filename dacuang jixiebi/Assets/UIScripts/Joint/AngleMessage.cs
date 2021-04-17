using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleMessage : MonoBehaviour
{
    public GameObject meui;
    public GameObject[] angleMessage = new GameObject[5];
    static float[] JointSpeed = new float[5];

    private void Awake()
    {
        meui = GameObject.FindGameObjectWithTag("MessagePanel");
        for (int i = 0; i < angleMessage.Length; i++)
        {
            angleMessage[i] = FindChildGameObject($"Angle{i + 1}");
        }
    }

    void Update()
    {
        GetAngle();
    }

    /// <summary>
    /// 获取各个关节的角度
    /// </summary>
    public void GetAngle()
    {
        angleMessage[0].GetComponent<Text>().text = $"{JointControl.joint[0].transform.localRotation.eulerAngles.z}";
        angleMessage[1].GetComponent<Text>().text = $"{JointControl.joint[1].transform.localRotation.eulerAngles.x}";
        angleMessage[2].GetComponent<Text>().text = $"{JointControl.joint[2].transform.localRotation.eulerAngles.x}";
        angleMessage[3].GetComponent<Text>().text = $"{JointControl.joint[3].transform.localRotation.eulerAngles.x}";
        angleMessage[4].GetComponent<Text>().text = $"{JointControl.joint[4].transform.localRotation.eulerAngles.z}";

    }


    /// <summary>
    /// 获取单个关节速度的值
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    static public float GetSingleAngelSpeed(int n)
    {
        if (n == 1)
            return JointControl.joint[0].GetComponent<Joint1Contol>().j1RotationSpeedZ;
        if (n == 2)
            return JointControl.joint[1].GetComponent<Joint2Contol>().j2RotationSpeedX;
        if (n == 3)
            return JointControl.joint[2].GetComponent<Joint3Contol>().j3RotationSpeedX;
        if (n == 4)
            return JointControl.joint[3].GetComponent<Joint4Contol>().j4RotationSpeedX;
        if (n == 5)
            return JointControl.joint[4].GetComponent<Joint5Contol>().j5RotationSpeedZ;
        return 0;
    }

    /// <summary>
    /// 紧急停止，将关节速度变为0
    /// </summary>
    static public void SetSpeedZero()
    {
        JointControl.joint[0].GetComponent<Joint1Contol>().j1RotationSpeedZ = 0;
        JointControl.joint[1].GetComponent<Joint2Contol>().j2RotationSpeedX = 0;
        JointControl.joint[2].GetComponent<Joint3Contol>().j3RotationSpeedX = 0;
        JointControl.joint[3].GetComponent<Joint4Contol>().j4RotationSpeedX = 0;
        JointControl.joint[4].GetComponent<Joint5Contol>().j5RotationSpeedZ = 0;
    }

    /// <summary>
    /// 获取关节的速度
    /// </summary>
    /// <param name="speed"></param>
    static public void GetAngleSpeed()
    {
        for (int i = 0; i < JointSpeed.Length; i++)
        {
            JointSpeed[i] = GetSingleAngelSpeed(i + 1);
        }
    }

    /// <summary>
    /// 设置关节速度，用于紧急停止后的恢复运行
    /// </summary>
    static public void SetAngleSpeed()
    {
        JointControl.joint[0].GetComponent<Joint1Contol>().j1RotationSpeedZ = JointSpeed[0];
        JointControl.joint[1].GetComponent<Joint2Contol>().j2RotationSpeedX = JointSpeed[1];
        JointControl.joint[2].GetComponent<Joint3Contol>().j3RotationSpeedX = JointSpeed[2];
        JointControl.joint[3].GetComponent<Joint4Contol>().j4RotationSpeedX = JointSpeed[3];
        JointControl.joint[4].GetComponent<Joint5Contol>().j5RotationSpeedZ = JointSpeed[4];
    }


    public GameObject FindChildGameObject(string name)
    {
        Transform[] trans = meui.GetComponentsInChildren<Transform>();

        foreach (Transform item in trans)
        {
            if (item.name == name)
            {
                return item.gameObject;
            }
        }

        Debug.LogWarning($"{meui.name}里找不到名为{name}的子对象");
        return null;
    }
}
