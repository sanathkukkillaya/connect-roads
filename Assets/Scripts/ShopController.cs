using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDK;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopController : MonoBehaviour
{
    [SerializeField] private IAP.IAPProduct[] m_AndroidProducts;
    [SerializeField] private IAP.IAPProduct[] m_IosProducts;
    [SerializeField] private Text m_Result;

    public static ShopController instance;

    public void Awake()
    {
        IAP.Init(m_AndroidProducts, m_IosProducts);
        m_Result.text = "Initialized";

        if (instance == null)
        {
            instance = gameObject.GetComponent<ShopController>();
        }
    }

    public void Start()
    {

    }

    public void Buy(int index)
    {

#if UNITY_ANDROID
        //int index = Random.Range(0, m_AndroidProducts.Length);

        IAP.BuyProduct(m_AndroidProducts[index].m_ProductId);

        m_Result.text = "Purchased " + m_AndroidProducts[index].m_ProductId;

        PlayerPrefs.SetString("PlayerCar", m_Result.text);
        PlayerPrefs.SetString(m_AndroidProducts[index].m_ProductId + " Status", "Owned");

#elif UNITY_IOS
        int index1 = Random.Range(0, m_IosProducts.Length);
        IAP.BuyProduct(m_IosProducts[index1].m_ProductId);
        m_Result.text = "Purchased " + m_IosProducts[index1].m_ProductId;
#endif

    }

    public IAP.IAPProduct GetProduct(int index)
    {
        if (index < m_AndroidProducts.Length)
            return m_AndroidProducts[index];
        else
            return null;
    }

    public IAP.IAPProduct[] GetAllProducts()
    {
        return m_AndroidProducts;
    }
}
