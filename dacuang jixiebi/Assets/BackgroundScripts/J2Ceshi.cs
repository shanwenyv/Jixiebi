/*******************************
 * 因为物体角度大于180度时，实际角度为 360+真实角度 ,所以在判定时需要进行人为的校准
 *****************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class J2Ceshi : MonoBehaviour
{
	public static J2Ceshi Instance;
    public float joint2Angle = 90;//关节2旋转角度
    //关节旋转角度
    public float j2RotationSpeedX = 30;//设定初始运动速度
    public float j2RotationSpeedY = 0;
    public float j2RotationSpeedZ = 0;
    public float joint2AngleAbjust = 0;//在运动代码部分，当旋转角度大于180度时，校准度数
    public float joint2SpeedAbjust = 0;//在重置速度部分，当旋转角度大于180度时，校准度数
    private void Awake()
    {
        Instance = this;                            //单例模式
    }
    public float GetInspectorRotationValueMethod(Transform transform)
    {
        // 获取原生值
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


    private int t = 0;
    private int a = 0;
    // Start is called before the first frame update
    void Start()
    {
        //joint2Angle = -joint2Angle+20;
        //joint2Angle = joint2Angle / 2;
        //joint2Angle = joint2Angle - 90;//旋转角度校准
        /*System.Type transformType = transform.GetType();
        PropertyInfo m_propertyInfo_rotationOrder = transformType.GetProperty("rotationOrder", BindingFlags.Instance | BindingFlags.NonPublic);
        object m_OldRotationOrder = m_propertyInfo_rotationOrder.GetValue(transform, null);
        MethodInfo m_methodInfo_GetLocalEulerAngles = transformType.GetMethod("GetLocalEulerAngles", BindingFlags.Instance | BindingFlags.NonPublic);
        object value = m_methodInfo_GetLocalEulerAngles.Invoke(transform, new object[] { m_OldRotationOrder });
        //获取的真实值
        Debug.LogError("反射调用GetLocalEulerAngles方法获得的值：" + value.ToString());
        Debug.LogError("transform.localEulerAngles获取的值：" + transform.localEulerAngles.ToString());*/
    }

    // Update is called once per frame
    void Update()
    {
        /*System.Type transformType = transform.GetType();
        PropertyInfo m_propertyInfo_rotationOrder = transformType.GetProperty("rotationOrder", BindingFlags.Instance | BindingFlags.NonPublic);
        object m_OldRotationOrder = m_propertyInfo_rotationOrder.GetValue(transform, null);
        MethodInfo m_methodInfo_GetLocalEulerAngles = transformType.GetMethod("GetLocalEulerAngles", BindingFlags.Instance | BindingFlags.NonPublic);
        object value = m_methodInfo_GetLocalEulerAngles.Invoke(transform, new object[] { m_OldRotationOrder });
        //获取的真实值
        Debug.LogError("反射调用GetLocalEulerAngles方法获得的值：" + value.ToString());
        Debug.LogError("transform.localEulerAngles获取的值：" + transform.localEulerAngles.ToString());*/
        /*if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Return))
        {
            print("旋转了：" + this.transform.localEulerAngles.x);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(joint2Angle, 0, 0), 0.01f);//调用Joint2Contl中的旋转角度抵消臂1对臂2坐标的影响
        }*/
        float currentRotateX = transform.eulerAngles.x;
        if (currentRotateX > 180)
        {
            currentRotateX = currentRotateX - 360;//这样-5度还是-5度 而不是355！
        }
            //float num = float.Parse(this.transform.localEulerAngles.x);
            if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Return))
            //transform.Rotate(Vector3.forward * Time.deltaTime * j2RotationSpeedX);
        {
            if(GetInspectorRotationValueMethod(transform) < 0)//如果旋转角度大于180度，则矫正角度到真实角度
            {
                joint2AngleAbjust = 360 + GetInspectorRotationValueMethod(transform);
            }
            //print("旋转了：" + GetInspectorRotationValueMethod(transform));//输出旋转角度
            if (joint2Angle >= 0)//当设定角度大于初始角度时
            {
                //print("旋转了：" + this.transform.localEulerAngles.z);
                if (j2RotationSpeedX > 0)
                {
                    if (GetInspectorRotationValueMethod(transform) > 0)//角度在0到180之间时，正常旋转
                    {
                        if (GetInspectorRotationValueMethod(transform) >= joint2Angle + 0.5)
                        {
                            j2RotationSpeedX = 0;

                        }
                    }
                    else if(GetInspectorRotationValueMethod(transform) < 0)//角度在180到360之间时，先通过角度校准，再进行旋转
                    {
                        if (joint2AngleAbjust >= joint2Angle + 0.5)
                        {
                            j2RotationSpeedX = 0;

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
                        }
                    }
                    else if (GetInspectorRotationValueMethod(transform) < 0)//角度在180到360之间时，先通过角度校准，再进行旋转
                        if (joint2AngleAbjust <= joint2Angle + 0.5)
                        {
                            j2RotationSpeedX = 0;

                        }
                }
                transform.Rotate(new Vector3(j2RotationSpeedX, j2RotationSpeedY, j2RotationSpeedZ) * Time.deltaTime);

            }
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90), 0.01f);
            /*if (joint2Angle >= 0 && joint2Angle <= 90)//到了指定角度就停止旋转
            {
                if (j2RotationSpeedX > 0)
                {
                    if (this.transform.localEulerAngles.x >= joint2Angle - 0.5)
                    {
                        j2RotationSpeedX = 0;
                    }
                }
                if (j2RotationSpeedX < 0)
                {
                    if (this.transform.localEulerAngles.x <= joint2Angle + 0.5)
                    {
                        j2RotationSpeedX = 0;
                    }
                }
                transform.Rotate(new Vector3(j2RotationSpeedX, j2RotationSpeedY, j2RotationSpeedZ) * Time.deltaTime);
                //print("旋转了：" + this.transform.localEulerAngles.x);
            }
            else if (joint2Angle > 90 && joint2Angle <= 180)
            {


                transform.Rotate(new Vector3(j2RotationSpeedX, j2RotationSpeedY, j2RotationSpeedZ) * Time.deltaTime);
                
                print("旋转了：" + this.transform.localEulerAngles.x);
                
                if (this.transform.localEulerAngles.x > 89.5)//校准：当旋转角度大于90的时候，启动检测判定（因为此模型转轴度数的特殊性，特写此算法）
                {
                    t = 1;
                }
                if (j2RotationSpeedX > 0)
                {
                    if (this.transform.localEulerAngles.x < 90 - (joint2Angle - 90.5) && t == 1)
                    {
                        //print("执行了233");
                        j2RotationSpeedX = 0;
                    }
                }
                if (j2RotationSpeedX < 0)
                {
                    if (this.transform.localEulerAngles.x > 90 && this.transform.localEulerAngles.x < 90.3)
                    {
                        t = 1;
                    }
                    if (this.transform.localEulerAngles.x > 90 - (joint2Angle - 90.5) && t == 0)
                    {
                        //print("执行了233");
                        j2RotationSpeedX = 0;
                    }
                }

            }
            else if (joint2Angle > 180 && joint2Angle <= 270)
            {
                transform.Rotate(new Vector3(j2RotationSpeedX, j2RotationSpeedY, j2RotationSpeedZ) * Time.deltaTime);
                //print("旋转了：" + this.transform.localEulerAngles.x); ;
                if (this.transform.localEulerAngles.x > 179.8)//校准：当旋转角度大于180的时候，启动检测判定（因为此模型转轴度数的特殊性，特写此算法）
                {
                    t = 1;
                }
                if (this.transform.localEulerAngles.x < 360.5 - (180 - (360 - joint2Angle)) && t == 1)
                {
                    //print("执行了233");
                    j2RotationSpeedX = 0;
                }
            }
            else if (joint2Angle > 270 && joint2Angle <= 360)
            {

                transform.Rotate(new Vector3(j2RotationSpeedX, j2RotationSpeedY, j2RotationSpeedZ) * Time.deltaTime);
                //print("旋转了：" + this.transform.localEulerAngles.x);
                if (this.transform.localEulerAngles.x > 350.5)//校准：当旋转角度大于270的时候，启动检测判定（因为此模型转轴度数的特殊性，特写此算法）
                {
                    t = 1;
                }
                if (this.transform.localEulerAngles.x < 270.5 && t == 1)
                {
                    a = 1;
                }
                if (this.transform.localEulerAngles.x >= joint2Angle - 0.5 && a == 1)
                    j2RotationSpeedX = 0;
            }*/
        }
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            joint2SpeedAbjust = 0;
            joint2AngleAbjust = 0;
            j2RotationSpeedX = 30;//按下小键盘回车，重置速度初始值
            a = 0;
            t = 0;
            if(GetInspectorRotationValueMethod(transform) < 0)//如果物体角度大于180度，则校准角度
            {
                t = 1;
                joint2SpeedAbjust = 360 + GetInspectorRotationValueMethod(transform);

            }
            if (joint2Angle < GetInspectorRotationValueMethod(transform) - 0.5)//物体大于180度时，校准速度。
            {
                j2RotationSpeedX = -j2RotationSpeedX;
            }
            else if(joint2Angle < joint2SpeedAbjust)
            {
                j2RotationSpeedX = -j2RotationSpeedX;
            }
        }
    }
}
