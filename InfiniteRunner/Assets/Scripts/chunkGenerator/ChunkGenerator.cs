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

    Queue<GameObject> roads = new Queue<GameObject>();
    bool firstChunkPassed = false;

    float currentSpawnXLeft = 0;
    float currentSpawnXRight = 0;
    
    private void Start()
    {
        foreach(Structure s in structures)
        {
           s.extentX = s.prefab.GetComponentInChildren<MeshFilter>().sharedMesh.bounds.extents.x;
        }
    }

    public void generateChunk(Vector3 position, Quaternion rotation)
    {
        GameObject roadSection = GameObject.Instantiate(road, position + new Vector3(180 * roads.Count, 0, -0.15f), rotation);
       roads.Enqueue(roadSection);
        generateBuildings();


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
}
