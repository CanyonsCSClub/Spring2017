using UnityEngine;
using System.Collections;

public class Medkit : MonoBehaviour 
{
	private int hpValue;
	private Rigidbody2D kitBody;

	void Start () 
	{
		float speed = 50f;
		kitBody = GetComponent<Rigidbody2D> ();
		hpValue = 50;
		Vector2 movement = new Vector2 (Random.Range (-1f, 1f), Random.Range (-1f, 1f));
		movement.Normalize ();
		kitBody.AddForce (movement * speed);
	}
	

	void Update () 
	{
		transform.Rotate (new Vector3 (0, 0, 45) * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D target) 
	{
		if(target.CompareTag("Player"))
		{
			target.GetComponent<PlayerController> ().GiveHealth (hpValue);
			Destroy (gameObject);
		}
	}
}
