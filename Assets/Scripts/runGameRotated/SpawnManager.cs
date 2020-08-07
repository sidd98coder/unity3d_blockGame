using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    
    public GameObject[] Path;
    private Transform tileHolderPosition;
    private int lastPrefabIndex = 0;
    private int secondLastPrefabIndex=2;                    //left right pattern

    private void Start()
    {
        tileHolderPosition = GetComponent<Transform>().transform;
        spawnTile(5);
    }

    private void Update()
    {
        
        
        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex || randomIndex == secondLastPrefabIndex)
        {
            randomIndex = Random.Range(0, 3);
        }
        secondLastPrefabIndex = lastPrefabIndex;
        lastPrefabIndex = randomIndex;
        spawnTile(randomIndex);
    }
    /*void OnTriggerEnter(Collider hit)
    {
        //player has hit the Spawn Checker collider
        if (hit.gameObject.tag == "spawnCheck")
        {
            
            int randomSpawnPoint = Random.Range(0, 3);
            spawnTile(randomSpawnPoint);
            
        }
    }
    */
    void spawnTile(int randomSpawnPoint)
    {
        GameObject go;

        if (randomSpawnPoint == 0)
        {
            print(randomSpawnPoint);

            transform.position += transform.TransformDirection(new Vector3(-5.25f, 0f, 10.75f));
            //tileHolderPosition.transform.localPosition += new Vector3(-5.25f, 0f, 10.75f);
            tileHolderPosition.Rotate(0f, -90f, 0f, Space.Self);
            go = Instantiate(Path[1], tileHolderPosition.position, tileHolderPosition.rotation) as GameObject;


        }
        else if (randomSpawnPoint == 1)
        {
            print(randomSpawnPoint);
            //tileHolderPosition.transform.localPosition += new Vector3(0.0f, 0.0f, 16f);
            transform.position += transform.TransformDirection(new Vector3(0.0f, 0.0f, 16f));

            tileHolderPosition.Rotate(0, 0, 0, Space.Self);
            go = Instantiate(Path[0], tileHolderPosition.position, tileHolderPosition.rotation) as GameObject;


        }
        else if (randomSpawnPoint == 2)
        {
            print(randomSpawnPoint);

            transform.position += transform.TransformDirection(new Vector3(5.25f, 0f, 10.75f));
            //tileHolderPosition.transform.localPosition += new Vector3(5.25f, 0f, 10.75f);
            tileHolderPosition.Rotate(0, +90f, 0);
            go = Instantiate(Path[1], tileHolderPosition.position, tileHolderPosition.rotation) as GameObject;


        }
        else
        {
            print(randomSpawnPoint);
            go = Instantiate(Path[0], tileHolderPosition.position, tileHolderPosition.rotation) as GameObject;
        }
            
    }
}
