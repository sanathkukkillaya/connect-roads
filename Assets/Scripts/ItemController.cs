using SDK;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    private GameObject instantiatedObject;

    public int itemIndex;

    IAP.IAPProduct product;
    bool isProductPurchased = false;
    string purchaseStatusString;    

    // Start is called before the first frame update
    void Start()
    {
        product = ShopController.instance.GetProduct(itemIndex);
        
        if (product != null)
        {
            RenderShopItem(product.car);
            purchaseStatusString = product.m_ProductId + " Status";

            GetComponent<Button>().onClick.AddListener(BuyProduct);
            //SetStatus();
        }        
    }

    private void BuyProduct()
    {
        if (!isProductPurchased)
        {
            ShopController.instance.Buy(itemIndex);
        }
        else
        {
            PlayerPrefs.SetString("PlayerCar", product.m_ProductId);
        }

    }

    private void RenderShopItem(GameObject car)
    {
        //Debug.Log("Rendering Shop item: " + itemIndex);
        RenderTexture rt = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
        var rawImg = GetComponentInChildren<RawImage>();
        rawImg.texture = rt;

        GameObject carShopItem = GameObject.Find("CarShopItem");
        Vector3 position = carShopItem.transform.position;
        position.x += itemIndex * 20; // This is done to prevent overlapping with other items
        instantiatedObject = Instantiate(carShopItem, position, Quaternion.identity);
        var camera = instantiatedObject.GetComponentInChildren<Camera>();
        camera.targetTexture = rt;

        var carObject = Instantiate(car, instantiatedObject.transform.Find("Car").gameObject.transform);
        carObject.layer = 10; // UI Shop
        //Debug.Log("Shop Item rendered");
    }

    public void SetStatusText(string purchaseStatus)
    {
        TextMeshProUGUI statusText = GetComponentInChildren<TextMeshProUGUI>();
        statusText.SetText(purchaseStatus);
    }

    public void SetStatus()
    {
        TextMeshProUGUI statusText = GameObject.Find("StatusText").GetComponent<TextMeshProUGUI>();
        statusText.SetText("Owned");
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey(purchaseStatusString))
        {
            if (PlayerPrefs.GetString(purchaseStatusString) == "Owned")
            {
                isProductPurchased = true;
                if (PlayerPrefs.HasKey("PlayerCar") && PlayerPrefs.GetString("PlayerCar") == product.m_ProductId)
                    SetStatusText("Selected");
                else
                    SetStatusText("Owned");
            }
        }
        else
        {
            isProductPurchased = false;
            SetStatusText(product.cost.ToString());
        }
    }
}
