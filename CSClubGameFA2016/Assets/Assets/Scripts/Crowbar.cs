using UnityEngine;
using System.Collections;

public class Crowbar : MonoBehaviour 
{

    public GameObject bloodSplatter;

    public int damage;

    void OnTriggerEnter2D(Collider2D target)
    {

        //Debug.Log("Target = " + target.name);

        GameObject targetObject = target.gameObject;

        if (target.tag == "Enemy")
        {
            Vector3 bloodPosDelta = new Vector3(0, 0, 0.5f);
            if(bloodSplatter != null)
                Instantiate(bloodSplatter, target.transform.position + bloodPosDelta, target.transform.rotation);
            targetObject.GetComponent<Enemy>().TakeDamage(damage, transform.parent.gameObject);

        }

    }
}
