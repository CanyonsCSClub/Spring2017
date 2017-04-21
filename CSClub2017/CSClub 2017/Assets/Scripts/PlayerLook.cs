using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {

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

	public GameObject playerCapsule;

	// Use this for initialization
	void Start () {

		playerCapsule = GetComponentInParent<GameObject> ();

	}



	// Update is called once per frame

	//void LateUpdate () {





	//CameraRotation(); // Calls upon the CameraRotation() function.


	//}


	//	void CameraRotation() {


	//		xRotation += 0 - (Input.GetAxis("Mouse Y") * lookSensitivityY); // Stores the mouse's input along the y-axis (like a hinge, rotating left and right) and multiplying it to the lookSensitivity to get the value for the xRotation. Subtracting from zero corrects the axis from being inversed.

	//		yRotation -= 0 - (Input.GetAxis("Mouse X") * lookSensitivityX); // Stores the mouse's input along the x-axis (like a hinge, rotating up and down) and multiplies it to the lookSensitivity to get the value for the yRotation. Subtracting from zero corrects the axis from being inversed.



	//		xRotation = Mathf.Clamp(xRotation, minVertical, maxVertical); // Makes sure the character has a realistic range of vertical head movement, clamps between minimumn and maximum. (Look Variable, minimum look, maximum look)



	//		currentRotationX = Mathf.SmoothDamp(currentRotationX, xRotation, ref xRotationV, lookSmoothDamp); // Gets the new x rotation on the x-axis by accounting for all of the variables that affect it's movement.

	//		currentRotationY = Mathf.SmoothDamp(currentRotationY, yRotation, ref yRotationV, lookSmoothDamp); // Gets the new y rotation on the y-axis by accounting for all of the variables that affect it's movement.





	//		//do null check

	//		//playerCapsule.GetComponent<PlayerControl> ().changeRotation (currentRotationY);//might cause the camera to over rotate

	//		//ask tuan tb64 on discord....best way to do camera and player....

	//		//b4 that /\ look up unity fps shooter tutorial 




	//		//PlayerControl.FixedUpdate(currentRotationX);

	//	}

}