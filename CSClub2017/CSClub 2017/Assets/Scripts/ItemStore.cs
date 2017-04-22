using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStore : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        string tagStr = "placeholder"; // this string will hold the tag of the object the user is currently holding

        // Write input stream that stores the object's tag in the string tagStr 
        // ^ NEI on the objects to do this 

        GameObject[] nightStand = GameObject.FindGameObjectsWithTag(tagStr); // Stores the object the user is holding in the nightStand array 
    }
	
	// Update is called once per frame 
	void Update () 
    { 
		
	} 
} 
