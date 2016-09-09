using UnityEngine;
using System.Collections;

public class MeleeScript : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D target)
    {

        //Debug.Log("Target = " + target.name);

        if(target.tag == "Enemy")
            Destroy(target.gameObject);
    }
}
