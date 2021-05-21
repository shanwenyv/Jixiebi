using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IRBAngleMessage : MonoBehaviour
{
    public GameObject meui;
    public GameObject[] rAngleMessage = new GameObject[4];

    //新机械臂控制信息
    #region
    static float[] rJointSpeed = new float[4];
    static float[] rJointSetAngle = new float[4];
    static float[] rJointAngle = new float[4];
    #endregion

    private void Awake()
    {
        meui = GameObject.FindGameObjectWithTag("MessagePanel2");
        for (int i = 0; i < rAngleMessage.Length; i++)
        {
            rAngleMessage[i] = FindChildGameObject($"Angle{i + 1}");
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
        rJointSetAngle[0] = IRBJointControl.rJoint[0].GetComponent<IRB4600Joint1Contol>().irb4600joint1Angle;
        rJointSetAngle[1] = IRBJointControl.rJoint[1].GetComponent<IRB4600Joint2Contol>().irb4600joint2Angle;
        rJointSetAngle[2] = IRBJointControl.rJoint[2].GetComponent<IRB4600Joint3Contol>().irb4600joint3Angle;
        rJointSetAngle[3] = IRBJointControl.rJoint[3].GetComponent<IRB4600Joint4Contol>().irb4600joint4Angle;
    }

    /// <summary>
    /// 获取各个关节的角度
    /// </summary>
    public void GetAngle()
    {
        rJointAngle[0] = GetAngleZ(IRBJointControl.rJoint[0].transform.localRotation.eulerAngles.y, 0);
        rJointAngle[1] = GetAngleZ(IRBJointControl.rJoint[1].transform.localRotation.eulerAngles.z, 0);
        rJointAngle[2] = GetAngleZ(IRBJointControl.rJoint[2].transform.localRotation.eulerAngles.z, 0);
        rJointAngle[3] = GetAngleZ(IRBJointControl.rJoint[3].transform.localRotation.eulerAngles.x, 0);
    }

    public void ShowAngle()
    {
        for (int i = 0; i < rJointAngle.Length; i++)
        {
            rAngleMessage[i].GetComponent<Text>().text = $"{rJointAngle[i]}";
        }
        //clawMessage.GetComponent<Text>().text = MechanicalClawContol.grabUP.ToString();
    }

    /// <summary>
    /// 四舍五入JonintAngle
    /// </summary>
    /// <returns></returns>
    public void setAngle()
    {


        for (int i = 0; i < rJointAngle.Length; i++)
        {
            float Angle = rJointSetAngle[i] - rJointAngle[i];
            if (Angle <= 0.6 && Angle >= -0.6)
            {
                rJointAngle[i] = rJointSetAngle[i];
            }
        }

    }


    /// <summary>
    /// 关节15的信息修正
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public float GetAngleZ(float value, int i)
    {
        float angle = value - 180;
        float ang = 0;
        if (angle > 0)
            ang = angle - 180;
        else
            ang = angle + 180;
        if (rJointSetAngle[i] >= 0 && ang < 0)
            return ang + 360;
        if (rJointSetAngle[i] < 0 && ang > 0)
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
            return IRBJointControl.rJoint[0].GetComponent<IRB4600Joint1Contol>().irb4600j1RotationSpeedY;
        if (n == 2)
            return IRBJointControl.rJoint[1].GetComponent<IRB4600Joint2Contol>().irb4600j2RotationSpeedZ;
        if (n == 3)
            return IRBJointControl.rJoint[2].GetComponent<IRB4600Joint3Contol>().irb4600j3RotationSpeedZ;
        if (n == 4)
            return IRBJointControl.rJoint[3].GetComponent<IRB4600Joint4Contol>().irb4600j4RotationSpeedX;
        return 0;
    }

    /// <summary>
    /// 紧急停止，将关节速度变为0
    /// </summary>
    static public void SetSpeedZero()
    {
        IRBJointControl.rJoint[0].GetComponent<IRB4600Joint1Contol>().irb4600j1RotationSpeedY = 0;
        IRBJointControl.rJoint[1].GetComponent<IRB4600Joint2Contol>().irb4600j2RotationSpeedZ = 0;
        IRBJointControl.rJoint[2].GetComponent<IRB4600Joint3Contol>().irb4600j3RotationSpeedZ = 0;
        IRBJointControl.rJoint[3].GetComponent<IRB4600Joint4Contol>().irb4600j4RotationSpeedX = 0;
    }

    /// <summary>
    /// 获取关节的速度
    /// </summary>
    /// <param name="speed"></param>
    static public void GetAngleSpeed()
    {
        for (int i = 0; i < rJointSpeed.Length; i++)
        {
            rJointSpeed[i] = GetSingleAngelSpeed(i + 1);
        }
    }

    /// <summary>
    /// 设置关节速度，用于紧急停止后的恢复运行
    /// </summary>
    static public void SetAngleSpeed()
    {

        IRBJointControl.rJoint[0].GetComponent<IRB4600Joint1Contol>().irb4600j1RotationSpeedY = rJointSpeed[0];
        IRBJointControl.rJoint[1].GetComponent<IRB4600Joint2Contol>().irb4600j2RotationSpeedZ = rJointSpeed[1];
        IRBJointControl.rJoint[2].GetComponent<IRB4600Joint3Contol>().irb4600j3RotationSpeedZ = rJointSpeed[2];
        IRBJointControl.rJoint[3].GetComponent<IRB4600Joint4Contol>().irb4600j4RotationSpeedX = rJointSpeed[3];


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
        if (rJointSetAngle[i] >= 0 && vector3.x < 0)
            return 360 + vector3.x;
        if (rJointSetAngle[i] < 0 && vector3.x > 0)
            return vector3.x - 360;
        return vector3.x;
    }


}
