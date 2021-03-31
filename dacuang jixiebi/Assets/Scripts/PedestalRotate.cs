using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalRotate : MonoBehaviour
{
    public Space m_RotateSpace;
    public float m_RotateSpeed = 20f;
    //public Transform 臂1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.down * m_RotateSpeed * Time.deltaTime, m_RotateSpace);
    }
}
