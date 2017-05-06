using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet1 : MonoBehaviour {

    public GameObject OutsideWaypoint;
    public GameObject InsideWaypoint;
    //public GameObject Collider;
    public GameObject Door;

	// Use this for initialization
	void Start () {
        if (this.OutsideWaypoint == null || this.InsideWaypoint == null)
            Debug.Log("Waypoints Null");

        Debug.Log("trigger start");
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
        if (this.OutsideWaypoint == null || this.InsideWaypoint == null)
            return;

        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Use"))
            {
                Debug.Log("Player Pressed Use.");

                //TEMP CODE
                //Removing door instead of animating until animation is done.
                Door.GetComponent<BoxCollider>().enabled = !Door.GetComponent<BoxCollider>().enabled;
                Door.GetComponent<MeshRenderer>().enabled = !Door.GetComponent<MeshRenderer>().enabled;
                if (!Door.GetComponent<MeshRenderer>().enabled)
                    other.GetComponent<PlayerControl>().HideDisable();
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter Trigger");
    }
}
