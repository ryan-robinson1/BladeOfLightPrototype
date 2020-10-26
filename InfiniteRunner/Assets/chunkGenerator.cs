using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chunkGenerator : MonoBehaviour
{
    [SerializeField]
    public GameObject[] chunks;
    [SerializeField]
    private float chunkSize = 300;
    [SerializeField]
    private GameObject player;

    private int relativePosition;
    private int distanceTraveled;

    private int lastPositionSpawned = 150;
    private Queue<GameObject> renderedObjects = new Queue<GameObject>();
  
    private void Update()
    {
        Debug.Log(relativePosition);
        if (relativePosition >= chunkSize)
        {
            relativePosition = 0;
            distanceTraveled = Mathf.RoundToInt(player.transform.position.x);
        }
        else
        {
            relativePosition = Mathf.RoundToInt(player.transform.position.x)-distanceTraveled;
        }
        if (relativePosition == 150)

        {
            int nextSpawnPosition = distanceTraveled + Mathf.RoundToInt(relativePosition) + 300;
            if (lastPositionSpawned != nextSpawnPosition)
            {
                renderedObjects.Enqueue(Instantiate(chunks[Random.Range(0, 3)], new Vector3(nextSpawnPosition, 0, 0), Quaternion.Euler(0, 0, 0)));

                if (renderedObjects.Count > 2)
                {
                    Destroy(renderedObjects.Dequeue());
                }
            }

            lastPositionSpawned = nextSpawnPosition;
        }
    }

}
