using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cabinet1 : MonoBehaviour {

    //public GameObject Collider;
    public GameObject Door;
    public string uiString;

    private BoxCollider doorColider;
    private MeshRenderer doorRender;
    private Text textCanvas;



    // Use this for initialization
    void Start () {
        Debug.Log("trigger start");

        if(Door != null)
        {
            doorColider = Door.GetComponent<BoxCollider>();
            doorRender = Door.GetComponent<MeshRenderer>();
        }
        

        if(uiString == "")
        {
            //Pull the bound use button and use it in string.
            this.uiString = "Press \"" + Input.GetButton("Use").ToString() + "\" to use.";
        }
        

    }
	
	// Update is called once per frame
	void Update () {

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

                Door.GetComponent<OpenDoor>().AnimDoor();
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
