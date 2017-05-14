using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideZone : MonoBehaviour {

    public GameObject Door;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Enter Hiding");

        if(other.tag == "Player" && !other.GetComponent<PlayerControl>().isHidden())
        {
            if(Door != null && Door.GetComponent<MeshRenderer>().enabled)
                other.GetComponent<PlayerControl>().HideEnable();
            else
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
