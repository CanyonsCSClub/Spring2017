using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject pivotObj;

    bool open = false;
    bool movingDoor = false;
    float smooth = 3.0f;
    float totalMovement = 0f;
    float DoorOpenAngle = 45.0f;
    float DoorCloseAngle;
    Vector3 doorPivot;
    float doorTime = 0f;
    private Transform pivot;

    private void Start()
    {
        DoorCloseAngle = transform.rotation.eulerAngles.y;
        DoorOpenAngle += DoorCloseAngle;

        if (pivotObj != null)
            pivot = pivotObj.GetComponentInChildren<Transform>();
        else
            Debug.Log("Door Pivot Missing");

        if (pivot != null)
        {
            doorPivot = new Vector3(
                pivot.position.x,
                pivot.position.y,
                pivot.position.z);

            //Debug.Log("Pivot " + doorPivot);
        }
    }

    void Update()
    {
        if (pivotObj != null && movingDoor)
        {
            if (!open)
            {
                if (totalMovement >= 60f)
                {
                    open = true;
                    movingDoor = false;
                }
                transform.RotateAround(doorPivot, Vector3.up, smooth);
                totalMovement += smooth;
            }
            else
            {
                if (totalMovement <= 0)
                {
                    open = false;
                    movingDoor = false;
                }
                transform.RotateAround(doorPivot, Vector3.up, 0 - smooth);
                totalMovement -= smooth;
            }
        }
    }

    public bool isOpen()
    {
        return (open || movingDoor);
    }

    public void Open()
    {
        if(!open)
            movingDoor = true;
    }


    public void Close()
    {
        if(open)
            movingDoor = true;
    }

    public void AnimDoor()
    {
        movingDoor = true;
    }

}