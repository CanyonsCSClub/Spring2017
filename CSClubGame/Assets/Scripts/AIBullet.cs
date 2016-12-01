using UnityEngine;
using System.Collections;

public class AIBullet : MonoBehaviour
{
    private float speed;
    private float range;
    private int damage;
    private bool isPiercing;
    private Rigidbody2D bulletBody;
    private Vector2 start;
    private GameObject playerID;
    protected string[] targetTags;

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (range < Vector2.Distance(start, transform.position))
            Destroy(this.gameObject);
    }

    void Shoot()
    {
        start = transform.position;
        bulletBody = GetComponent<Rigidbody2D>();
        bulletBody.AddForce(gameObject.transform.up * speed);
        //Debug.Log("AI bullet speed " + speed);
        //what is this for?
        if (isPiercing == null)
            isPiercing = false;
        //_________________
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        //Debug.Log("Hit object " + target.tag);
        bool validTarget = ValidTag(target.tag);

        GameObject targetObject = target.gameObject;

        if (target.tag == "World")
            Destroy(gameObject);

        if (validTarget)
        {
            // Vector3 bloodPosDelta = new Vector3(0, 0, 0.5f);
            // Instantiate(bloodSplatter, target.transform.position + bloodPosDelta, target.transform.rotation);
            //Debug.Log(damage);
            targetObject.GetComponent<Player>().TakeDamage(damage, playerID);

            //Vector3 bloodPosDelta = new Vector3(0, 0, 0.5f);
            //Instantiate(bloodSplatter, target.transform.position + bloodPosDelta, target.transform.rotation);
            //targetObject.GetComponent<ZombieScript>().TakeDamage(damage);


            if (!isPiercing)
            {
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

        Shoot();
    }


    public void setParm(GameObject GO, float inSpeed, float inRange, int inDamage, bool inPiercing, string[] targets)
    {
        this.playerID = GO;
        setSpeed(inSpeed);
        setRange(inRange);
        this.damage = inDamage;
        this.isPiercing = inPiercing;
        this.targetTags = targets;

        Shoot();
    }

    public void setSpeed(float inputSpeed)
    {
        if (inputSpeed > 0.0f)
            speed = inputSpeed;
        else
            Debug.Log("Speed Change Failed");
    }

    public void setRange(float inputRange)
    {
        if (inputRange > 0.0f)
            range = inputRange;
        else
            Debug.Log("Range Change Failed");
    }

    public bool ValidTag(string tag)
    {
        bool isValid = false;

        if (tag == null || this.targetTags == null)
            return false;

        foreach(string target in this.targetTags)
        {
            if(tag.Contains(target))
            {
                Debug.Log("Valid Tag" + tag);
                isValid = true;
                break;
            }
        }
        return isValid;
    }
}
