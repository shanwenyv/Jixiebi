/*******************************
 * 因为物体角度大于180度时，实际角度为 360+真实角度 ,所以在判定时需要进行人为的校准
 *****************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class Joint2Contol : MonoBehaviour
{
    private int t = 0;
    private int a = 0;
    public static Joint2Contol Instance2;
    public float joint2Angle = 90;//关节2旋转角度

    public static bool joint2Automatic = false;//关节2自动运行判定，否停止，真开始

    public static bool joint2MotionOver = false;//如果关节完成运动，其值为1，否则为0

    public float j2RotationSpeedX = 30;//设定初始运动速度
    public float j2RotationSpeedY = 0;
    public float j2RotationSpeedZ = 0;
    public float joint2AngleAbjust = 0;//在运动代码部分，当旋转角度大于180度时，校准度数
    public float joint2SpeedAbjust = 0;//在重置速度部分，当旋转角度大于180度时，校准度数

    public void Joint2Reset()//重置关节代码中的各项参数
    {
        j2RotationSpeedX = 30;//按下小键盘回车，重置速度初始值
        joint2SpeedAbjust = 0;//重置速度中判断角度调整值
        joint2AngleAbjust = 0;//重置旋转角度调整值
        if (joint2Angle >= 0)
        {
            if (GetInspectorRotationValueMethod(transform) < 0)//如果物体角度大于180度，则校准角度
            {
                t = 1;
                joint2SpeedAbjust = 360 + GetInspectorRotationValueMethod(transform);

            }
            if (joint2Angle < GetInspectorRotationValueMethod(transform) - 0.5)//物体大于180度时，校准角度
            {
                j2RotationSpeedX = -j2RotationSpeedX;
            }
            else if (joint2Angle < joint2SpeedAbjust)
            {
                j2RotationSpeedX = -j2RotationSpeedX;
            }
        }
        else if (joint2Angle <= 0)
        {
            j2RotationSpeedX = -30;
            if (GetInspectorRotationValueMethod(transform) > 0)//如果物体角度小于-180度，则校准角度
            {
                joint2SpeedAbjust = GetInspectorRotationValueMethod(transform) - 360;//校准角度
            }
            if (joint2SpeedAbjust < joint2Angle)
            {
                j2RotationSpeedX = -j2RotationSpeedX;
            }
            if (joint2Angle > GetInspectorRotationValueMethod(transform))
            {
                j2RotationSpeedX = -j2RotationSpeedX;
            }
        }
    }

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
        if (joint2Angle < 0)
            j2RotationSpeedX = -30;
    }

    void Update()
    {
        if (JointInitiaze.jointIntiazeBool == true)//初始化机械臂关节2角度
        {
            joint2Angle = 0;
        }
        //print("旋转了" + GetInspectorRotationValueMethod(transform));
        float currentRotateX = transform.eulerAngles.x;
        if (currentRotateX > 180)
        {
            currentRotateX = currentRotateX - 360;//这样-5度还是-5度 而不是355！
        }
        if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Return) || joint2Automatic == true)
        {
            transform.Rotate(new Vector3(j2RotationSpeedX, j2RotationSpeedY, j2RotationSpeedZ) * Time.deltaTime);
            if (GetInspectorRotationValueMethod(transform) < 0 && joint2Angle > 0)//如果做正数角度旋转且旋转角度大于180度，则矫正角度到真实角度
            {
                joint2AngleAbjust = 360 + GetInspectorRotationValueMethod(transform);
            }
            if (GetInspectorRotationValueMethod(transform) > 0 && joint2Angle < 0)//如果做负数角度旋转且旋转角度小于于-180度，则矫正角度到真实角度
            {
                joint2AngleAbjust = GetInspectorRotationValueMethod(transform) - 360;
            }
            //print("旋转了：" + GetInspectorRotationValueMethod(transform));//输出旋转角度
            if (joint2Angle >= 0)//当设定角度大于初始角度时,机械臂做正方向运动
            {
                //print("旋转了：" + this.transform.localEulerAngles.z);
                if (j2RotationSpeedX > 0)
                {
                    if (GetInspectorRotationValueMethod(transform) > 0)//角度在0到180之间时，正常旋转
                    {
                        if (GetInspectorRotationValueMethod(transform) >= joint2Angle + 0.5)
                        {
                            j2RotationSpeedX = 0;
                            joint2MotionOver = true;
                            joint2Automatic = false;
                        }
                    }
                    else if (GetInspectorRotationValueMethod(transform) < 0)//角度在180到360之间时，先通过角度校准，再进行旋转
                    {
                        if (joint2AngleAbjust >= joint2Angle + 0.5)
                        {
                            j2RotationSpeedX = 0;
                            joint2MotionOver = true;
                            joint2Automatic = false;
                        }
                    }
                }
                else if (j2RotationSpeedX < 0)//当设定角度小于初始角度时
                {
                    if (GetInspectorRotationValueMethod(transform) > 0)//角度在0到180之间时，正常旋转
                    {
                        if (GetInspectorRotationValueMethod(transform) <= joint2Angle + 0.5)
                        {
                            j2RotationSpeedX = 0;
                            joint2MotionOver = true;
                            joint2Automatic = false;
                        }
                    }
                    else if (GetInspectorRotationValueMethod(transform) < 0)//角度在180到360之间时，先通过角度校准，再进行旋转
                    {
                        if (joint2AngleAbjust <= joint2Angle + 0.5)
                        {
                            j2RotationSpeedX = 0;
                            joint2MotionOver = true;
                            joint2Automatic = false;
                        }
                    }
                }
            }
            else if (joint2Angle <= 0)//当设定角度小于初始角度时,机械臂做负方向运动
            {
                if (j2RotationSpeedX < 0)//机械臂做逆时针转动
                {
                    if (GetInspectorRotationValueMethod(transform) < 0)//当旋转角度在0到-180度时
                    {
                        if (GetInspectorRotationValueMethod(transform) < joint2Angle)
                        {
                            j2RotationSpeedX = 0;
                            joint2MotionOver = true;
                            joint2Automatic = false;
                        }
                    }
                    else if (GetInspectorRotationValueMethod(transform) > 0)//当旋转角度在-180度到-360度时
                    {
                        if (joint2AngleAbjust < joint2Angle)
                        {
                            j2RotationSpeedX = 0;
                            joint2MotionOver = true;
                            joint2Automatic = false;
                        }
                    }
                }
                else if (j2RotationSpeedX > 0)//机械臂做顺时针转动
                {
                    if (GetInspectorRotationValueMethod(transform) < 0)//当旋转角度在0到-180度时
                    {
                        if (GetInspectorRotationValueMethod(transform) > joint2Angle)
                        {
                            j2RotationSpeedX = 0;
                            joint2MotionOver = true;
                            joint2Automatic = false;
                        }
                    }
                    if (GetInspectorRotationValueMethod(transform) > 0)//当旋转角度在-180度到-360度时
                    {
                        if (joint2AngleAbjust > joint2Angle)
                        {
                            j2RotationSpeedX = 0;
                            joint2MotionOver = true;
                            joint2Automatic = false;
                        }
                    }
                }
            }
        }
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            Joint2Reset();
        }
    }
}
