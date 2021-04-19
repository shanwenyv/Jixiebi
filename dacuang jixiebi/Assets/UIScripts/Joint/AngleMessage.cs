using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class AngleMessage : MonoBehaviour
{
    public GameObject meui;
    public GameObject[] angleMessage = new GameObject[5];
    static float[] jointSpeed = new float[5];
    static float[] jointSetAngle = new float[5];
    static float[] jointAngle = new float[5];

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
        GetAngleSpeed();
        GetAngle();
        GetSetJointAngle();
        setAngle();
        ShowAngle();
    }

    public void GetSetJointAngle()
    {
        jointSetAngle[0] = JointControl.joint[0].GetComponent<Joint1Contol>().joint1Angle;
        jointSetAngle[1] = JointControl.joint[1].GetComponent<Joint2Contol>().joint2Angle;
        jointSetAngle[2] = JointControl.joint[2].GetComponent<Joint3Contol>().joint3Angle;
        jointSetAngle[3] = JointControl.joint[3].GetComponent<Joint4Contol>().joint4Angle;
        jointSetAngle[4] = JointControl.joint[4].GetComponent<Joint5Contol>().joint5Angle;
    }

    /// <summary>
    /// 获取各个关节的角度
    /// </summary>
    public void GetAngle()
    {
        jointAngle[0] = GetAngleZ(JointControl.joint[0].transform.localRotation.eulerAngles.z, 0);
        jointAngle[1] = GetInspectorRotationValueMethod(Joint2Contol.Instance2.transform, 1);
        jointAngle[2] = GetInspectorRotationValueMethod(Joint3Contol.Instance3.transform, 2);
        jointAngle[3] = GetInspectorRotationValueMethod(Joint4Contol.Instance4.transform, 3);
        jointAngle[4] = GetAngleZ(JointControl.joint[4].transform.localRotation.eulerAngles.z, 4);
    }

    public void ShowAngle()
    {
        for (int i = 0; i < jointAngle.Length; i++)
        {
            angleMessage[i].GetComponent<Text>().text = $"{jointAngle[i]}";
        }
    }

    /// <summary>
    /// 四舍五入JonintAngle
    /// </summary>
    /// <returns></returns>
    public void setAngle()
    {
        for (int i = 0; i < jointAngle.Length; i++)
        {
            float Angle = jointSetAngle[i] - jointAngle[i]; 
            if (Angle <= 0.5 && Angle >= -0.5)
            {
                jointAngle[i] = jointSetAngle[i];
            }
        }
    }


    /// <summary>
    /// 关节15的信息修正
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public float GetAngleZ(float value,int i)
    {
        float angle = value - 180;
        float ang = 0;
        if (angle > 0) 
            ang = angle - 180;
        else
            ang  = angle + 180;
        if (jointSetAngle[i] > 0 && ang < 0)
            return ang + 360;
        if (jointSetAngle[i] < 0 && ang > 0)
            return ang - 360;
        return ang; 
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
        for (int i = 0; i < jointSpeed.Length; i++)
        {
            jointSpeed[i] = GetSingleAngelSpeed(i + 1);
        }
    }

    /// <summary>
    /// 设置关节速度，用于紧急停止后的恢复运行
    /// </summary>
    static public void SetAngleSpeed()
    {
        JointControl.joint[0].GetComponent<Joint1Contol>().j1RotationSpeedZ = jointSpeed[0];
        JointControl.joint[1].GetComponent<Joint2Contol>().j2RotationSpeedX = jointSpeed[1];
        JointControl.joint[2].GetComponent<Joint3Contol>().j3RotationSpeedX = jointSpeed[2];
        JointControl.joint[3].GetComponent<Joint4Contol>().j4RotationSpeedX = jointSpeed[3];
        JointControl.joint[4].GetComponent<Joint5Contol>().j5RotationSpeedZ = jointSpeed[4];
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

    public float GetInspectorRotationValueMethod(Transform transform, int i)
    {
        /*******************************
        // 获取j角度原生值
        *******************************/
        System.Type transformType = transform.GetType();
        PropertyInfo m_propertyInfo_rotationOrder = transformType.GetProperty("rotationOrder", BindingFlags.Instance | BindingFlags.NonPublic);
        object m_OldRotationOrder = m_propertyInfo_rotationOrder.GetValue(transform, null);
        MethodInfo m_methodInfo_GetLocalEulerAngles = transformType.GetMethod("GetLocalEulerAngles", BindingFlags.Instance | BindingFlags.NonPublic);
        object value = m_methodInfo_GetLocalEulerAngles.Invoke(transform, new object[] { m_OldRotationOrder });
        string temp = value.ToString();
        //将字符串第一个和最后一个去掉
        temp = temp.Remove(0, 1);
        temp = temp.Remove(temp.Length - 1, 1);
        //用‘，’号分割
        string[] tempVector3;
        tempVector3 = temp.Split(',');
        //将分割好的数据传给Vector3
        Vector3 vector3 = new Vector3(float.Parse(tempVector3[0]), float.Parse(tempVector3[1]), float.Parse(tempVector3[2]));
        if (jointSetAngle[i] >= 0 && vector3.x < 0)
            return 360 + vector3.x;
        if (jointSetAngle[i] < 0 && vector3.x > 0) 
        return vector3.x - 360;
        return vector3.x;
    }
}
