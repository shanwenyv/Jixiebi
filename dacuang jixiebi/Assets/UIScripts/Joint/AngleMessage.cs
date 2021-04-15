using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleMessage : MonoBehaviour
{
    public GameObject meui;
    //public GameObject angle1Message;
    //public GameObject angle2Message;
    //public GameObject angle3Message;
    //public GameObject angle4Message;
    //public GameObject angle5Message;
    public GameObject[] angleMessage = new GameObject[5];

    private void Awake()
    {
        meui = GameObject.FindGameObjectWithTag("MessagePanel");
        //angle1Message = FindChildGameObject("Angle1");
        //angle2Message = FindChildGameObject("Angle2");
        //angle3Message = FindChildGameObject("Angle3");
        //angle4Message = FindChildGameObject("Angle4");
        //angle5Message = FindChildGameObject("Angle5");
        for (int i = 0; i < angleMessage.Length; i++)
        {
            angleMessage[i] = FindChildGameObject($"Angle{i + 1}");
        }
    }

    void Update()
    {
        GetAngle();
    }

    public void GetAngle()
    {
        //angle1Message.GetComponent<Text>().text = $"{JointControl.joint1.transform.localRotation.eulerAngles.z}";
        //angle2Message.GetComponent<Text>().text = $"{JointControl.joint2.transform.localRotation.eulerAngles.x}";
        //angle3Message.GetComponent<Text>().text = $"{JointControl.joint3.transform.localRotation.eulerAngles.x}";
        //angle4Message.GetComponent<Text>().text = $"{JointControl.joint4.transform.localRotation.eulerAngles.x}";
        //angle5Message.GetComponent<Text>().text = $"{JointControl.joint5.transform.localRotation.eulerAngles.z}";

        angleMessage[0].GetComponent<Text>().text = $"{JointControl.joint[0].transform.localRotation.eulerAngles.z}";
        angleMessage[1].GetComponent<Text>().text = $"{JointControl.joint[1].transform.localRotation.eulerAngles.x}";
        angleMessage[2].GetComponent<Text>().text = $"{JointControl.joint[2].transform.localRotation.eulerAngles.x}";
        angleMessage[3].GetComponent<Text>().text = $"{JointControl.joint[3].transform.localRotation.eulerAngles.x}";
        angleMessage[4].GetComponent<Text>().text = $"{JointControl.joint[4].transform.localRotation.eulerAngles.z}";

    }

    public GameObject FindChildGameObject(string name)
    {
        Transform[] trans = meui.GetComponentsInChildren<Transform>();

        foreach (Transform item in trans)
        {
            if (item.name == name)
            {
                return item.gameObject;
            }
        }

        Debug.LogWarning($"{meui.name}里找不到名为{name}的子对象");
        return null;
    }
}
