using UnityEngine;
using System.Collections;

public class MeleeScript : MonoBehaviour {

    public GameObject bloodSplatter;

    void OnTriggerEnter2D(Collider2D target)
    {

        //Debug.Log("Target = " + target.name);

        if(target.tag == "Enemy")
        {
            Vector3 bloodPosDelta = new Vector3(0,0,0.5f);
            Instantiate(bloodSplatter,target.transform.position + bloodPosDelta, target.transform.rotation);
            Destroy(target.gameObject);
        }
            
    }
}
