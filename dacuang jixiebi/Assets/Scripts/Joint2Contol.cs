using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint2Contol : MonoBehaviour
{
    public float joint2Angle = 45;//旋转角度
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 180, 0), 0.01f);
        }
    }
}
