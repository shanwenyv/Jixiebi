/*******************************
 * 因为物体角度大于180度时，实际角度为 360+真实角度 ,所以在判定时需要进行人为的校准
 *****************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class Joint4Contol : MonoBehaviour
{
    private int t = 0;
    private int a = 0;
    public static Joint4Contol Instance4;
    public float joint4Angle = 90;//关节2旋转角度

    public static bool joint4Automatic = false;//关节4自动运行判定，否停止，真开始

    public static bool joint4MotionOver = false;//如果关节完成运动，其值为1，否则为0

    public float j4RotationSpeedX = 30;//设定初始运动速度
    public float j4RotationSpeedY = 0;
    public float j4RotationSpeedZ = 0;
    public float joint4AngleAbjust = 0;//在运动代码部分，当旋转角度大于180度时，校准度数
    public float joint4SpeedAbjust = 0;//在重置速度部分，当旋转角度大于180度时，校准度数

    private float t4;
    public void Joint4Reset()//重置关节代码中的各项参数
    {
        joint4MotionOver = false;
        j4RotationSpeedX = 30;//按下小键盘回车，重置速度初始值
        joint4SpeedAbjust = 0;//重置速度中判断角度调整值
        joint4AngleAbjust = 0;//重置旋转角度调整值
        if (joint4Angle >= 0 && t4 >= 0)
        {
            if (GetInspectorRotationValueMethod(transform) < 0)//如果物体角度大于180度，则校准角度
            {
                t = 1;
                joint4SpeedAbjust = 360 + GetInspectorRotationValueMethod(transform);

            }
            if (joint4Angle < GetInspectorRotationValueMethod(transform) - 0.5)//物体大于180度时，校准角度
            {
                j4RotationSpeedX = -j4RotationSpeedX;
            }
            else if (joint4Angle < joint4SpeedAbjust)
            {
                j4RotationSpeedX = -j4RotationSpeedX;
            }
        }
        else if (joint4Angle <= 0 && t4 <= 0)
        {
            j4RotationSpeedX = -30;
            if (GetInspectorRotationValueMethod(transform) > 0)//如果物体角度小于-180度，则校准角度
            {
                joint4SpeedAbjust = GetInspectorRotationValueMethod(transform) - 360;//校准角度
            }
            if (joint4SpeedAbjust < joint4Angle)
            {
                j4RotationSpeedX = -j4RotationSpeedX;
            }
            if (joint4Angle > GetInspectorRotationValueMethod(transform))
            {
                j4RotationSpeedX = -j4RotationSpeedX;
            }
        }
    }

    public void Joint4AutomationContol()//关节4自动运动
    {
        transform.Rotate(new Vector3(j4RotationSpeedX, j4RotationSpeedY, j4RotationSpeedZ) * Time.deltaTime);
        if (GetInspectorRotationValueMethod(transform) < 0 && joint4Angle > 0)//如果做正数角度旋转且旋转角度大于180度，则矫正角度到真实角度
        {
            joint4AngleAbjust = 360 + GetInspectorRotationValueMethod(transform);
        }
        if (GetInspectorRotationValueMethod(transform) > 0 && joint4Angle < 0)//如果做负数角度旋转且旋转角度小于于-180度，则矫正角度到真实角度
        {
            joint4AngleAbjust = GetInspectorRotationValueMethod(transform) - 360;
        }
        //print("旋转了：" + GetInspectorRotationValueMethod(transform));//输出旋转角度
        if (joint4Angle >= 0)//当设定角度大于初始角度时,机械臂做正方向运动
        {
            //print("旋转了：" + this.transform.localEulerAngles.z);
            if (j4RotationSpeedX > 0)
            {
                if (GetInspectorRotationValueMethod(transform) > 0)//角度在0到180之间时，正常旋转
                {
                    if (GetInspectorRotationValueMethod(transform) >= joint4Angle + 0.5)
                    {
                        j4RotationSpeedX = 0;
                        joint4MotionOver = true;
                        joint4Automatic = false;
                    }
                }
                else if (GetInspectorRotationValueMethod(transform) < 0)//角度在180到360之间时，先通过角度校准，再进行旋转
                {
                    if (joint4AngleAbjust >= joint4Angle + 0.5)
                    {
                        j4RotationSpeedX = 0;
                        joint4MotionOver = true;
                        joint4Automatic = false;
                    }
                }
            }
            else if (j4RotationSpeedX < 0)//当设定角度小于初始角度时
            {
                if (GetInspectorRotationValueMethod(transform) > 0)//角度在0到180之间时，正常旋转
                {
                    if (GetInspectorRotationValueMethod(transform) <= joint4Angle + 0.5)
                    {
                        j4RotationSpeedX = 0;
                        joint4MotionOver = true;
                        joint4Automatic = false;
                    }
                }
                else if (GetInspectorRotationValueMethod(transform) < 0)//角度在180到360之间时，先通过角度校准，再进行旋转
                {
                    if (joint4AngleAbjust <= joint4Angle + 0.5)
                    {
                        j4RotationSpeedX = 0;
                        joint4MotionOver = true;
                        joint4Automatic = false;
                    }
                }
            }
        }
        else if (joint4Angle <= 0)//当设定角度小于初始角度时,机械臂做负方向运动
        {
            if (j4RotationSpeedX < 0)//机械臂做逆时针转动
            {
                if (GetInspectorRotationValueMethod(transform) < 0)//当旋转角度在0到-180度时
                {
                    if (GetInspectorRotationValueMethod(transform) < joint4Angle)
                    {
                        j4RotationSpeedX = 0;
                        joint4MotionOver = true;
                        joint4Automatic = false;
                    }
                }
                else if (GetInspectorRotationValueMethod(transform) > 0)//当旋转角度在-180度到-360度时
                {
                    if (joint4AngleAbjust < joint4Angle)
                    {
                        j4RotationSpeedX = 0;
                        joint4MotionOver = true;
                        joint4Automatic = false;
                    }
                }
            }
            else if (j4RotationSpeedX > 0)//机械臂做顺时针转动
            {
                if (GetInspectorRotationValueMethod(transform) < 0)//当旋转角度在0到-180度时
                {
                    if (GetInspectorRotationValueMethod(transform) > joint4Angle)
                    {
                        j4RotationSpeedX = 0;
                        joint4MotionOver = true;
                        joint4Automatic = false;
                    }
                }
                if (GetInspectorRotationValueMethod(transform) > 0)//当旋转角度在-180度到-360度时
                {
                    if (joint4AngleAbjust > joint4Angle)
                    {
                        j4RotationSpeedX = 0;
                        joint4MotionOver = true;
                        joint4Automatic = false;
                    }
                }
            }
        }
    }
    private void Awake()
    {
        Instance4 = this;                            //单例模式

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
        if (joint4Angle < 0)
            j4RotationSpeedX = -30;
    }

    void Update()
    {
        if (JointInitiaze.jointIntiazeBool == true)//初始化机械臂关节4角度
        {
            //joint4Angle = 0;
            transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            Joint4Reset();
        }
        //print("旋转了" + GetInspectorRotationValueMethod(transform));
        float currentRotateX = transform.eulerAngles.x;
        if (currentRotateX > 180)
        {
            currentRotateX = currentRotateX - 360;//这样-5度还是-5度 而不是355！
        }
        if (Input.GetKey(KeyCode.Keypad4) || Input.GetKey(KeyCode.Return) || joint4Automatic == true)
        {
            t4 = joint4Angle;
            Joint4AutomationContol();
        }
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            Joint4Reset();
        }
    }
}
