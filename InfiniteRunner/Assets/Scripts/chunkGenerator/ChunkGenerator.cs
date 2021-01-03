using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject road;

    Queue<GameObject> roads = new Queue<GameObject>();
    bool firstChunkPassed = false;

    public void generateChunk(Vector3 position, Quaternion rotation)
    {
       roads.Enqueue(GameObject.Instantiate(road, position + new Vector3(180 * roads.Count, 0, 0), rotation));
    }
    public void degenerateChunk()
    {
        if (firstChunkPassed)
        {
            Destroy(roads.Dequeue());
        }
        else firstChunkPassed = true;
       
    }
}
