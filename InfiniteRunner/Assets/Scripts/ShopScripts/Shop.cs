using System;
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
    public DissolveEnemyScript shopEnemyScript;
    public DamageManager damageManagerScript;
    public DamageManager shopDamageManagerScript;
    public SwordColorScript swordColorScript;
    public SwordColorScript shopHeroSwordColorScript;
    public SwordColorScript shopSwordColorScript;
    public BladeCoreSwitcher bladeCoreSwitcher;
    public StartMenuScript startMenu;
    public bool deletePlayerPrefs = false;
  
    TextMeshProUGUI achievmentText;
    Button purchaseButton;
    TextMeshProUGUI itemName;
    private bool firstTimePlaying;

    private void Awake()
    {
        if (deletePlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
        }
        Enum.TryParse(PlayerPrefs.GetString("EnemyColor", "red"), out ColorDataBase.enemyColorOptions enemyC);
        ColorDataBase.setEnemyColor(enemyC);
        Debug.Log(enemyC.ToString());
        Enum.TryParse(PlayerPrefs.GetString("HeroColor", "blue"), out ColorDataBase.heroColorOptions heroC);
        ColorDataBase.setHeroColor(heroC);
        Debug.Log(heroC.ToString());
        Enum.TryParse(PlayerPrefs.GetString("SwordColor", "blue"), out ColorDataBase.heroColorOptions swordC);
        ColorDataBase.setSwordColor(swordC);
        Debug.Log(swordC.ToString());

    }
    void Start()
    {
        
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
    public int selectedPanel()
    {
        return scrollSnap.CurrentPanel;
    }
    public void setUpItemPlayerPrefs()
    {
        bool noMoreEquipEnemyFlag = false;
        bool noMoreEquipSwordCoreFlag = false;
        bool noMoreEquipSword = false;

        foreach (Transform canvas in itemCanvas)
        {
            foreach(Transform t in canvas)
            {
                Item tempItem = t.GetComponent<Item>();
               
                tempItem.purchaseState = (Item.ButtonState)Enum.Parse(typeof(Item.ButtonState), PlayerPrefs.GetString(tempItem.name, tempItem.purchaseState.ToString()), true);
    
                if (tempItem.defaultItem && firstTimePlaying)
                {
                    tempItem.price = 0;
                    
                    tempItem.purchaseState = Item.ButtonState.equipped;
                    PlayerPrefs.SetString(tempItem.name, tempItem.purchaseState.ToString());

                }
            }
        }
    }
  
    
    public int updateScrollSnap(DanielLochner.Assets.SimpleScrollSnap.SimpleScrollSnap _scrollSnap)
    {
        int equippedIndex = -1;
        itemList.Clear();
        scrollSnap = _scrollSnap;
        GameObject content = _scrollSnap.transform.GetChild(0).GetChild(0).gameObject;
       
        foreach (Transform child in content.transform)
        {
            Item i = child.GetComponent<Item>();
            itemList.Add(i);
            if(i.purchaseState == Item.ButtonState.equipped)
            {
                if(i.type == Item.Type.enemyColor && i.enemyColor.ToString() == ColorDataBase.enemyColorName)
                {
                    equippedIndex = itemList.Count - 1;
                }
                else if(i.type == Item.Type.enemyColor && i.enemyColor.ToString() != ColorDataBase.enemyColorName)
                {
                    i.purchaseState = Item.ButtonState.purchased;
                }

                else if (i.type == Item.Type.swordColor && i.heroColor.ToString() == ColorDataBase.swordColorName)
                {
                    equippedIndex = itemList.Count - 1;
                    Debug.Log("here");
                }
                else if (i.type == Item.Type.swordColor && i.heroColor.ToString() != ColorDataBase.swordColorName)
                {
                    i.purchaseState = Item.ButtonState.purchased;
                }
                else
                {
                    equippedIndex = itemList.Count - 1;
                }
            }
            else if(i.type == Item.Type.swordColor && i.heroColor.ToString() == ColorDataBase.swordColorName)
            {
                equippedIndex = itemList.Count - 1;
             
            }
            else if (i.type == Item.Type.enemyColor && i.enemyColor.ToString() == ColorDataBase.enemyColorName)
            {
                equippedIndex = itemList.Count - 1;
         
            }
        }
        Debug.Log("EQUIP INDEX::" + equippedIndex);
        return equippedIndex;
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
        Debug.Log(itemList.Count);
        achievmentText.text = itemList[index].achievement;
        itemName.text = itemList[index].displayName;

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
        else if (itemList[index].purchaseState == Item.ButtonState.noPurchase)
        {
            buttonText.text = "";
        }
    }
    public void setItemValues(int _index)
    {
        int index = _index;
        
        achievmentText.text = itemList[index].achievement;
        itemName.text = itemList[index].displayName;

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
        else if (itemList[index].purchaseState == Item.ButtonState.noPurchase)
        {
            buttonText.text = "-";
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
            if(i.type == Item.Type.heroColor)
            {
                bladeCoreSwitcher.purchasesToMakeIndex.Add(scrollSnap.CurrentPanel);
                Debug.Log("Added");
               
            }
        
        }
    }
    public void purchaseItemsNoCost(ref List<int> indexes)
    {
        for(int j = 0;j < indexes.Count; j++)
        {
            Item i = itemList[indexes[j]];
            i.purchaseState = Item.ButtonState.purchased;
            Debug.Log(i.name);
            PlayerPrefs.SetString(i.name, i.purchaseState.ToString());
            setItemValues();
        }
        indexes.Clear();
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
            if(i.type == Item.Type.enemyColor)
            {
                ColorDataBase.setEnemyColor(i.enemyColor);
           
               shopEnemyScript.SetMatColors();
    
            }
            else if (i.type == Item.Type.heroColor)
            {
                ColorDataBase.setHeroColor(i.heroColor);
       
                swordColorScript.setSwordColor();
                damageManagerScript.SetHeroColor();
                shopDamageManagerScript.SetHeroColor();
                startMenu.updateUI();
                bladeCoreSwitcher.heroSyncEquipIndex = scrollSnap.CurrentPanel;
                Debug.Log(bladeCoreSwitcher.heroSyncEquipIndex);
         
            }
            else if (i.type == Item.Type.swordColor)
            {
                ColorDataBase.setSwordColor(i.heroColor);
         
                swordColorScript.setSwordColor();
                shopHeroSwordColorScript.setSwordColor();
                shopSwordColorScript.setSwordColor();

            }
            else if (i.type == Item.Type.swordModel)
            {
                //Switch The Model
            }
        }
        
    }
    //Only used by sword cores
    public void equipItem(int  j)
    {
        Item i = itemList[j];
      
            foreach (var otherItem in itemList)
            {
                if (otherItem.purchaseState == Item.ButtonState.equipped)
                {
                    otherItem.purchaseState = Item.ButtonState.purchased;
                }
            }
            i.purchaseState = Item.ButtonState.equipped;
            PlayerPrefs.SetString(i.name, i.purchaseState.ToString());
            PlayerPrefs.SetString("SwordColor", i.heroColor.ToString());
            Debug.Log(i.name + " " + i.type);
            setItemValues();
       
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
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("HeroColor", ColorDataBase.heroColorName);
        PlayerPrefs.SetString("EnemyColor", ColorDataBase.enemyColorName);
        PlayerPrefs.SetString("SwordColor", ColorDataBase.swordColorName);

/*
        foreach (Transform canvas in itemCanvas)
        {
            foreach (Transform t in canvas)
            {
                Item tempItem = t.GetComponent<Item>();

               PlayerPrefs.SetString(tempItem.name,tempItem.p)
             
            }
        }*/
    }
   

}
