
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //attached with TileHolder(empty object)

    int destroyTime = 20;       //objects gets destroyed by this time
    public GameObject[] Path;       //holds tile prefabs
    
    
    public GameObject[] Objects;        //holds obstacle, coin and candy prefabs
    private List<Vector3> objectSpawnPoints;            //(1.3, 1, 3.5)    (0, 1, 0.5)   (-1.3, 1, 6)

    public List<GameObject> activeList;     //adding created objects in a list for deleting
    
    public List<Transform> Tile;            //hold transforms of every tile that will be created

    



    private void Start()
    {
        
        activeList = new List<GameObject>();
        Tile = new List<Transform>();
        //spawnTile();
        Instantiate(Path[1], transform.position, transform.rotation);
    }


    

    public void spawnTile(int randomSpawnPoint=1)
    {
        //print("transform  rotation : " + transform.rotation.eulerAngles);
        GameObject go;
        transform.Translate(RelativePointsForNextTile(randomSpawnPoint));                                          //translateing to next position
        transform.Rotate(0f, RotateByAngle(randomSpawnPoint), 0f, Space.World);                                  //then rotating empty game object
        go = Instantiate(Path[randomSpawnPoint], transform.position, transform.rotation) as GameObject;         //spawning tile at empty game object transforms
        
        activeList.Add(go);     //adding spawned tile to list
        Tile.Add(go.transform);        //adding transforms of each tile to a list, later used for positioning on lane
       
        objectSpawnPoints = new List<Vector3>();
        //creating objects spawn points relative to each tile centre
        for (int i =0; i<3; i++)
            objectSpawnPoints.Add(transform.TransformPoint(RelativePointsForNextObject(i)));
        SpawnObjects(objectSpawnPoints);
    }


    private void SpawnObjects(List<Vector3> spawnPoints)
    {
        for (int i = 0; i < 3; i++)                    // spawn points length = 3
        {
            int obstacle = Random.Range(0, 2);                      //50% chances of getting obstacle
            if (obstacle == 0)
            {
                GameObject go1 = Instantiate(Objects[0], spawnPoints[i], transform.rotation);
                Destroy(go1, destroyTime);
            }
            else
            {
                int coin = Random.Range(0, 4);                      //37.5% chances of getting coin
                if (coin != 0)
                {
                    GameObject go2 = Instantiate(Objects[1], spawnPoints[i], transform.rotation);
                    Destroy(go2, destroyTime);
                }                                                   //12.5% chances of getting candy
                else
                {
                    GameObject go3 = Instantiate(Objects[2], spawnPoints[i], transform.rotation);
                    Destroy(go3, destroyTime);
                }
            }
        }


    }

    Vector3 RelativePointsForNextTile(int num)      //fuctions returns Vector3 for nect tile spawning
    {
        Vector3 points;
        switch (num)
        {
            case 0:
                points = new Vector3(-5.25f, 0f, 10.75f);
                break;
            case 2:
                points = new Vector3(5.25f, 0f, 10.75f);
                break;
            default:
                points = new Vector3(0f, 0f, 16f);
                break;
        }
        return points;
    }

    float RotateByAngle(int num)        //function returns float by which tile should be rotated
    {
        float angle;
        switch (num)
        {
            case 0:
                angle = -90f;
                break;
            case 2:
                angle = 90f;
                break;
            default:
                angle = 0f;
                break;
        }
        return angle;
    }

    Vector3 RelativePointsForNextObject(int num)
    {
        Vector3 points;
        switch (num)
        {
            case 0:
                points = new Vector3(0f, 1f, 7f);       //SP1
                break;
            case 1:
                points = new Vector3(1.6f, 1f, 3.5f);   //SP2
                break;
            case 2:
                points = new Vector3(-1.6f, 1f, -0.15f);    //SP3
                break;
            default:
                points = Vector3.zero;
                break;
        }
        return points;
    }
    public void deleteTile()
    {
        Destroy(activeList[0]);
        activeList.RemoveAt(0);
    }

}
