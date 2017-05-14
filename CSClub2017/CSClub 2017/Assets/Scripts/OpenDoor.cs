using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    float smooth = 2.0f;
    float DoorOpenAngle = 90.0f;
    float DoorCloseAngle = 0.0f;
    bool open;
    bool enter;



    public void Open()
    {
        open = true;
    }


    public void Close()
    {
        open = false;

        
    }

    

    void Update()
    {
        if (open == true)
        {
            print("if(open)");
            var target = Quaternion.Euler(0, DoorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * smooth);
        }

        if (open == false)
        {
            print("if(!open)");
            var target1 = Quaternion.Euler(0, DoorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * smooth);
        }

        if (enter == true)
        {
            print("if(enter)");

        }
    }

    public bool isOpen()
    {
        return open;
    }
}