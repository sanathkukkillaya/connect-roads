using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SDK;
public class IAPHandler : MonoBehaviour
{

    [SerializeField] private IAP.IAPProduct[] m_AndroidProducts;
    [SerializeField] private IAP.IAPProduct[] m_IosProducts;
    [SerializeField] private Text m_Result;

    public void Initialize()
    {
        IAP.Init(m_AndroidProducts, m_IosProducts);
        m_Result.text = "Initialized";
    }


    public void Buy()
    {

#if UNITY_ANDROID
        int index = Random.Range(0, m_AndroidProducts.Length);

        IAP.BuyProduct(m_AndroidProducts[index].m_ProductId);

        m_Result.text = "Purchased " + m_AndroidProducts[index].m_ProductId;
    
#elif UNITY_IOS
        int index1 = Random.Range(0, m_IosProducts.Length);
        IAP.BuyProduct(m_IosProducts[index1].m_ProductId);
        m_Result.text = "Purchased " + m_IosProducts[index1].m_ProductId;
#endif

    }
}
