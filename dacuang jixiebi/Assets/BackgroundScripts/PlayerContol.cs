using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContol : MonoBehaviour
{
    private CharacterController cc;//

    public float moveSpeed;//角色移动速度

    public float jumpSpeed;//角色跳跃速度

    private float horizontalMove, verticalMove;//按键值变量

    private Vector3 dir;//定义三维变量,dir控制方向

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;
        verticalMove = Input.GetAxis("Vertical") * moveSpeed;

        //dir储存移动的方向
        dir = transform.forward * verticalMove + transform.right * horizontalMove;
        cc.Move(dir * Time.deltaTime);
    
    
    
    }
}
