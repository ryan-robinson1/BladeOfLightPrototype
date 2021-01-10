using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInterfaceNav : MonoBehaviour
{
    public Camera ShopCamera;
    public Camera MainMenuCamera;
    public Canvas PlayerShopCanvas;
    public Canvas EnemyShopCanvas;
    public Canvas SwordShopCanvas;
    public Canvas mainMenuCanvas;
    public GameObject shop;
    bool moveToEnemyShop = false;
    bool moveToPlayerShop = false;
    bool moveToSwordShop = false;
    Vector3 enemyShopCameraPosition = new Vector3(-8.152f, 2.43f, 10.6f);
    Vector3 playerShopCameraPosition = new Vector3(-15f, 2.43f, 10.6f);
    Vector3 swordShopCameraPosition = new Vector3(-21.848f, 2.43f, 10.6f);
    public void goToShop()
    {
        ShopCamera.enabled = true;
        MainMenuCamera.enabled = false;
        PlayerShopCanvas.enabled = true;
        mainMenuCanvas.enabled = false;
        shop.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void goToMainMenu()
    {
        ShopCamera.enabled = false;
        ShopCamera.transform.position = playerShopCameraPosition;
        MainMenuCamera.enabled = true;
        PlayerShopCanvas.enabled = false;
        EnemyShopCanvas.enabled = false;
        SwordShopCanvas.enabled = false;
        mainMenuCanvas.enabled = true;
        shop.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void goToEnemyShop()
    {
        moveToEnemyShop = true;
       
        PlayerShopCanvas.enabled = false;
    }
    public void goToPlayerShop()
    {
        EnemyShopCanvas.enabled = false;
        SwordShopCanvas.enabled = false;
       
        moveToPlayerShop = true;
    }
    public void goToSwordShop()
    {
        PlayerShopCanvas.enabled = false;
        
        moveToSwordShop = true;

    }
    void Update()
    {
        if (moveToEnemyShop)
        {
            ShopCamera.transform.position = Vector3.MoveTowards(ShopCamera.transform.position, enemyShopCameraPosition, Time.deltaTime*10);
            if(ShopCamera.transform.position == enemyShopCameraPosition)
            {
                moveToEnemyShop = false;
                EnemyShopCanvas.enabled = true;
            }
        }
        else if (moveToPlayerShop)
        {
            ShopCamera.transform.position = Vector3.MoveTowards(ShopCamera.transform.position, playerShopCameraPosition, Time.deltaTime * 10);
            if (ShopCamera.transform.position == playerShopCameraPosition)
            {
                moveToPlayerShop = false;
                PlayerShopCanvas.enabled = true;
            }
        }
        else if (moveToSwordShop)
        {
            ShopCamera.transform.position = Vector3.MoveTowards(ShopCamera.transform.position, swordShopCameraPosition, Time.deltaTime * 10);
            if (ShopCamera.transform.position == swordShopCameraPosition)
            {
                moveToSwordShop = false;
                SwordShopCanvas.enabled = true;
            }
        }
    }
}
