using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour
{
    private float mouseX, mouseY;//鼠标坐标的值
    public float moseSensitivity;//鼠标灵敏度

    public Transform player;

    public float xRotation;
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * moseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * moseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        player.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    
    
    }

}
