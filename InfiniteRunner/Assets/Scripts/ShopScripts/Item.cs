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
    public ColorDataBase.enemyColorOptions enemyColor;
    public ColorDataBase.heroColorOptions heroColor;
    //Change to class later
    public string achievement;
    public ButtonState purchaseState;
    public Type type;

    public enum ButtonState
    {
        purchased,
        equipped,
        unlocked,
        locked
    }
    public enum Type
    {
        heroColor,
        enemyColor,
        swordColor,
        swordModel
    }
 
}

