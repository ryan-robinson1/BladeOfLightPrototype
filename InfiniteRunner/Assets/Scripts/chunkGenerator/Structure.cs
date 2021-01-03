using System.Collections;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Structure
{ 
    public GameObject prefab;
    public int height;
    public float probabilityOfSpawning;
    public Vector3 spawnPosition;
    [HideInInspector]
    public float extentX;
}
