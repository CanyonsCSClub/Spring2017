/*
 * 
 * Author: Spencer Wilson
 * Date Created: 4/22/2017
 * Last Edited: 4/22/2017, 10:08 pm
 * File: PlayerLook.cs
 * Description: This file contains the script that allows the camera to rotate, enabling the player to "look".
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    float xRotation;
    float yRotation;
    float newXRotation;
    float newYRotation;
    float lookSensitivity;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerRotate();
    }

    void PlayerRotate()
    {

        Ray playerFocus = Camera.main.ScreenPointToRay(Input.mousePosition);
        //xRotation = Ray.playerFocus(Input.mousePosition, 0 , 0);
        yRotation = (Input.GetAxis("Vertical"));

        transform.rotation = Quaternion.Euler(0, 0, 0); // Rotates the camera.
    }
}
