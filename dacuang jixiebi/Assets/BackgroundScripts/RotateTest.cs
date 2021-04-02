using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTest : MonoBehaviour
{
    //private float rataleSpeed = 1f;//旋转速度
    //Quaternion targetAngels;
    // public Transform rotateCentre;
    // Start is called before the first frame update
    //float jointCeShi = 1;
    //private float duration = 10;//旋转时间
    private float angle = 90;//旋转角度
    //int i = 0;
    /*void Start()
    {
        //targetAngels = Quaternion.Euler(45f, 45f, 0);//第二个参数需要的是四元数,所以这里需要将目标的角度转成四元数去计算
        jointCeShi = angle * rataleSpeed;
    }*/

    /*private void MethodName()
    {
        return;
        
    }*/
    // Update is called once per frame
    void Update()
    {
        //  用 slerp 进行插值平滑的旋转
        //transform.rotation = Quaternion.Euler(transform.rotation, targetAngels, rotateSpeed * Time.deltaTime);
        // 当初始角度跟目标角度小于1,将目标角度赋值给初始角度,让旋转角度是我们需要的角度
        //transform.rotation = Quaternion.Lerp(transform.rotation, transform.localEulerAngles = new Vector3(0, 0, 90), 0.05f);
        //transform.localEulerAngles = new Vector3(0, 0, 90);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90), 0.05f);
        //jointCeShi = 1;
        /*if (Input.GetKey(KeyCode.Space))
        {

            //如果是围绕自身的x轴进行旋转，就是transform.Rotate(new Vector3(1, 0, 0));
            //如果是围绕自身的y轴进行旋转，就是transform.Rotate(new Vector3(0, 1, 0));
            //如果是围绕自身的z轴进行旋转，就是transform.Rotate(new Vector3(0, 0, 1));
            transform.Rotate(new Vector3(0, 0, jointCeShi));
            i++;
            Invoke(nameof(MethodName), duration);
            
        }*/
        if (Input.GetKey(KeyCode.Space))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-90, 0, angle), 0.01f);
        }


    }
}
