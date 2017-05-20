using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideZone : MonoBehaviour {

    public GameObject Door;
	// Use this for initialization
	void Start () {
        if(Door == null)
            Debug.Log("No object attached to Hide Zone");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Enter Hiding");

        if(other.tag == "Player" && !other.GetComponent<PlayerControl>().isHidden())
        {
            if(Door == null)
                other.GetComponent<PlayerControl>().HideEnable();
            if (Door != null && !Door.GetComponent<OpenDoor>().isOpen())
                other.GetComponent<PlayerControl>().HideEnable();
        }
    }

    //NOTE NEED TO DISABLE HIDE WHEN PLAYER OPENS DOOR
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerControl>().HideDisable();
        }
    }
}
