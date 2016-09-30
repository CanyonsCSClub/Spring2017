using UnityEngine;
using System.Collections;

public class ExpOrb : MonoBehaviour 
{
	private int expValue;
	private Rigidbody2D orbBody;

	void Start () 
	{
		float speed = 50f;
		orbBody = GetComponent<Rigidbody2D> ();
		expValue = 10;
		Vector2 movement = new Vector2 (Random.Range (-1f, 1f), Random.Range (-1f, 1f));
		movement.Normalize ();
		orbBody.AddForce (movement * speed);
	}

	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if(target.CompareTag("Player"))
		{
		target.GetComponent<PlayerController> ().GiveExp (expValue);
		}
	}
}