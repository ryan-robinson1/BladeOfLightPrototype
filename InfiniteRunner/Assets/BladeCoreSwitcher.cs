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

    bool initialSetupFlag = false;

    public void switchSelects()
    {
        if (!initialSetupFlag)
        {
            shop.updateScrollSnap(scrollSnapSwordCore);
            shop.setItemValues(2);
            coreSelect.SetActive(false);
            swordSelect.SetActive(true);
            switchFlag = true;
            buttonText.text = "Blades";
            initialSetupFlag = true;
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
                shop.setItemValues();
                coreSelect.SetActive(false);
                swordSelect.SetActive(true);
                switchFlag = true;
                buttonText.text = "Blades";

            }
        }
        
    }
}
