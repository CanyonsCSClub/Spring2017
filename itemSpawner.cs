using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSpawner : MonoBehaviour
{
    public GameObject[] spawnObj;
    public Transform[] spawnPoints;
	// Use this for initialization
	void Start ()
    {
        Spawn();//calls spawn() at start of game.
	}
    void Spawn()
    {
        int spawnPointIndex, spawnObjIndex;
        /*GameObject[] tempSO=new GameObject[spawnObj.Length];//creates temp of Spawn Obj
        for (int i = spawnObj.Length; i >= 1; i--)
        {
            tempSO[i] = spawnObj[i];
        }
        Transform[] tempSP = spawnPoints;
        for (int i = spawnPoints.Length; i >= 1; i--)//creates temp of Spawn Points
        {
            tempSP[i] = spawnPoints[i];
        }*/
        for (int i = spawnPoints.Length; i >= 1; i--)//only works when spawnObj and SpawnPos have same lengths
        {
            spawnPointIndex= Random.Range(0, i);
            spawnObjIndex = Random.Range(0, i);//random int in range for both arrays excludes used ones
            Instantiate(spawnObj[spawnObjIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);//creates obj at Spawns randomly
            GameObject tempO= spawnObj[spawnObjIndex];//"deletes" spawn objects that was used, so it can no longer be used. in this method
            Transform tempP= spawnPoints[spawnPointIndex];//"deletes" spawn point that was used, so it can no longer be used. in this method
            int tempPos = 0,otherPos=0;
            for (int k = 0; k < spawnObj.Length; k++)//moves null to the end of the list
            {
                if (spawnObj[k] != spawnObj[spawnObjIndex])
                {
                    spawnObj[tempPos] = spawnObj[k];
                    tempPos++;
                }
            }
            spawnObj[tempPos] = tempO;
            for (int k = 0; k < spawnPoints.Length; k++)//moves null to the end of the list
            {
                if (spawnPoints[k] != spawnPoints[spawnPointIndex])
                {
                    spawnPoints[otherPos] = spawnPoints[k];
                    otherPos++;
                }
            }
            spawnPoints[otherPos] = tempP;
        }
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }
}
