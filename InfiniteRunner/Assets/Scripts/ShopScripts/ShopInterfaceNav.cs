using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInterfaceNav : MonoBehaviour
{
    public Camera ShopCamera;
    public Camera MainMenuCamera;
    public Canvas ShopCanvas;
    public Canvas mainMenuCanvas;
    public GameObject shop;
   
    public void goToShop()
    {
        ShopCamera.enabled = true;
        MainMenuCamera.enabled = false;
        ShopCanvas.enabled = true;
        mainMenuCanvas.enabled = false;
        shop.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void goToMainMenu()
    {
        ShopCamera.enabled = false;
        MainMenuCamera.enabled = true;
        ShopCanvas.enabled = false;
        mainMenuCanvas.enabled = true;
        shop.transform.GetChild(0).gameObject.SetActive(false);
    }
}
