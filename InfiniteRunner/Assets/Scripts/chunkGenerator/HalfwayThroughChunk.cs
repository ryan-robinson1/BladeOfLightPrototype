﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfwayThroughChunk : MonoBehaviour
{

    private GameObject chunkGenerator;
    private void Start()
    {
        chunkGenerator = GameObject.FindGameObjectWithTag("ChunkGenerator");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            chunkGenerator.GetComponent<ChunkGenerator>().generateChunk(transform.position + new Vector3(180, 0, 0), Quaternion.Euler(0, 90, 0));
        }
    }
}
