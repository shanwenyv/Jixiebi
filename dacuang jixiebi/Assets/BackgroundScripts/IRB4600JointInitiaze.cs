using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRB4600JointInitiaze : MonoBehaviour
{
    // Start is called before the first frame update

    static public bool irb4600jointIntiazeBool = false;//关节初始化判定

    public static bool irb4600jointIntiazeStart = false;//按钮按下检测

    public static void IRB4600Initiaze()//机械臂初始化方法
    {
        irb4600jointIntiazeBool = true;
        return;
    }
    public static void IRB4600InitiazeReturn()//初始化机械臂关节初始化判定
    {
        irb4600jointIntiazeBool = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))//当Q键被按下的时候，关节启动初始化程序
        {
            IRB4600Initiaze();
        }
        if (Input.GetKey(KeyCode.KeypadEnter))//按下小键盘回车键，初始化 关节初始化判定bool变量
        {
            IRB4600InitiazeReturn();
        }
    }
}
