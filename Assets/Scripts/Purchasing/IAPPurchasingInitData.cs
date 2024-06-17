using System;
using System.Collections.Generic;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Purchasing;
using Product = UnityEngine.Purchasing.Product;


namespace SlotGameVova.Assets.Scripts.Purchasing
{
    public class IAPPurchasingInitData : MonoBehaviour, IStoreListener
    {
        private event Action OnPurchaseCompleted;

        private readonly string COINS_10000 = "slotgame10000coins";
        private readonly string COINS_50000 = "slotgame50000coins";
        private readonly string COINS_100000 = "slotgame100000coins";



        public static IAPPurchasingInitData Instance;

        private IStoreController _storeController;
        private IExtensionProvider _extensionProvider;
        private static Product _staticTestProduct = null;


        IGooglePlayStoreExtensions m_GooglePlayStoreExtensions;


        private Boolean return_complete = true;


        private void Awake()
        {
            if (!Instance)
                Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }

        async void Start()
        {
            try
            {
                await UnityServices.InitializeAsync();
            }
            catch (Exception e)
            {
                Debug.Log("Consent: :" + e.ToString());  // Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately.
            }

            EventActionInt += MethodEventInt;

            InitPurchases();
        }



        private void InitPurchases()
        {
            if (IsInitialized())
            {
                return;
            }

            var storeBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            storeBuilder.Configure<IGooglePlayConfiguration>().SetDeferredPurchaseListener(OnDeferredPurchase);
            storeBuilder.Configure<IGooglePlayConfiguration>().SetQueryProductDetailsFailedListener(EventActionInt);

            storeBuilder.AddProduct(COINS_10000, ProductType.Consumable);
            storeBuilder.AddProduct(COINS_50000, ProductType.Consumable);
            storeBuilder.AddProduct(COINS_100000, ProductType.Consumable);


            UnityPurchasing.Initialize(this, storeBuilder);
        }

        private event Action<int> EventActionInt;

        void MethodEventInt(int @int)
        {
            Debug.Log("Listener = " + @int.ToString());
        }
        private bool IsInitialized()
        {
            return _storeController != null && _extensionProvider != null;
        }

        void OnDeferredPurchase(UnityEngine.Purchasing.Product product)
        {
            Debug.Log($"Purchase of {product.definition.id} is deferred");
        }


        public void CompletePurchase()
        {
            if (_staticTestProduct == null)
                Debug.Log("Cannot complete purchase, product not initialized.");
            else
            {
                _storeController.ConfirmPendingPurchase(_staticTestProduct);
                Debug.Log("Completed purchase with " + _staticTestProduct.transactionID.ToString());
            }

        }

        public void ToggleComplete()
        {
            return_complete = !return_complete;
            Debug.Log("Complete = " + return_complete.ToString());

        }
        public void OnRestorePurchases()
        {
            _extensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(result =>
            {

                if (result)
                {
                    Debug.Log("Restore purchases succeeded.");
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                    { "restore_success", true },
                    };
                    //AnalyticsService.Instance.CustomData("myRestore", parameters);
                }
                else
                {
                    Debug.Log("Restore purchases failed.");
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                    { "restore_success", false },
                    };
                    // AnalyticsService.Instance.CustomData("myRestore", parameters);
                }

                // AnalyticsService.Instance.Flush();
            });

        }

        public void ByIdPurchase(string productId, Action OnActionDone)
        {
            if (IsInitialized())
            {
                UnityEngine.Purchasing.Product product = _storeController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product:" + product.definition.id.ToString()));
                    _storeController.InitiatePurchase(product);
                    OnActionDone?.Invoke();
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("OnInitialized: PASS");

            _storeController = controller;
            _extensionProvider = extensions;
            m_GooglePlayStoreExtensions = extensions.GetExtension<IGooglePlayStoreExtensions>();

        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            _staticTestProduct = args.purchasedProduct;

            //var validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
            //var result = validator.Validate(args.purchasedProduct.receipt);
            //MyDebug("Validate = " + result.ToString());

            if (m_GooglePlayStoreExtensions.IsPurchasedProductDeferred(_staticTestProduct))
            {
                //The purchase is Deferred.
                //Therefore, we do not unlock the content or complete the transaction.
                //ProcessPurchase will be called again once the purchase is Purchased.
                return PurchaseProcessingResult.Pending;
            }
            if (return_complete)
            {
                Debug.Log(string.Format("ProcessPurchase: Complete. Product:" + args.purchasedProduct.definition.id + " - " + _staticTestProduct.transactionID.ToString()));
                return PurchaseProcessingResult.Complete;
            }
            else
            {
                Debug.Log(string.Format("ProcessPurchase: Pending. Product:" + args.purchasedProduct.definition.id + " - " + _staticTestProduct.transactionID.ToString()));
                return PurchaseProcessingResult.Pending;
            }

        }

        public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        }


        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.Log("OnInitializeFailed " + message);
        }

    }
}
