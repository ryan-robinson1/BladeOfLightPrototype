﻿using System;
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
    public List<Transform> itemCanvas;
    public bool deletePlayerPrefs = false;
    TextMeshProUGUI achievmentText;
    Button purchaseButton;
    TextMeshProUGUI itemName;
    private bool firstTimePlaying;

    void Start()
    {
        if (deletePlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
        }
        hasPlayed();
        setUpItemPlayerPrefs();
        money = PlayerPrefs.GetInt("money", 0);
        moneyText.text = money + "";
        moneyText2.text = money + "";
        moneyText3.text = money + "";
        moneyText4.text = money + "";
        

        
    }
    public void hasPlayed()
    {
        int hasPlayed = PlayerPrefs.GetInt("hasPlayed",0);
        if (hasPlayed == 0)
        {
            PlayerPrefs.SetInt("hasPlayed", 1);
            firstTimePlaying = true;
            
        }
        else
        {
            firstTimePlaying = false;
        }
        
    }
    public void setUpItemPlayerPrefs()
    {
        

        foreach(Transform canvas in itemCanvas)
        {
            foreach(Transform t in canvas)
            {
                Item tempItem = t.GetComponent<Item>();
                tempItem.purchaseState = (Item.ButtonState)Enum.Parse(typeof(Item.ButtonState), PlayerPrefs.GetString(tempItem.name, "unlocked"), true);

                if (tempItem.defaultItem && firstTimePlaying)
                {
                    tempItem.price = 0;
                    
                    tempItem.purchaseState = Item.ButtonState.equipped;
                    PlayerPrefs.SetString(tempItem.name, tempItem.purchaseState.ToString());
                    // equipItem(tempItem);
                    Debug.Log("FirstTimePlaying");
                }
            }
        }
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
    public void pressPurchaseEnableButton(Button button)
    {
        Text buttonText = button.GetComponentInChildren<Text>();
        if (buttonText.text == "Equip")
        {
            equipItem();
        }
        else if (buttonText.text.StartsWith("$"))
        {
            purchaseItem();
        }
    }
    public void purchaseItem()
    {
        Item i = itemList[scrollSnap.CurrentPanel];
        if (withdrawlMoney(i.price))
        {
            i.purchaseState = Item.ButtonState.purchased;
            PlayerPrefs.SetString(i.name, i.purchaseState.ToString());
            setItemValues();
        }
    }
    public void equipItem()
    {
        Item i = itemList[scrollSnap.CurrentPanel];
        if (i.purchaseState == Item.ButtonState.purchased)
        {
            foreach (var otherItem in itemList)
            {
                if(otherItem.purchaseState == Item.ButtonState.equipped)
                {
                    otherItem.purchaseState = Item.ButtonState.purchased;
                    PlayerPrefs.SetString(otherItem.name, otherItem.purchaseState.ToString());
                }
            }
            i.purchaseState = Item.ButtonState.equipped;
            PlayerPrefs.SetString(i.name, i.purchaseState.ToString());
            
            setItemValues();
        }
    }
    public void equipItem(Item i)
    {
        if (i.purchaseState == Item.ButtonState.purchased)
        {
            foreach (var otherItem in itemList)
            {
                if (otherItem.purchaseState == Item.ButtonState.equipped)
                {
                    otherItem.purchaseState = Item.ButtonState.purchased;
                }
            }
            i.purchaseState = Item.ButtonState.equipped;
            setItemValues();
        }
    }
    public void unlockItem(Item i)
    {
        i.purchaseState = Item.ButtonState.unlocked;
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
