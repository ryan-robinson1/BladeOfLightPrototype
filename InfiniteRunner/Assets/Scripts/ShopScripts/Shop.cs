using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private int money;
    private List<Item> itemList = new List<Item>();
    private DanielLochner.Assets.SimpleScrollSnap.SimpleScrollSnap scrollSnap; 
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI moneyText2;
    public TextMeshProUGUI moneyText3;
    public TextMeshProUGUI moneyText4;
    TextMeshProUGUI achievmentText;
    Button purchaseButton;
    TextMeshProUGUI itemName;

    void Start()
    {
        money = PlayerPrefs.GetInt("money", 0);
        moneyText.text = money + "";
        moneyText2.text = money + "";
        moneyText3.text = money + "";
        moneyText4.text = money + "";
    }

    
    void Update()
    {
        
    }
    public void updateScrollSnap(DanielLochner.Assets.SimpleScrollSnap.SimpleScrollSnap _scrollSnap)
    {
        itemList.Clear();
        scrollSnap = _scrollSnap;
        GameObject content = _scrollSnap.transform.GetChild(0).GetChild(0).gameObject;
        foreach (Transform child in content.transform)
        {
            itemList.Add(child.GetComponent<Item>());
        }
    }
    public void updateShopUIReferences(TextMeshProUGUI _achievementText, Button _purchaseButton, TextMeshProUGUI _itemName)
    {
        itemName = _itemName;
        purchaseButton = _purchaseButton;
        achievmentText = _achievementText;
    }
    public void setItemValues()
    {
        int index = scrollSnap.CurrentPanel;
        Debug.Log(index);
        achievmentText.text = itemList[index].achievement;
        itemName.text = itemList[index].name;

        Text buttonText = purchaseButton.GetComponentInChildren<Text>();
        if (itemList[index].purchaseState == Item.ButtonState.equipped)
        {
            buttonText.text = "Equipped"; 
        }
        else if (itemList[index].purchaseState == Item.ButtonState.locked)
        {
            buttonText.text = "Locked";
        }
        else if (itemList[index].purchaseState == Item.ButtonState.unlocked)
        {
            buttonText.text = "$" + itemList[index].price;
        }
        else if (itemList[index].purchaseState == Item.ButtonState.purchased)
        {
            buttonText.text = "Equip";
        }
    }
    public void setItemValues(int _index)
    {
        int index = _index;
        
        achievmentText.text = itemList[index].achievement;
        itemName.text = itemList[index].name;

        Text buttonText = purchaseButton.GetComponentInChildren<Text>();
        if (itemList[index].purchaseState == Item.ButtonState.equipped)
        {
            buttonText.text = "Equipped";
        }
        else if (itemList[index].purchaseState == Item.ButtonState.locked)
        {
            buttonText.text = "Locked";
        }
        else if (itemList[index].purchaseState == Item.ButtonState.unlocked)
        {
            buttonText.text = "$" + itemList[index].price;
        }
        else if (itemList[index].purchaseState == Item.ButtonState.purchased)
        {
            buttonText.text = "Equip";
        }
    }
    public void purchaseItem(string name)
    {
        Item i = itemList[scrollSnap.CurrentPanel];
        if (withdrawlMoney(i.price))
        {
            i.purchaseState = Item.ButtonState.purchased;
        }
    }
    public void equipItem()
    {
        Item i = itemList[scrollSnap.CurrentPanel];
        if (i.purchaseState == Item.ButtonState.purchased)
        {
            i.purchaseState = Item.ButtonState.equipped;
        }
    }
    public void depositMoney(int deposit)
    {
        money += deposit;
        PlayerPrefs.SetInt("money", money);
        moneyText.text = money+"";
        moneyText2.text = money + "";
        moneyText3.text = money + "";
        moneyText4.text = money + "";

    }
    public bool withdrawlMoney(int withdrawl)
    {

        if (money - withdrawl >= 0)
        {
            money -= withdrawl;
            PlayerPrefs.SetInt("money", money);
            moneyText.text = money + "";
            moneyText2.text = money + "";
            moneyText3.text = money + "";
            moneyText4.text = money + "";
            return true;
        }
        else
        {
            return false;
        }
       
       
    }
   /* public Item findItemByName(string name)
    {
        foreach(Item i in itemList)
        {
            if (i.name == name)
            {
                return i;
            }
        }
        return null;
    }*/
}
