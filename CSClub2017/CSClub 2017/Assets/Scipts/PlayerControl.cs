/*
 * 
 * Author: Spencer Wilson
 * Date: 3/14/2017 (Happy Pi-Day everyone!)
 * File: PlayerControl.cs
 * Description: This file contains the code that takes user input in order to control the character.
 * Disclaimer: A  majority of this code I have gotten from tutorials. I have not been simply copying them, I have been
 * trying to understand them and how they effect the gameplay mechanics in the Unity engine so that I may write code on my
 * own one day. Simply put, they are a learning aid and have helped guide me along the process of creation.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerControl : MonoBehaviour
{

    public float speed = 3; // Declaring a variable that holds the player's speed.
    private Rigidbody rb; // Declaring a Rigidbody variable.

    float xRotationV = 0.0f; // The velocity along the x-axis. SmoothDamp function requires you to have a saved velocity variable.
    float yRotationV = 0.0f; // The velocity along the y-axis. SmoothDamp function requires you to have a saved velocity variable.
    float yRotation; // Variable that holds the value that the character rotates along the y-axis. (Where the player turns too)
    float xRotation; // Variable that holds the value that the character rotates along the x-axis. (Where the player turns too)
    public float currentRotationX; // Variable that holds the current value of the XRotation, prior to the camera movement.
    public float currentRotationY; // Variable that holds the current value of the YRotation, priotr to the camera movement.
    public float lookSmoothDamp = 0.1f; // Controls the smoothness of the player's look. (controls how smooth or jarring the camera is)
    public float lookSensitivityX = 5.0f; // Variable that controls the sensitivity of the horizontal look.
    public float lookSensitivityY = 5.0f; // Variable that controls the sensitivty of the vertical look.
    public int maxVertical = 90; // Controls the maximum value along the vertical axis that the player can look.
    public int minVertical = -90; // Controls the minimum value along the vertical axis that the player can look.


    void Start() // Used a tutorial, hopefully going to be expanding on this method to suit the game's needs and funtionalities.
    {
        rb = GetComponent<Rigidbody>(); // Getting the Rigidbody Component from Unity. (My guess)
    }


    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate() // Used a tutorial, hopefully going to be expanding on this method to suit the game's needs and functionalities.
    {
        PlayerRun(); // Calls upon the PlayerRun() function.
        PlayerMovement(); // Calls upon the PlayerMovement() function.
        PlayerCrouch(); // Calls upon the PlayerCrouch() function.
        PlayerLean(); // Calls upon the PlayerLean() function.
        PlayerProne(); // Calls upon the PlayerProne() function.
    }



    void PlayerMovement()
    {
        float xAxis = Input.GetAxis("Horizontal"); // Declaring xAxis and assigning the Player's position along the x-axis to it. (My guess)
        float zAxis = Input.GetAxis("Vertical"); // Declaring zAxis and assigning the Player's position along the z-axis to it. (My guess)

        Vector3 movement = new Vector3(xAxis, 0f, zAxis) * speed * Time.deltaTime; // Assigning the player's movement to the movement variable. (My guess)

        transform.position += transform.forward * xAxis + transform.forward * zAxis; // Stores the player's movement into transform forward?
        rb.MovePosition(transform.position + movement); // Adding player's input movement to the new position, allowing the player to move. (My guess)
    }

    void PlayerRun() // Creating a function that get's input in order to run.
    {
        if (Input.GetKey("left shift")) // When user presses down on the left shift, speed is set to 8.
        {
            speed = 8;
        }
        if (Input.GetKeyUp("left shift")) // When user lets go of the left shift, speed is set to 3.
        {
            speed = 3;
        }
        if (Input.GetKey("right shift")) // When user presses down on the right shift, speed is set to 8.
        {
            speed = 8;
        }
        if (Input.GetKeyUp("right shift")) // When user lets go of the right shift, speed is set to 3.
        {
            speed = 3;
        }
    }



    void PlayerCrouch()
    {
        if (Input.GetKeyDown("x"))
        {
            // Insert crouch code here
        }

        if (Input.GetKeyUp("x"))
        {
            // Insert standing up code here
        }
    }



    void PlayerLean()
    {
        if (Input.GetKeyDown("q"))
        {
            // Insert Lean Left Code Here
        }
        if (Input.GetKeyUp("q"))
        {
            // Insert Return To Normal Posture Code Here
        }
        if (Input.GetKeyDown("e"))
        {
            // Insert Lean Right Code Here
        }
        if (Input.GetKeyUp("e"))
        {
            // Insert Return To Normal Posture Code Here
        }
    }

    void PlayerProne()
    {
        // Insert Player Prone Stuff. Might have to call upon the PlayerMovement function here to change the value of speed so it is slower when crawling.
    }
}