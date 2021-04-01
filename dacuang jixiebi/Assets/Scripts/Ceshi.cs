using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceshi : MonoBehaviour
{
    /*private bool onDrag = false; //是否被拖拽//
    public float speed = 6f; //旋转速度//
    private float tempSpeed; //阻尼速度//
    private float axisX = 1;
    //鼠标沿水平方向移动的增量//
    private float axisY = 1; //鼠标沿竖直方向移动的增量//
    private float cXY;*/
    public bool isOpen = false; //是否开始旋转

    public int speed = 100; //旋转的速度
    void OnMouseDown()
    {
        //接受鼠标按下的事件//
        //axisX = 0f; axisY = 0f;
    }
    /*void OnMouseDrag() //鼠标拖拽时的操作//
    {
        onDrag = true;
        axisX = -Input.GetAxis("moveX");
        //获得鼠标增量//
        axisY = Input.GetAxis("moveY");
        cXY = Mathf.Sqrt(axisX * axisX + axisY * axisY); //计算鼠标移动的长度//
        if (cXY == 0f) { cXY = 1f; }
    }
    float Rigid() //计算阻尼速度//
    {
        if (onDrag)
        {
            tempSpeed = speed;
        }
        else
        {
            if (tempSpeed > 0)
            {
                tempSpeed -= speed * 2 * Time.deltaTime / cXY; //通过除以鼠标移动长度实现拖拽越长速度减缓越慢//
            }
            else
            {
                tempSpeed = 0;
            }
        }
        return tempSpeed;
    }**/
    void Update()
    {
        // this.transform.Rotate(new Vector3(axisY, axisX, 0) * Rigid(), Space.World); //这个是是按照之前方向一直慢速旋转
        /*if (!Input.GetMouseButton(0))
        {
            onDrag = false;
            this.transform.Rotate(new Vector3(axisY, axisX, 0) * 0.5f, Space.World);
        }*/
        //transform.RotateAround(new Vector3(10f, 0f, 0f), new Vector3(0f, 0f, 1f), 3f);
        if (isOpen)

        {
            RotateAxisOfSelf(SelfAxis.X, speed);

        }
    }
    private void RotateAxisOfSelf(SelfAxis selfAxis, int speed = 2)

    {
        switch (selfAxis)

        {
            case SelfAxis.X:

                this.transform.Rotate(new Vector3(1 * Time.deltaTime * speed, 1, 0));
                //this.transform.localEulerAngles = new Vector3(30, 0, 0);

                break;

            case SelfAxis.Y:

                this.transform.Rotate(new Vector3(0, 1 * Time.deltaTime * speed, 0));

                break;

            case SelfAxis.Z:

                this.transform.Rotate(new Vector3(0, 0, 1 * Time.deltaTime * speed));

                break;

            default:

                this.transform.Rotate(new Vector3(1 * Time.deltaTime * speed, 0, 0));

                break;

        }

    }
    
    enum SelfAxis//枚举轴

    {
        X,

        Y,

        Z,

    }

}
