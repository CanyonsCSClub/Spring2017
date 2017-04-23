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

	public Camera mainCamera;


	void Start() // Used a tutorial, hopefully going to be expanding on this method to suit the game's needs and funtionalities.
	{
		Cursor.visible = false;
		rb = GetComponent<Rigidbody>(); // Getting the Rigidbody Component from Unity. (My guess)
	}


	private void Update()
	{

	} 

	// Update is called once per frame
	void FixedUpdate() // Used a tutorial, hopefully going to be expanding on this method to suit the game's needs and functionalities.
	{
		PlayerRotation(); // Calls upon the PlayerRotation() function.
		//PlayerRun(); // Calls upon the PlayerRun() function.
		PlayerMovement(); // Calls upon the PlayerMovement() function.
		//PlayerCrouch(); // Calls upon the PlayerCrouch() function.
		//PlayerLean(); // Calls upon the PlayerLean() function.
		//PlayerProne(); // Calls upon the PlayerProne() function.
	}



	void PlayerMovement()
	{
		float xAxis 	= Input.GetAxis("Horizontal"); // Declaring xAxis and assigning the Player's position along the x-axis to it. (My guess)
		float zAxis 	= Input.GetAxis("Vertical"); // Declaring zAxis and assigning the Player's position along the z-axis to it. (My guess)
		//Debug.Log( "x: " + xAxis + " z: " + zAxis);

		//Converting EularAngles to Radians, Adding 90 degrees for Left Right Radian converting
		float radiansLR 	= ((mainCamera.transform.rotation.eulerAngles.y  + 90) * Mathf.PI) / 180f ;
		
		//Mathf uses Radians only!
		float modCos 		= Mathf.Cos (radians);
		float modSin 		= Mathf.Sin (radians);
		
		float modCosLR 	= Mathf.Cos (radiansLR);
		float modSinLR 	= Mathf.Sin (radiansLR);
		
		float newXDirec 	= zAxis * modSin 	+  xAxis * modSinLR; 
		float newZDirec 	= zAxis * modCos 	+ xAxis * modCosLR; 

		//Debug.Log( "x: " + modCos + " z: " + modSin + " Cam: " +  mainCamera.transform.rotation.eulerAngles.y 
		//	+  " Cam2: " +  radians  + " Pos: " + transform.position.x + " " + transform.position.z);
		
		//Debug.Log( "x: " + newXDirec + " z: " + newZDirec);
		//Debug.Log( "Cam Rot: " + mainCamera.transform.rotation.eulerAngles.y);
		
		Vector3 movement = new Vector3(newXDirec, 0f, newZDirec) * speed * Time.deltaTime;
		
		//Assigning the player's movement to the movement variable. (My guess)

		//transform.position += transform.forward * xAxis + transform.forward * zAxis; Another way to make the player move, expect it doesn't allow the player to strafe sideways left and right.

		rb.MovePosition(transform.position + movement); // Adding player's input movement to the new position, allowing the player to move. (My guess)
	}



	void PlayerRotation()
	{
		xRotation += 0 - (Input.GetAxis("Mouse Y") * lookSensitivityY); // Stores the mouse's input along the y-axis (like a hinge, rotating left and right) and multiplying it to the lookSensitivity to get the value for the xRotation. Subtracting from zero corrects the axis from being inversed.
		yRotation -= 0 - (Input.GetAxis("Mouse X") * lookSensitivityX); // Stores the mouse's input along the x-axis (like a hinge, rotating up and down) and multiplies it to the lookSensitivity to get the value for the yRotation. Subtracting from zero corrects the axis from being inversed.

		xRotation = Mathf.Clamp(xRotation, minVertical, maxVertical); // Makes sure the character has a realistic range of vertical head movement, clapms between minimumn and maximum. (Look Variable, minimum look, maximum look)

		currentRotationX = Mathf.SmoothDamp(currentRotationX, xRotation, ref xRotationV, lookSmoothDamp); // Gets the new x rotation on the x-axis by accounting for all of the variables that affect it's movement.
		currentRotationY = Mathf.SmoothDamp(currentRotationY, yRotation, ref yRotationV, lookSmoothDamp); // Gets the new y rotation on the y-axis by accounting for all of the variables that affect it's movement.

		mainCamera.transform.rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0); // Transforms from the previous rotations to the new rotations.
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