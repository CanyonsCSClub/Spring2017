using UnityEngine;
using System.Collections;

public class BulletMover : MonoBehaviour
{
    public float speed;
    public float range;
	public int damage;
    public bool isPiercing;
    private Rigidbody2D bulletBody;
    private Vector2 start;
	private GameObject playerID;

    void Start()
    {
		
        start = transform.position;
        bulletBody = GetComponent<Rigidbody2D>();
        bulletBody.AddForce(gameObject.transform.up * speed);

		//what is this for?
        if (isPiercing == null)
            isPiercing = false;
		//_________________
    }

    void FixedUpdate()
    {
        if (range < Vector2.Distance(start, transform.position))
            Destroy(this.gameObject); 
    }

    void OnTriggerEnter2D(Collider2D target)
    {

        //Debug.Log("Target = " + target.name);

        GameObject targetObject = target.gameObject;
		
		if(target.tag == "World")
			Destroy(gameObject);

        if (target.tag == "Enemy")
        {
           // Vector3 bloodPosDelta = new Vector3(0, 0, 0.5f);
           // Instantiate(bloodSplatter, target.transform.position + bloodPosDelta, target.transform.rotation);
			Debug.Log(damage);
			targetObject.GetComponent<Enemy>().TakeDamage(damage, playerID);

            //Vector3 bloodPosDelta = new Vector3(0, 0, 0.5f);
            //Instantiate(bloodSplatter, target.transform.position + bloodPosDelta, target.transform.rotation);
            //targetObject.GetComponent<ZombieScript>().TakeDamage(damage);
 

            if (!isPiercing){
                Destroy(gameObject);
                //Hit animation?
            }
        }

    }


    public void setParm(GameObject GO, float inSpeed, float inRange, int inDamage, bool inPiercing)
    {
		this.playerID = GO;
        setSpeed(inSpeed);
        setRange(inRange);
        this.damage = inDamage;
        this.isPiercing = inPiercing;
    }

    public void setSpeed(float inputSpeed)
    {
        if(inputSpeed > 0)
            speed = inputSpeed;
    }

    public void setRange(float inputRange)
    {
        if(inputRange > 0)
            range = inputRange;
    }
    
}
