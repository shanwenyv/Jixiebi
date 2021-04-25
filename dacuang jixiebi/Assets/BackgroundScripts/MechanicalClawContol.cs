using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalClawContol : MonoBehaviour
{
    // Start is called before the first frame update
    private int t = 0;
    static public  bool grabUP = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Catch")
        {
            print("触发碰撞");
            grabUP = true;
        }
    }
    public Transform trans;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(grabUP ==true || Input.GetKey(KeyCode.F1))
        {
            trans.parent = this.transform;
        }
        if(grabUP == false || Input.GetKey(KeyCode.F2))
        {
            this.transform.DetachChildren();
        }
    }
}
