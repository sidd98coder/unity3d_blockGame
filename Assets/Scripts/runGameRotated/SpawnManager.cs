
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int tileNum = 0;
    int destroyTime = 20;
    public GameObject[] Path;
    public Transform tileHolderPosition;                           // (0, -0.5, 8)
    public int lastPrefabIndex = 0;
    public int secondLastPrefabIndex=2;             //left right pattern

    
    public GameObject[] Objects;
    private List<Vector3> objectSpawnPoints;            //(1.3, 1, 3.5)    (0, 1, 0.5)   (-1.3, 1, 6)

    public List<GameObject> activeList;
    private float safeZone = 8f;
    public List<Transform> Tile;

    



    private void Start()
    {
        tileHolderPosition = GetComponent<Transform>().transform;
        activeList = new List<GameObject>();
        Tile = new List<Transform>();
        spawnTile();
        
    }


    /*private void Update()
    {
        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, 3);
        }
        //secondLastPrefabIndex = lastPrefabIndex;
        lastPrefabIndex = randomIndex;
        spawnTile(randomIndex);
    }*/

    public void spawnTile(int randomSpawnPoint=-1)
    {
        GameObject go;
        

        if (randomSpawnPoint == 0)
        {
            //print(randomSpawnPoint);

            transform.position += transform.TransformDirection(new Vector3(-5.25f, 0f, 10.75f));
            //tileHolderPosition.transform.localPosition += new Vector3(-5.25f, 0f, 10.75f);
            tileHolderPosition.Rotate(0f, -90f, 0f, Space.Self);
            go = Instantiate(Path[1], tileHolderPosition.position, tileHolderPosition.rotation) as GameObject;
            



        }
        else if (randomSpawnPoint == 1)
        {
            //print(randomSpawnPoint);
            //tileHolderPosition.transform.localPosition += new Vector3(0.0f, 0.0f, 16f);
            transform.position += transform.TransformDirection(new Vector3(0.0f, 0.0f, 16f));

            tileHolderPosition.Rotate(0, 0, 0, Space.Self);
            go = Instantiate(Path[0], tileHolderPosition.position, tileHolderPosition.rotation) as GameObject;
            



        }
        else if (randomSpawnPoint == 2)
        {
            //print(randomSpawnPoint);

            transform.position += transform.TransformDirection(new Vector3(5.25f, 0f, 10.75f));
            //tileHolderPosition.transform.localPosition += new Vector3(5.25f, 0f, 10.75f);
            tileHolderPosition.Rotate(0, +90f, 0);
            go = Instantiate(Path[2], tileHolderPosition.position, tileHolderPosition.rotation) as GameObject;
            


        }
        else
        {
            //print(randomSpawnPoint);
            go = Instantiate(Path[0], tileHolderPosition.position, tileHolderPosition.rotation) as GameObject;
            
        }
        Tile.Add(transform);
        
        

        activeList.Add(go);
        objectSpawnPoints = new List<Vector3>();

        /*objectSpawnPoints.Add(transform.position + new Vector3(0f, 1f, 6f));  ///transform.TransformPoint(0f, 1f, 6f);
        objectSpawnPoints.Add(transform.position + new Vector3(1.55f, 1f, -2f)); ///transform.TransformPoint(1.55f, 1f, -2f);
        objectSpawnPoints.Add(transform.position + new Vector3(-1.5f, 1f, 1f));*/ ///transform.TransformPoint(-1.5f, 1f, 1f);
        objectSpawnPoints.Add(transform.TransformPoint(1.3f, 1f, 3.5f));
        objectSpawnPoints.Add(transform.TransformPoint(0f, 1f, 0.5f));
        objectSpawnPoints.Add(transform.TransformPoint(-1.3f, 1f, 6f));
        objectSpawner(objectSpawnPoints);
        //print(objectSpawnPoints[0]);
    }

    public void deleteTile()
    {
        Destroy(activeList[0]);
        activeList.RemoveAt(0);
    }

    private void objectSpawner(List<Vector3> spawnPoints)
    {
        
        for (int i = 0; i < 3; i++)                    // spawn points length
        {
            int obstacle = Random.Range(0, 2);                      //50% chances of getting obstacle
            
            if (obstacle == 0)
            {
                
                GameObject go1 = Instantiate(Objects[0], spawnPoints[i], transform.rotation);
                Destroy(go1, destroyTime);
            }
            else
            {
                int coin = Random.Range(0, 3);                      //33% chances of getting coin
                if (coin != 0)
                {
                    GameObject go2 = Instantiate(Objects[1], spawnPoints[i], transform.rotation);
                    Destroy(go2, destroyTime);
                }                                                   //17% chances of getting candy
                else
                {
                    GameObject go3 = Instantiate(Objects[2], spawnPoints[i], transform.rotation);
                    Destroy(go3, destroyTime);
                }
            }
               
        }
        
        
    }
}
