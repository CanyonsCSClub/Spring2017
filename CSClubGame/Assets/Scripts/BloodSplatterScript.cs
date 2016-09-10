using UnityEngine;
using System.Collections;

public class BloodSplatterScript : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Invoke("Die", 10f);

    }


    void Die()
    {
        Destroy(gameObject);
    }
}
