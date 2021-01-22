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
    private GameObject pistolEnemy;
    [SerializeField]
    private GameObject turretEnemy;
    [SerializeField]
    private GameObject healthPack;
    [SerializeField]
    private GameObject streetLight;
    [SerializeField]
    private GameObject streetLightOff;
    [SerializeField]
    private GameObject roadPatch;

    Queue<GameObject> roads = new Queue<GameObject>();
    bool firstChunkPassed = false;
    bool firstSpawn = true;

    float currentSpawnXLeft = 0;
    float currentSpawnXRight = 0;
    float streetLightSpawnX = 0;
    float roadPatchX = 0;
    float enemiesX = 0;

    int spaceBetweenEnemies = 6;
    int previousBuildingIndexLeft = -1;
    int previousBuildingIndexRight = -1;

    int roadBoundX = 0;

    private List<float> enemySpawnPoints = new List<float>();
    Dictionary<float,int> zCoordTranslations = new Dictionary<float, int>(){
    {3.5f,1},
    {1.75f,2},
    {0,3},
    {-1.75f,4},
    {-3.5f,5}
        };

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
        roadBoundX = (int)(roadSection.transform.position.x) + 90;
       generateBuildings();
       spawnEnemies();
       spawnStreetLights();
       spawnPowerUps();
       //spawnRoadPatch();


    }
    public void spawnRoadPatch()
    {
        GameObject.Instantiate(roadPatch, new Vector3(roadPatchX, 0.1726f, 0), Quaternion.identity);
        roadPatchX += 180;
    }
    public void generateBuildings()
    {
        for(int i = 0; i < 12; i++)
        {
            int buildingNum = buildingIndex(previousBuildingIndexLeft);
            Vector3 spawnPosition = structures[buildingNum].spawnPosition + new Vector3(currentSpawnXLeft + structures[buildingNum].extentX, 0, 0);
            GameObject.Instantiate(structures[buildingNum].prefab, spawnPosition, Quaternion.identity);
            currentSpawnXLeft += (structures[buildingNum].extentX * 2) + alleyWidth;
            previousBuildingIndexLeft = buildingNum;
            if (currentSpawnXLeft > roadBoundX) break;
        }
        for (int i = 0; i < 12; i++)
        {
            int buildingNum = buildingIndex(previousBuildingIndexRight);
            Vector3 spawnPosition = new Vector3(0, structures[buildingNum].spawnPosition.y, structures[buildingNum].spawnPosition.z*-1) + new Vector3(currentSpawnXRight + structures[buildingNum].extentX, 0, 0);
            GameObject.Instantiate(structures[buildingNum].prefab, spawnPosition, Quaternion.Euler(0,180,0));
            currentSpawnXRight += (structures[buildingNum].extentX * 2) + alleyWidth;
            previousBuildingIndexRight = buildingNum;
            if (currentSpawnXRight > roadBoundX) break;
        }
    }
    /*
     * Returns a structures array index that corresponds to the correct next building to spawn based on structure parameters
     * 
     * @param previousBuildingIndex The index of the last building spawned
     * 
     * @return the structures array index of the building that should be spawned
     */
    private int buildingIndex(int _previousBuildingIndex)
    {
        List<int> weightedIndexes = new List<int>();
        HashSet<int> blackListedIndexes = new HashSet<int>();
        if (_previousBuildingIndex != -1 && structures[_previousBuildingIndex].height == 1)
        {
            for (int i = 0; i < structures.Length; i++)
            {
                int h = structures[i].height;
                if (h == 1 || h == 2)
                {
                    blackListedIndexes.Add(i);
                }
            }
        }
        blackListedIndexes.Add(_previousBuildingIndex);
        for (int i = 0; i < structures.Length; i++)
        {
            if (!blackListedIndexes.Contains(i))
            {
                for(int j=0;j< structures[i].probabilityMultiplier; j++)
                {
                    weightedIndexes.Add(i);
                }
            }
        }

        return weightedIndexes[Random.Range(0, weightedIndexes.Count)];
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
        GameObject _streetLight1 = streetLight;
        GameObject _streetLight2 = streetLight;
        if (Random.Range(0, 10) == Random.Range(0, 10))
        {
     
            _streetLight1 = streetLightOff;
        }
        else if(Random.Range(0, 10) == Random.Range(0, 10))
        {

            _streetLight2 = streetLightOff;
        }
      
        for (int i = 0; i < 10; i++)
        {
            if(Random.Range(0,5) != 2)
            {
                GameObject.Instantiate(_streetLight1, new Vector3(streetLightSpawnX+Random.Range(3,15), 0, 5), Quaternion.identity);
            }

            if (Random.Range(0, 5) != 3)
            {
                GameObject.Instantiate(_streetLight2, new Vector3(streetLightSpawnX - Random.Range(5, 15), 0, -5), Quaternion.Euler(0,180,0));
            }
            streetLightSpawnX += 55;

            if (streetLightSpawnX > roadBoundX) break;
        }
    }
    void spawnPowerUps()
    {
        if (Random.Range(0, 3) == 0)
        {
            Instantiate(healthPack, new Vector3(enemiesX, 0, 0) + returnRandomSpawnPoint(), Quaternion.identity);
            Debug.Log("Big health");

        }

    }
    private Vector3 returnRandomSpawnPoint()
    {
        float[] possibleZCoor = { -3.5f, -1.75f, 0, 1.75f, 3.5f };
        float x = Random.Range(0, 180);
        float z = possibleZCoor[Random.Range(0, 5)];
        float y = 0.6f;

        return new Vector3(x, y, z);
    }
    void spawnEnemies()
    {


        for(int i = 0; i < Random.Range(5,10);i++)
        {
            enemySpawnPoints.Add(returnEnemyXSpawnPoint());  
        }
        enemySpawnPoints.Sort();
      

        List<Vector3> spawnPointVectors = createVector3FromX(enemySpawnPoints);
        enemySpawnPoints.Clear();

        spaceOutSpawnPoints(ref spawnPointVectors);

        printVector3List(spawnPointVectors);


        for(int i = 0; i < spawnPointVectors.Count; i++)
        {
            if (enemiesX + spawnPointVectors[i].x > 50)
            {
                float random = Random.Range(0f, 1f);
                if (i > 0 && spawnPointVectors[i].x-spawnPointVectors[i-1].x>spaceBetweenEnemies && random <0.85)
                {
                    Instantiate(pistolEnemy, new Vector3(enemiesX, 0.5f, 0) + new Vector3(spawnPointVectors[i].x,0,spawnPointVectors[i].z), Quaternion.Euler(0, -90, 0));
                }
                else if(i > 0 && spawnPointVectors[i].x - spawnPointVectors[i - 1].x > spaceBetweenEnemies && random >= 0.85)
                {
                 
                   Instantiate(turretEnemy, new Vector3(enemiesX, 0.24f, 0) + new Vector3(spawnPointVectors[i].x, 0, spawnPointVectors[i].z), Quaternion.Euler(0, -90, 0));
                }
             
            }
        }

        enemiesX += 180;
    }
    private float returnEnemyXSpawnPoint()
    {
        float xCoor = Random.Range(0, 180);
        return xCoor;
    }
    private List<Vector3> createVector3FromX(List<float> xCoords)
    {
       float[] possibleZCoor = { -3.5f, -1.75f, 0, 1.75f, 3.5f };
       List<Vector3> spawnPoints = new List<Vector3>();
       foreach(float x in xCoords)
        {
            float z = possibleZCoor[Random.Range(0, 5)];
            spawnPoints.Add(new Vector3(x, 0.43f, z));
        }
        return spawnPoints;
    }
    private void spaceOutSpawnPoints(ref List<Vector3> list)
    {
        for(int i = 1; i < list.Count; i++)
        {
            int delta = spaceBetweenEnemies * (Mathf.Abs(zCoordTranslations[list[i - 1].z] - zCoordTranslations[list[i].z])+1);
           
            if (list[i].x-list[i-1].x < delta && list[i].x - list[i - 1].x >= 0)
            {
               
                list[i] = new Vector3(delta, list[i].y, list[i].z) + new Vector3(list[i - 1].x,0,0);
            }
            else if(list[i].x - list[i - 1].x < 0 && list[i - 1].x- list[i].x < delta)
            {
                list[i] = new Vector3(delta, list[i].y, list[i].z) + new Vector3(list[i - 1].x, 0, 0);
            }
            else if (list[i].x - list[i - 1].x < 0)
            {
                foreach(Vector3 v in list)
                {
                    int localDelta = spaceBetweenEnemies * (Mathf.Abs(zCoordTranslations[v.z] - zCoordTranslations[list[i].z]) + 1);
                    if (list[i].x - v.x < localDelta)
                    {
                        list[i] = new Vector3(delta, list[i].y, list[i].z) + new Vector3(list[i - 1].x, 0, 0);
                        break;
                    }
                }
            }
        }
    }
    private void printVector3List(List<Vector3> v)
    {
        string s = "";
        foreach (Vector3 vec in v)
        {
            s += "("+vec.x+","+vec.z+"), ";
        }
        //Debug.Log(s);
    }

}
