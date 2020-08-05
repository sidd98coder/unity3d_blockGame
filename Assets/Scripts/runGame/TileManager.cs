using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    private Transform PlayerTransform;
    public GameObject[] prefabs;
    private int tileLength = 20;
    private float Zpos = 10f;
    int bridgeLength = 5;
    private List<GameObject> activeList;
    private GameObject Player;
    private float safeZone = 10;
    private int lastPrefabIndex = 0; 
    void Start()
    {
        activeList = new List<GameObject>();
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerTransform = Player.transform;
        for(int i = 0; i < bridgeLength; i++)
        {
            if (i <= 1)
            {
                spawnTile(0);
            }
            else
            {
                spawnTile();
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)
        {
            if (PlayerTransform.position.z - safeZone > Zpos - (tileLength * (bridgeLength - 1)))
            {
                spawnTile();
                deleteTile();
            }
        }
    }
    void spawnTile(int tileIndex=-1)
    {
        GameObject go;
        if (tileIndex == -1)
        {
            go = Instantiate(prefabs[randomPrefabIndex()]) as GameObject;
        }
        else
        {
            go = Instantiate(prefabs[tileIndex]) as GameObject;
        }
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * Zpos;
        Zpos += tileLength;
        activeList.Add(go);
    }

    void deleteTile()
    {
        Destroy(activeList[0]);
        activeList.RemoveAt(0);
    }
    int randomPrefabIndex()
    {
        if (prefabs.Length <= 1)
        {
            return 0;
        }
        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, prefabs.Length);
        }
        return randomIndex;
    }
}
