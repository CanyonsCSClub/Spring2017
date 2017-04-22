using UnityEngine;
using System.Collections;

public class ZombieDead : MonoBehaviour {

    public GameObject[] drops;
	// Use this for initialization
	void Start ()
    {
        Invoke("Die", 1f);
	
	}
	

	void Die ()
    {
        ItemDrop();
        Destroy(gameObject);
	}

    void ItemDrop()
    {
        if (drops == null)
            return;
        float randomNum = Random.value;
        int dropIndex = (int)(randomNum * ((float)drops.Length));
        Debug.Log("Dropping item #" + dropIndex + " RandomNum=" + randomNum + " " + (randomNum * ((float)drops.Length)) + " drops#" + drops.Length);

        if (dropIndex == drops.Length)
            dropIndex = drops.Length - 1;

        Instantiate(drops[dropIndex], transform.position, transform.rotation);
    }
}
