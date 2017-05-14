using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cabinet1 : MonoBehaviour {

    //public GameObject Collider;
    public GameObject Door;
    public string uiString;
    private Transform pivot;

    private BoxCollider doorColider;
    private MeshRenderer doorRender;
    private Text textCanvas;

    bool open;
    float smooth = 2.0f;
    float DoorOpenAngle = 90.0f;
    float DoorCloseAngle = 0.0f;
    Vector3 doorPivot;

    // Use this for initialization
    void Start () {
        open = false;
        Debug.Log("trigger start");

        if(Door != null)
        {
            doorColider = Door.GetComponent<BoxCollider>();
            doorRender = Door.GetComponent<MeshRenderer>();
            pivot = Door.GetComponentInChildren<Transform>();

            if(pivot != null)
            {
                doorPivot = new Vector3(
                    pivot.position.x,
                    pivot.position.y,
                    pivot.position.z);

                Debug.Log("Pivot " + doorPivot);
            }
        }
        

        if(uiString == "")
        {
            //Pull the bound use button and use it in string.
            this.uiString = "Press \"" + Input.GetButton("Use").ToString() + "\" to use.";
        }
        

    }
	
	// Update is called once per frame
	void Update () {
        if(open)
        {
            var target = Quaternion.Euler(0, transform.localRotation.eulerAngles.y + DoorOpenAngle, 0);
            Quaternion doorRoation = Quaternion.Slerp(Door.transform.localRotation, target, Time.deltaTime * smooth);
            float doorAngle = 1f;
            //Vector3 doorPivot = new Vector3( pivot.transform.position.x , , );
            Door.transform.RotateAround(doorPivot,Vector3.up, doorAngle);

        }
    }

    /// <summary>
    /// While player is in trigger
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay(Collider other)
    {
        //if(!textCanvas.enabled)
        //    textCanvas.enabled = !textCanvas.enabled;
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Use"))
            {
                Debug.Log("Player Pressed Use.");
                
                //TEMP CODE
                //Removing door instead of animating until animation is done.
                doorColider.enabled = !doorColider.enabled;
                doorRender.enabled = !doorRender.enabled;
                //textCanvas.enabled = !textCanvas.enabled;
                if (!Door.GetComponent<MeshRenderer>().enabled)
                    other.GetComponent<PlayerControl>().HideDisable();
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Setting text = " + uiString);
            other.GetComponent<PlayerControl>().UI_Set_Text(this.uiString);
            open = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerControl>().UI_Clear_Text();
        }
    }
}
