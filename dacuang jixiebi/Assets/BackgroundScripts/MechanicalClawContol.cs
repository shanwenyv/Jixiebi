using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalClawContol : MonoBehaviour
{
    // Start is called before the first frame update
    private int t = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Catch")
        {
            print("触发碰撞");
            t = 1;
        }
    }
    public Transform trans;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(t == 1 && Input.GetKey(KeyCode.Keypad0))
        {
            trans.parent = this.transform;
        }
    }
}
