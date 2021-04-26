using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalClawContol : MonoBehaviour
{
    // Start is called before the first frame update
    static public bool t = false;
    static public bool touch = false;

    static public  bool grabUP = false;

    public void Up()//抓起
    {
        grabUP = true;
    }
    public void Down()//放下
    {
        grabUP = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Catch")
        {
            print("触发碰撞");
            touch = true;
        }
    }
    public Transform trans;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F1))
        {
            print("抓起");
            Up();
        }
        if (Input.GetKey(KeyCode.F2))
        {
            print("放下");
            Down();
        }
        if (grabUP ==true && touch == true)
        {
            trans.parent = this.transform;
        }
        if(grabUP == false && touch == true)
        {
            this.transform.DetachChildren();
        }
    }
}
