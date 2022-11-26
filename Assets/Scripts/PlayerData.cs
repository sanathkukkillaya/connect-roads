using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public string playerCar; // PlayerCar
    public int[] bestScores; // Best+levelindex
    public string[] purchasedProducts; // PurchasedProductID+ Status

    public PlayerData()
    {
        if (PlayerPrefs.HasKey("PlayerCar"))
        {
            playerCar = PlayerPrefs.GetString("PlayerCar");
        }
        else
        {
            playerCar = string.Empty;
        }

        var products = ShopController.instance.GetAllProducts();
        purchasedProducts = new string[products.Length];
        for (int i = 0; i < products.Length; i++)
        {
            string productStatus = products[i].m_ProductId + " Status";
            if (PlayerPrefs.HasKey(productStatus))
            {
                purchasedProducts[i] = PlayerPrefs.GetString(productStatus);
            }
            else
            {
                purchasedProducts[i] = string.Empty;
            }
        }

        var totalLevels = SceneManager.sceneCount; // Levels start from scene 1 because scene 0 is main menu
        bestScores = new int[totalLevels];
        for( int i = 1; i < totalLevels; i++)
        {
            string bestKey = "Best" + i;
            if (PlayerPrefs.HasKey(bestKey))
            {
                bestScores[i] = PlayerPrefs.GetInt(bestKey);
            }
            else
            {
                bestScores[i] = 0;
            }
        }
    }
}
