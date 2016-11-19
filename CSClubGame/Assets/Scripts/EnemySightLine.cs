using UnityEngine;
using System.Collections;

public class EnemySightLine : MonoBehaviour {

	public CircleCollider2D enemySight;
	private float ColliderRadius;
	// Use this for initialization
	void Start () {
		enemySight = GetComponent<CircleCollider2D> ();
	}
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D entering2D){
		if (entering2D.gameObject.CompareTag ("Player")) {
			GetComponentInParent<Enemy>().setPlayerInRange (entering2D.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D exiting2D){
		if (exiting2D.gameObject.CompareTag ("Player")
			&& (exiting2D.gameObject == GetComponentInParent<Enemy> ().getPlayerInRange ())) {
			//Debug.Log ("Is this going on exit");
			GetComponentInParent<Enemy> ().setPlayerInRange (null);
		}
		
	}
	/*	
	void OnTriggerStay2D(Collider2D staying2D){

		if(staying2D.CompareTag("Player")){
			//Debug.Log ("aggro:" + aggroOnPlayer + "PIR: " + playerInRange);
			GameObject aggroOnPlayer = owner.GetComponent<Enemy>().getAggro();
			GameObject playerInRange = owner.GetComponent<Enemy> ().getPlayerInRange ();
			if(aggroOnPlayer == playerInRange){//if they are the same
				if (playerInRange != null) {
					owner.GetComponent<Enemy> ().Move (playerInRange);
				}
			} else { //if they aren't the same \/

				if (staying2D.gameObject == playerInRange && aggroOnPlayer == null) {//if the other in range is the same target as the
					//playerInRange and the enemy is not aggro'd
					owner.GetComponent<Enemy> ().Move (staying2D.gameObject);
				} else if(staying2D.gameObject != playerInRange && aggroOnPlayer == null){
					//if the player in the collider is not the playerInRange 
					owner.GetComponent<Enemy> ().Move(playerInRange);
					//ignore them
				} else if (aggroOnPlayer != null) { //if the enemy is aggro'd on somebody
					if ((aggroOnPlayer.transform.position - this.transform.position).sqrMagnitude < ColliderRadius * ColliderRadius) {
						//if the aggro'd target is in range move to them
						owner.GetComponent<Enemy> ().Move (aggroOnPlayer);
					}
				} else {
					owner.GetComponent<Enemy> ().Move ();
				}

			}

		} else {

			//if the tag is not a player do.....
		}
	}
	*/


	public void setColliderRadius(float val){
        if(enemySight == null)
        {
            Debug.Log("Enemy Sight is null");
            return;
        }
		enemySight.radius = val;
		Debug.Log ("Radius" + enemySight.radius);
	}
}
