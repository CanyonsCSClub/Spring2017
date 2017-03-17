using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	#region Variables
	//Public Vars_________________________________
	public Transform[] waypoints;
	public float moveSpeed;

	//Components__________________________________
	private Transform currentWaypoint;
	private Rigidbody enemyBody;
	//eventually the cone field of vision

	private bool patrolling = true;
			//we talked about the enemy patroling versus staying in one place
	private bool patrolComplete;
	private int waypointNum = 0;


	#endregion




	void Start () {
		enemyBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		//is player in view
			//patrolling = false;
			//ChasePlayer();
		if (patrolling) {
			Patrol ();
		}
		//check time of day...if "close" to morning....disappear 
		//player in view? chase
	}

	private void Patrol(){
		Transform currentPos = this.transform;
		if (currentPos.position.x == waypoints [waypointNum].position.x &&
		    currentPos.position.z == waypoints [waypointNum].position.z) {
			if (waypointNum >= waypoints.Length - 1) {
				patrolComplete = true;
			} else if (waypointNum == 0) {
				patrolComplete = false;
			}

			if (patrolComplete == false) {
				waypointNum++;
			} else {
				waypointNum--;
			}

			MoveTo (waypoints [waypointNum]);
		} else {
			MoveTo (waypoints [waypointNum]);
		}

	}

	void MoveTo (Transform destination){
		if (destination != null) {
			//rotating towards target

			//float y = Mathf.Atan2 ((destination.transform.position.y - this.transform.position.y),
			//	(destination.transform.position.x - this.transform.position.x)) * Mathf.Rad2Deg - 90;
			//transform.eulerAngles = new Vector3 (0,y,0);
			transform.LookAt(destination);
			// Moving to Target

			transform.position = Vector3.MoveTowards (transform.position, 
				destination.transform.position, moveSpeed * Time.deltaTime);
		}
	}

	void ChasePlayer(GameObject player){
		//check player....only time this will be called is if the player is seen
	}

	void Dissapear(){

	}


}
/*Notes:
 * 	No need for health and other utilities because the "dad" is unkillable....only avoidable
 * 
 * 	going to need to referance to time of day...once it is day enemy disapears
 * 
 * 	what happens if the player is being chased while it goes to day. 
 * 
 * 
 * 	We talked about the player being forced to wake up "MOM"
 */