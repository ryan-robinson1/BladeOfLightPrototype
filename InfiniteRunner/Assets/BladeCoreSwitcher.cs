using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BladeCoreSwitcher : MonoBehaviour
{
 
    public GameObject coreSelect;
    public GameObject swordSelect;
    public Text buttonText;
    bool blade = false;

    public void switchSelects()
    {
        if (blade)
        {
            coreSelect.SetActive(true);
            swordSelect.SetActive(false);
            blade = false;
            buttonText.text = "Cores";
        }
        else
        {
            coreSelect.SetActive(false);
            swordSelect.SetActive(true);
            blade = true;
            buttonText.text = "Blades";
        }
    }
}
