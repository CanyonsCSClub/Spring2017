using UnityEngine;
using System.Collections;

public class ImpactScript : MonoBehaviour {
    // Use this for initialization
    void Start()
    {
        Invoke("Die", 0.1f);

    }


    void Die()
    {
        Destroy(gameObject);
    }
}
