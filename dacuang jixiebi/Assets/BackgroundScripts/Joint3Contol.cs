/*******************************
 * 因为物体角度大于180度时，实际角度为 360+真实角度 ,所以在判定时需要进行人为的校准
 *****************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class Joint3Contol : MonoBehaviour
{
    private int t = 0;
    private int a = 0;
    public static Joint3Contol Instance3;
    public float joint3Angle = 90;//关节2旋转角度
    //关节旋转角度
    public float j3RotationSpeedX = 30;//设定初始运动速度
    public float j3RotationSpeedY = 0;
    public float j3RotationSpeedZ = 0;
    public float joint3AngleAbjust = 0;//在运动代码部分，当旋转角度大于180度时，校准度数
    public float joint3SpeedAbjust = 0;//在重置速度部分，当旋转角度大于180度时，校准度数
    private void Awake()
    {
        Instance3 = this;                            //单例模式
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
        if (joint3Angle < 0)
            j3RotationSpeedX = -30;
    }

    void Update()
    {
        print("旋转了" + GetInspectorRotationValueMethod(transform));
        float currentRotateX = transform.eulerAngles.x;
        if (currentRotateX > 180)
        {
            currentRotateX = currentRotateX - 360;//这样-5度还是-5度 而不是355！
        }
        if (Input.GetKey(KeyCode.Keypad3) || Input.GetKey(KeyCode.Return))
        {
            transform.Rotate(new Vector3(j3RotationSpeedX, j3RotationSpeedY, j3RotationSpeedZ) * Time.deltaTime);
            if (GetInspectorRotationValueMethod(transform) < 0 && joint3Angle > 0)//如果做正数角度旋转且旋转角度大于180度，则矫正角度到真实角度
            {
                joint3AngleAbjust = 360 + GetInspectorRotationValueMethod(transform);
            }
            if (GetInspectorRotationValueMethod(transform) > 0 && joint3Angle < 0)//如果做负数角度旋转且旋转角度小于于-180度，则矫正角度到真实角度
            {
                joint3AngleAbjust = GetInspectorRotationValueMethod(transform) - 360;
            }
            //print("旋转了：" + GetInspectorRotationValueMethod(transform));//输出旋转角度
            if (joint3Angle >= 0)//当设定角度大于初始角度时,机械臂做正方向运动
            {
                //print("旋转了：" + this.transform.localEulerAngles.z);
                if (j3RotationSpeedX > 0)
                {
                    if (GetInspectorRotationValueMethod(transform) > 0)//角度在0到180之间时，正常旋转
                    {
                        if (GetInspectorRotationValueMethod(transform) >= joint3Angle + 0.5)
                        {
                            j3RotationSpeedX = 0;

                        }
                    }
                    else if (GetInspectorRotationValueMethod(transform) < 0)//角度在180到360之间时，先通过角度校准，再进行旋转
                    {
                        if (joint3AngleAbjust >= joint3Angle + 0.5)
                        {
                            j3RotationSpeedX = 0;

                        }
                    }
                }
                else if (j3RotationSpeedX < 0)//当设定角度小于初始角度时
                {
                    if (GetInspectorRotationValueMethod(transform) > 0)//角度在0到180之间时，正常旋转
                    {
                        if (GetInspectorRotationValueMethod(transform) <= joint3Angle + 0.5)
                        {
                            j3RotationSpeedX = 0;
                        }
                    }
                    else if (GetInspectorRotationValueMethod(transform) < 0)//角度在180到360之间时，先通过角度校准，再进行旋转
                    {
                        if (joint3AngleAbjust <= joint3Angle + 0.5)
                        {
                            j3RotationSpeedX = 0;

                        }
                    }
                }
            }
            else if (joint3Angle <= 0)//当设定角度小于初始角度时,机械臂做负方向运动
            {
                if (j3RotationSpeedX < 0)//机械臂做逆时针转动
                {
                    if (GetInspectorRotationValueMethod(transform) < 0)//当旋转角度在0到-180度时
                    {
                        if (GetInspectorRotationValueMethod(transform) < joint3Angle)
                        {
                            j3RotationSpeedX = 0;
                        }
                    }
                    else if (GetInspectorRotationValueMethod(transform) > 0)//当旋转角度在-180度到-360度时
                    {
                        if (joint3AngleAbjust < joint3Angle)
                        {
                            j3RotationSpeedX = 0;
                        }
                    }
                }
                else if (j3RotationSpeedX > 0)//机械臂做顺时针转动
                {
                    if (GetInspectorRotationValueMethod(transform) < 0)//当旋转角度在0到-180度时
                    {
                        if (GetInspectorRotationValueMethod(transform) > joint3Angle)
                        {
                            j3RotationSpeedX = 0;
                        }
                    }
                    if (GetInspectorRotationValueMethod(transform) > 0)//当旋转角度在-180度到-360度时
                    {
                        if (joint3AngleAbjust > joint3Angle)
                        {
                            j3RotationSpeedX = 0;
                        }
                    }
                }
            }
        }
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            j3RotationSpeedX = 30;//按下小键盘回车，重置速度初始值
            joint3SpeedAbjust = 0;//重置速度中判断角度调整值
            joint3AngleAbjust = 0;//重置旋转角度调整值
            if (joint3Angle > 0)
            {
                if (GetInspectorRotationValueMethod(transform) < 0)//如果物体角度大于180度，则校准角度
                {
                    t = 1;
                    joint3SpeedAbjust = 360 + GetInspectorRotationValueMethod(transform);

                }
                if (joint3Angle < GetInspectorRotationValueMethod(transform) - 0.5)//物体大于180度时，校准角度
                {
                    j3RotationSpeedX = -j3RotationSpeedX;
                }
                else if (joint3Angle < joint3SpeedAbjust)
                {
                    j3RotationSpeedX = -j3RotationSpeedX;
                }
            }
            else if (joint3Angle < 0)
            {
                j3RotationSpeedX = -30;
                if (GetInspectorRotationValueMethod(transform) > 0)//如果物体角度小于-180度，则校准角度
                {
                    joint3SpeedAbjust = GetInspectorRotationValueMethod(transform) - 360;//校准角度
                }
                if (joint3SpeedAbjust < joint3Angle)
                {
                    j3RotationSpeedX = -j3RotationSpeedX;
                }
                if (joint3Angle > GetInspectorRotationValueMethod(transform))
                {
                    j3RotationSpeedX = -j3RotationSpeedX;
                }
            }
        }
    }
}
