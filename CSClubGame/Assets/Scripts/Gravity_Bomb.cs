using UnityEngine;
using System.Collections;

public class Gravity_Bomb : MonoBehaviour {

	public CircleCollider2D bombRadius;
	private float bombColliderRadius;

    private float gravityStr = 100f;

    private float durationTime = 5f;

	void Start () {
		bombRadius = GetComponent<CircleCollider2D> ();
		bombColliderRadius = bombRadius.radius;
        Invoke("DurationEnd", durationTime);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D entering2D){
		//calculate distance and add force on the gameObject towards bomb
		float distance = 0;
		if (entering2D.gameObject != null && entering2D.gameObject.CompareTag("Player")) {
			//calculate distance to target
			distance = Vector3.Distance (this.transform.position, 
				entering2D.gameObject.transform.position); 
			//add force towards target
			if (distance < 9.5f) {
				//Debug.Log ("Pulling object");
				//Debug.Log ((entering2D.gameObject.transform.position - transform.position)
				//* (bombColliderRadius - distance) * Time.smoothDeltaTime);
				entering2D.gameObject.GetComponent<Rigidbody2D> ().AddForce ((entering2D.gameObject.transform.position - transform.position)
				* gravityStr * (bombColliderRadius - distance) * Time.smoothDeltaTime);
			}
		}
	}

	void OnTriggerStay2D(Collider2D staying2D){
		float distance = 0;
		if (staying2D.gameObject != null && staying2D.gameObject.CompareTag("Player")) {
			//calculate distance to target
			distance = Vector3.Distance (this.transform.position, 
				staying2D.gameObject.transform.position); 
			Debug.Log ("Distance: " + distance);
			//add force towards target
			Debug.Log("Pulling object");
			//Debug.Log((staying2D.gameObject.transform.position - transform.position)
			//	*(bombColliderRadius - distance) * Time.smoothDeltaTime);
			
			staying2D.gameObject.GetComponent<Rigidbody2D>().AddForce((staying2D.gameObject.transform.position - transform.position)
				* gravityStr * (bombColliderRadius - distance) * Time.smoothDeltaTime);
		}
	
	}

    void DurationEnd()
    {
        Debug.Log("Gravity Bomb ended.");
        Destroy(gameObject);
    }

}
