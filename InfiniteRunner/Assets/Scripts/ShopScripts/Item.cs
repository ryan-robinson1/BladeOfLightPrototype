using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public GameObject prefab;
    public string name;
    public int price;

    [HideInInspector]
    public bool purchased;
    public bool equipped;
}

