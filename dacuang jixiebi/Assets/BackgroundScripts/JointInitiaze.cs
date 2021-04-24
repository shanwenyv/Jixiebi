using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointInitiaze : MonoBehaviour
{
    // Start is called before the first frame update
    static public bool jointIntiazeBool = false;//关节初始化判定

    public static bool jointIntiazeStart = false;//按钮按下检测

    static void Initiaze()//机械臂初始化方法
    {
        jointIntiazeBool = true;
        return;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))//当Q键被按下的时候，关节启动初始化程序
        {
            Initiaze();
        }
        if(Input.GetKey(KeyCode.KeypadEnter))//按下小键盘回车键，初始化 关节初始化判定bool变量
        {
            jointIntiazeBool = false;
        }
    }
}
