using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public GameObject prefab;
    public string name;
    public int price;
    public bool defaultItem;
    public string color;
    //Change to class later
    public string achievement;

    public ButtonState purchaseState;

    public enum ButtonState
    {
        purchased,
        equipped,
        unlocked,
        locked
    }
}

