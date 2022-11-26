namespace SDK
{
    using UnityEngine;
    using UnityEngine.Purchasing;

    public class IAP : IStoreListener
    {

        //public static bool IsReady { private set; get; }


        private static IStoreController m_StoreController;
        private static IExtensionProvider m_ExtensionProvider;


        public static void Init(IAPProduct[] androidProducts = null, IAPProduct[] iosProduct = null)
        {
            if (m_StoreController != null)
                return;

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
#if UNITY_ANDROID
            AddProducts(androidProducts, builder);
#elif UNITY_IOS
            AddProducts(iosProduct, builder);
#endif

            UnityPurchasing.Initialize(new IAP(), builder);
        }

        private static bool IsInitialized()
        {
            return m_StoreController != null && m_ExtensionProvider != null;
        }


        public static void BuyProduct(string productId)
        {
            if (IsInitialized())
            {
                Product product = m_StoreController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                {
                    m_StoreController.InitiatePurchase(product);
                }
                else
                    Debug.Log("No product");
            }
            else
            {
                Debug.Log("Not Initialized");
            }

        }

#if UNITY_IOS
        public static void RestorePurchases()
        {
           
            if (!IsInitialized())
            {
                Debug.Log("RestorePurchases FAIL. Not initialized.");
                return;
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                Debug.Log("RestorePurchases started ...");
                var apple = m_ExtensionProvider.GetExtension<IAppleExtensions>();
                apple.RestoreTransactions((result) => {
                    Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
                });
            }
            else
            {
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            }
        }
#endif
        public virtual void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            m_StoreController = controller;
            m_ExtensionProvider = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("Initialization Failed");
        }

        public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
        {
            Debug.Log("Purchase Failed");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
        {
            return PurchaseProcessingResult.Complete;
        }

        #region Internal Call
        private static void AddProducts(IAPProduct[] products, ConfigurationBuilder builder)
        {
            if (products != null)
                foreach (var product in products)
                {
                    builder.AddProduct(product.m_ProductId, product.type);
                }
        }
        #endregion


        #region Subclass
        [System.Serializable]
        public class IAPProduct
        {
            public string m_ProductId;
            public ProductType type;
            public float cost;
            public GameObject car;
        }
        #endregion
    }
}