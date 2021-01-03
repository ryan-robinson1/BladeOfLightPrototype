using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject road;

    Queue<GameObject> roads = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateChunk(Vector3 position, Quaternion rotation)
    {
       roads.Enqueue(GameObject.Instantiate(road, position, rotation));
    }
    public void degenerateChunk()
    {
        if(roads.Count > 2)
        {
            Destroy(roads.Dequeue());
        }
    }
}
