﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BladeCoreSwitcher : MonoBehaviour
{
 
    public GameObject coreSelect;
    public GameObject swordSelect;
    public Text buttonText;
    public Shop shop;
    public DanielLochner.Assets.SimpleScrollSnap.SimpleScrollSnap scrollSnapSwordCore;
    public DanielLochner.Assets.SimpleScrollSnap.SimpleScrollSnap scrollSnapSword;
    bool switchFlag = false;
    public bool setEquippedIndex = false;
    bool initialSetupFlag = false;
    [HideInInspector]
    public List<int> purchasesToMakeIndex = new List<int>();
    [HideInInspector]
    public int heroSyncEquipIndex = -1;
    public void switchSelects()
    {
        if (!initialSetupFlag)
        {
            swordSelect.transform.GetChild(0).GetChild(0).transform.gameObject.SetActive(true);
            shop.updateScrollSnap(scrollSnapSwordCore);
            UpdatePreviousPurchases();
            int equippedIndex = shop.updateScrollSnap(scrollSnapSwordCore);
            shop.setItemValues(2);
            coreSelect.SetActive(false);
            switchFlag = true;
            buttonText.text = "Blades";
            initialSetupFlag = true;
            if (setEquippedIndex && equippedIndex > -1)
            {
                scrollSnapSwordCore.GoToPanel(equippedIndex);
                setEquippedIndex = false;
            }
            else if(equippedIndex < 0)
            {
                Debug.LogError("No item equipped");
            }
           
        }
        else
        {
            if (switchFlag)
            {
                
                shop.updateScrollSnap(scrollSnapSword);
                shop.setItemValues();
                coreSelect.SetActive(true);
                swordSelect.SetActive(false);
                switchFlag = false;
                buttonText.text = "Cores";

            }
            else
            {
              
                shop.updateScrollSnap(scrollSnapSwordCore);
                UpdatePreviousPurchases();
                int equippedIndex = shop.updateScrollSnap(scrollSnapSwordCore);
                shop.setItemValues();
                coreSelect.SetActive(false);
                swordSelect.SetActive(true);
                switchFlag = true;
                buttonText.text = "Blades";
                if (setEquippedIndex && equippedIndex > -1)
                {
                    scrollSnapSwordCore.GoToPanel(equippedIndex);
                    setEquippedIndex = false;
                }
                else if (equippedIndex < 0)
                {
                    Debug.LogError("No item equipped");
                }

            }
        }
        
    }
    public void resetScrollSnap()
    {
        if (switchFlag)
        {
            shop.updateScrollSnap(scrollSnapSword);
            shop.setItemValues();
            coreSelect.SetActive(true);
            swordSelect.SetActive(false);
            switchFlag = false;
            buttonText.text = "Cores";

        }
    }
    private void UpdatePreviousPurchases()
    {
        shop.purchaseItemsNoCost(ref purchasesToMakeIndex);
        Debug.Log(heroSyncEquipIndex);
        if(heroSyncEquipIndex > -1)
        {
            Debug.Log("equipping");
            shop.equipItem(heroSyncEquipIndex);
            heroSyncEquipIndex = -1;
        }
    }
}
