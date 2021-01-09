using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private int money;
    [SerializeField]
    public Item[] itemArray;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI moneyText2;

    void Start()
    {
        money = PlayerPrefs.GetInt("money", 0);
        moneyText.text = money + "";
        moneyText2.text = money + "";
    }

    
    void Update()
    {
        
    }
    public void purchaseItem(string name)
    {
        Item i = findItemByName(name);
        if (withdrawlMoney(i.price))
        {
            i.purchased = true;
        }
    }
    public void equipItem(string name)
    {
        Item i = findItemByName(name);
        if (i.purchased)
        {
            i.equipped = true;
        }
    }
    public void depositMoney(int deposit)
    {
        money += deposit;
        PlayerPrefs.SetInt("money", money);
        moneyText.text = money+"";
        moneyText2.text = money + "";

    }
    public bool withdrawlMoney(int withdrawl)
    {

        if (money - withdrawl >= 0)
        {
            money -= withdrawl;
            PlayerPrefs.SetInt("money", money);
            moneyText.text = money + "";
            moneyText2.text = money + "";
            return true;
        }
        else
        {
            return false;
        }
       
       
    }
    public Item findItemByName(string name)
    {
        foreach(Item i in itemArray)
        {
            if (i.name == name)
            {
                return i;
            }
        }
        return null;
    }
}
