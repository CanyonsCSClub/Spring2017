using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;

    protected Vector3 offset;

	// Use this for initialization
	void Start ()
    {
        offset = this.transform.position - player.transform.position;
	
	}
	
	// Last Update 
	void LateUpdate ()
    {
        transform.position = player.transform.position + offset;
	}
}
