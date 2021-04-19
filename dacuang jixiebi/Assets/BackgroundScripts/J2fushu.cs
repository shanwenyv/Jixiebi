/*******************************
 * 因为物体角度大于180度时，实际角度为 360+真实角度 ,所以在判定时需要进行人为的校准
 *****************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class J2fushu : MonoBehaviour
{
    // Start is called before the first frame update
    private int t = 0;
    private int a = 0;
    public static J2fushu Instance2;
    public float joint2Angle = 90;//关节2旋转角度
    //关节旋转角度
    public float j2RotationSpeedX = 30;//设定初始运动速度
    public float j2RotationSpeedY = 0;
    public float j2RotationSpeedZ = 0;
    public float joint2AngleAbjust = 0;//在运动代码部分，当旋转角度大于180度时，校准度数
    public float joint2SpeedAbjust = 0;//在重置速度部分，当旋转角度大于180度时，校准度数
    private void Awake()
    {
        Instance2 = this;                            //单例模式
    }
    public float GetInspectorRotationValueMethod(Transform transform)
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
        return vector3.x;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print("旋转了" + GetInspectorRotationValueMethod(transform));
        if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Return))
        {
            transform.Rotate(new Vector3(j2RotationSpeedX, j2RotationSpeedY, j2RotationSpeedZ) * Time.deltaTime);
        }
    }
}
