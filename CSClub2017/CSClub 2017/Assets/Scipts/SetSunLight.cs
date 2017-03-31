using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSunLight : MonoBehaviour {

    Material sky;

    //public Transform stars;

	// Use this for initialization
	void Start () {
        sky = RenderSettings.skybox;
	}
	
	// Update is called once per frame
	void Update () {
        //stars.transform.rotation = transform.rotation;
	}
}
