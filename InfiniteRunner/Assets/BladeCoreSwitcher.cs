using System.Collections;
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
    public void switchSelects()
    {
        if (!initialSetupFlag)
        {
            swordSelect.transform.GetChild(0).GetChild(0).transform.gameObject.SetActive(true);
            int equippedIndex = shop.updateScrollSnap(scrollSnapSwordCore);
            UpdatePreviousPurchases();
            shop.setItemValues(2);
            coreSelect.SetActive(false);
            switchFlag = true;
            buttonText.text = "Blades";
            initialSetupFlag = true;
            if (setEquippedIndex)
            {
                scrollSnapSwordCore.GoToPanel(equippedIndex);
                setEquippedIndex = false;
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
              
                int equippedIndex = shop.updateScrollSnap(scrollSnapSwordCore);
                UpdatePreviousPurchases();
                shop.setItemValues();
                coreSelect.SetActive(false);
                swordSelect.SetActive(true);
                switchFlag = true;
                buttonText.text = "Blades";
                if (setEquippedIndex)
                {
                    scrollSnapSwordCore.GoToPanel(equippedIndex);
                    setEquippedIndex = false;
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
        foreach (int x in purchasesToMakeIndex)
        {
            Debug.Log(x);
        }

        shop.purchaseItemsNoCost(ref purchasesToMakeIndex);
        
    }
}
