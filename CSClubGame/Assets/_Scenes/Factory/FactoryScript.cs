using UnityEngine;
using System.Collections;

public class FactoryScript : CameraController
{

    // Use this for initialization
    void Start()
    {
        offset = this.transform.position - player.transform.position;

    }

    // Last Update 
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
