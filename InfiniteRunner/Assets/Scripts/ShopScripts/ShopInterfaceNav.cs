using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopInterfaceNav : MonoBehaviour
{
    public Camera ShopCamera;
    public Camera MainMenuCamera;
    public Canvas PlayerShopCanvas;
    public Canvas EnemyShopCanvas;
    public Canvas SwordShopCanvas;
    public Canvas mainMenuCanvas;
    public Shop shop;
    public DanielLochner.Assets.SimpleScrollSnap.SimpleScrollSnap scrollSnapSword;
    public DanielLochner.Assets.SimpleScrollSnap.SimpleScrollSnap scrollSnapSwordCore;
    public BladeCoreSwitcher bladeCoreSwitcher;
    bool moveToEnemyShop = false;
    bool moveToPlayerShop = false;
    bool moveToSwordShop = false;

    Vector3 enemyShopCameraPosition = new Vector3(-8.152f, 2.43f, 10.6f);
    Vector3 playerShopCameraPosition = new Vector3(-15f, 2.43f, 10.6f);
    Vector3 swordShopCameraPosition = new Vector3(-21.848f, 2.43f, 10.6f);
   
    private DanielLochner.Assets.SimpleScrollSnap.SimpleScrollSnap scrollSnap;
    public void goToShop()
    {
        int equippedIndex;
        ShopCamera.enabled = true;
        MainMenuCamera.enabled = false;
        PlayerShopCanvas.enabled = true;
        mainMenuCanvas.enabled = false;
        shop.transform.GetChild(0).gameObject.SetActive(true);
        scrollSnap = PlayerShopCanvas.transform.GetComponentInChildren<DanielLochner.Assets.SimpleScrollSnap.SimpleScrollSnap>();
      
        
        equippedIndex = shop.updateScrollSnap(scrollSnap);
        shop.updateShopUIReferences(PlayerShopCanvas.transform.Find("AchievementRequirement").GetComponent<TextMeshProUGUI>(), PlayerShopCanvas.transform.Find("PurchaseEnableButton").GetComponent<Button>(),
           
            PlayerShopCanvas.transform.Find("ItemName").GetComponent<TextMeshProUGUI>());
        shop.setItemValues();
        if (equippedIndex > -1)
        {
            scrollSnap.GoToPanel(equippedIndex);
        }
        else
        {
            Debug.LogError("Error: No item equipped");
        }

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
        int equippedIndex;
        moveToEnemyShop = true;
        PlayerShopCanvas.enabled = false;
        scrollSnap = EnemyShopCanvas.transform.GetComponentInChildren<DanielLochner.Assets.SimpleScrollSnap.SimpleScrollSnap>();
        equippedIndex = shop.updateScrollSnap(scrollSnap);
        shop.updateShopUIReferences(EnemyShopCanvas.transform.Find("AchievementRequirement").GetComponent<TextMeshProUGUI>(), EnemyShopCanvas.transform.Find("PurchaseEnableButton").GetComponent<Button>(),
            EnemyShopCanvas.transform.Find("ItemName").GetComponent<TextMeshProUGUI>());
        if(equippedIndex > -1)
        {
            scrollSnap.GoToPanel(equippedIndex);
        }
        else
        {
            Debug.LogError("Error: No item equipped");
        }
    }
    public void goToPlayerShop()
    {
        int equippedIndex;
        EnemyShopCanvas.enabled = false;
        SwordShopCanvas.enabled = false;
        moveToPlayerShop = true;
        scrollSnap = PlayerShopCanvas.transform.GetComponentInChildren<DanielLochner.Assets.SimpleScrollSnap.SimpleScrollSnap>();
        equippedIndex = shop.updateScrollSnap(scrollSnap);
        shop.updateShopUIReferences(PlayerShopCanvas.transform.Find("AchievementRequirement").GetComponent<TextMeshProUGUI>(), PlayerShopCanvas.transform.Find("PurchaseEnableButton").GetComponent<Button>(),
            PlayerShopCanvas.transform.Find("ItemName").GetComponent<TextMeshProUGUI>());
        if (equippedIndex > -1)
        {
            scrollSnap.GoToPanel(equippedIndex);
        }
        else
        {
            Debug.LogError("Error: No item equipped");
        }



    }
    public void goToSwordShop()
    {
     
            PlayerShopCanvas.enabled = false;
       
            int equippedIndex; 
            moveToSwordShop = true;
            bladeCoreSwitcher.resetScrollSnap();
            bladeCoreSwitcher.setEquippedIndex = true;
            equippedIndex = shop.updateScrollSnap(scrollSnapSword);
            shop.setItemValues();
            if (equippedIndex > -1)
            {
                scrollSnapSword.GoToPanel(equippedIndex);
            }
            else
            {
                Debug.LogError("Error: No item equipped");
            }
   
       
        shop.updateShopUIReferences(SwordShopCanvas.transform.Find("AchievementRequirement").GetComponent<TextMeshProUGUI>(), SwordShopCanvas.transform.Find("PurchaseEnableButton").GetComponent<Button>(),
            SwordShopCanvas.transform.Find("ItemName").GetComponent<TextMeshProUGUI>());
        
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
                shop.setItemValues();

            }
        }
        else if (moveToPlayerShop)
        {
            ShopCamera.transform.position = Vector3.MoveTowards(ShopCamera.transform.position, playerShopCameraPosition, Time.deltaTime * 10);
            if (ShopCamera.transform.position == playerShopCameraPosition)
            {
                moveToPlayerShop = false;
                PlayerShopCanvas.enabled = true;
                shop.setItemValues();
            }
        }
        else if (moveToSwordShop)
        {
            ShopCamera.transform.position = Vector3.MoveTowards(ShopCamera.transform.position, swordShopCameraPosition, Time.deltaTime * 10);
            if (ShopCamera.transform.position == swordShopCameraPosition)
            {
                moveToSwordShop = false;
                SwordShopCanvas.enabled = true;
                shop.setItemValues();
            }
        }
    }
}
