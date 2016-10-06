using UnityEngine;
using System.Collections;

public class HealthOrb : MonoBehaviour {

    private int healthValue;
    private Rigidbody2D hpOrb;
    private int currentPlayerHealth;
    private int currentBASE_HEALTH;

    // Use this for initialization
    void Start () {

        hpOrb = GetComponent<Rigidbody2D>();
        healthValue = 10;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime * 4);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        currentPlayerHealth = target.GetComponent<Player>().getHealth();
        currentBASE_HEALTH = target.GetComponent<Player>().getBASE_HEALTH();
        if (target.CompareTag("Player"))
        {
            if (currentPlayerHealth < currentBASE_HEALTH && target.GetComponent<Player>().getAlive())
            {
                target.GetComponent<Player>().GiveHealth(healthValue);
                Destroy(gameObject);
            }
        }
    }


}
