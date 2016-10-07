using UnityEngine;
using System.Collections;

public class AmmoBox : MonoBehaviour 
{
	private int ammoValue;
	private Rigidbody2D boxBody;

	void Start () 
	{
		float speed = 50f;
		boxBody = GetComponent<Rigidbody2D> ();
		ammoValue = 100;
		Vector2 movement = new Vector2 (Random.Range (-1f, 1f), Random.Range (-1f, 1f));
		movement.Normalize ();
		boxBody.AddForce (movement * speed);
	}


	void Update () 
	{
		transform.Rotate (new Vector3 (0, 0, 45) * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D target) 
	{
		if(target.CompareTag("Player"))
		{
			target.GetComponent<Player>().GiveAmmo(10);
			Destroy (gameObject);
		}
	}
}
