using UnityEngine;
using System.Collections;

public class DoorwaySpawner : MonoBehaviour {

    public GameObject[] spawners;
    public GameObject enemy;
    public float spawnCooldown;

    private float nextSpawnTime;

	// Use this for initialization
	void Start () {
        nextSpawnTime = 0f;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.time < nextSpawnTime)
            return;
        if (!other.tag.Contains("Player"))
            return;
        Debug.Log("Spawner Triggered");
        foreach(GameObject spawner in spawners)
        {
            Instantiate(enemy, spawner.transform.position, spawner.transform.rotation );
            nextSpawnTime = Time.time + spawnCooldown;
        }
        //Destroy(gameObject);
    }
}
