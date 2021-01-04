using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject road;
    [SerializeField]
    private float alleyWidth = 7f;      //Distance between buildings
    [SerializeField]
    private GameObject startingChunk;
    [SerializeField]
    private Structure[] structures;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject healthPack;
    [SerializeField]
    private GameObject streetLight;
    [SerializeField]
    private GameObject roadPatch;

    Queue<GameObject> roads = new Queue<GameObject>();
    bool firstChunkPassed = false;

    float currentSpawnXLeft = 0;
    float currentSpawnXRight = 0;
    float streetLightSpawnX = 0;
    float roadPatchX = 0;
    private ArrayList enemySpawnPoints = new ArrayList();
    private void Start()
    {
        foreach(Structure s in structures)
        {
           s.extentX = s.prefab.GetComponentInChildren<MeshFilter>().sharedMesh.bounds.extents.x;
        }
    }

    public void generateChunk(Vector3 position, Quaternion rotation)
    {
        GameObject roadSection = GameObject.Instantiate(road, position + new Vector3(180 * roads.Count, 0, 0), rotation);
       roads.Enqueue(roadSection);
       generateBuildings();
       spawnEnemies();
       spawnStreetLights();
       //spawnRoadPatch();


    }
    public void spawnRoadPatch()
    {
        GameObject.Instantiate(roadPatch, new Vector3(roadPatchX, 0.1726f, 0), Quaternion.identity);
        roadPatchX += 180;
    }
    public void generateBuildings()
    {
        for(int i = 0; i < 8; i++)
        {
            int buildingNum = Random.Range(0, structures.Length);
            Vector3 spawnPosition = structures[buildingNum].spawnPosition + new Vector3(currentSpawnXLeft + structures[buildingNum].extentX, 0, 0);
            GameObject.Instantiate(structures[buildingNum].prefab, spawnPosition, Quaternion.identity);
            currentSpawnXLeft += (structures[buildingNum].extentX * 2) + alleyWidth;
        }
        for (int i = 0; i < 8; i++)
        {
            int buildingNum = Random.Range(0, structures.Length);
            Vector3 spawnPosition = new Vector3(0, structures[buildingNum].spawnPosition.y, structures[buildingNum].spawnPosition.z*-1) + new Vector3(currentSpawnXRight + structures[buildingNum].extentX, 0, 0);
            GameObject.Instantiate(structures[buildingNum].prefab, spawnPosition, Quaternion.Euler(0,180,0));
            currentSpawnXRight += (structures[buildingNum].extentX * 2) + alleyWidth;
        }
    }
    public void degenerateChunk()
    {
        if (firstChunkPassed)
        {
            Destroy(roads.Dequeue());
        }
        else
        {
            Destroy(startingChunk);
            firstChunkPassed = true;
        }
       
    }
    void spawnStreetLights()
    {
        for (int i = 0; i < 8; i++)
        {
            if(Random.Range(0,5) != 2)
            {
                GameObject.Instantiate(streetLight, new Vector3(streetLightSpawnX, 2, 5), Quaternion.identity);
            }
            if (Random.Range(0, 5) != 3)
            {
                GameObject.Instantiate(streetLight, new Vector3(streetLightSpawnX, 2, -5), Quaternion.identity);
            }
            streetLightSpawnX += 35;
        }
    }
    void spawnEnemies()
    {

    /*    float[] possibleZCoor = { -3.5f, -1.75f, 0, 1.75f, 3.5f };
        int xCoor = Random.Range(0, 18) * 10;
        *//*while (enemySpawnPoints.Contains(xCoor))
        {
            xCoor = Random.Range(0, 30) * 10;
        }
        *//*
        enemySpawnPoints.Add(xCoor);

        float zCoor = possibleZCoor[Random.Range(0, 5)];

        if (Random.Range(0, 25) == 1)
        {
            Instantiate(healthPack, new Vector3(Mathf.RoundToInt(player.transform.position.x) + 90 + xCoor, 0.933f, zCoor), Quaternion.identity);
        }
        else
        {
            Instantiate(enemy, new Vector3(Mathf.RoundToInt(player.transform.position.x) + 90 + xCoor, 0.72f, zCoor), Quaternion.Euler(0, -90, 0));
        }*/



    }
}
