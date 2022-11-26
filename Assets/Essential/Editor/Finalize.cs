using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Finalize : Editor
{
    //Firebase
    private const string FIREBASE_FROM_PATH = "Assets/JTool/Firebase/Plugins";
    private const string FIREBASE_TO_PATH = "Assets/Plugins";
    private const string FIREBASE_IOS_FIREBASE = "iOS/Firebase";
    private const string FIREBASE__MANIFEST = "Android/Firebase_/AndroidManifest.xml";
    private const string FIREBASE__PROJECT = "Android/Firebase_/project.properties";
    private const string FIREBASE_MANIFEST = "Android/Firebase/AndroidManifest.xml";
    private const string FIREBASE_PROJECT = "Android/Firebase/project.properties";
    private const string FIREBASE_MESSAGING = "Android/libmessaging_unity_player_activity.jar";

    //Admob
    private const string ADMOB_FROM_PATH = "Assets/JTool/GoogleMobileAds/Plugins";
    private const string ADMOB_TO_PATH = "Assets/Plugins";
    private const string ADMOB_IOS = "iOS";
    private const string ADMOB_ANDROID = "Android/GoogleMobileAdsPlugin";

    //IAP
    private const string IAP_FROM_PATH = "Assets/JTool/IAP/Plugins";
    private const string IAP_TO_PATH = "Assets/Plugins";
    private const string IAP_UDP = "UDP";
    private const string IAP_UNITY_CHANNEL = "UnityChannel";
    private const string IAP_UNITY_PURCHASING = "UnityPurchasing";

    //NativeShare

    private const string NATIVESHARE_MAIN_MANIFEST_PATH = "Assets/Plugins/Android/AndroidManifest.xml";
    private const string NATIVESHARE_MANIFEST_PATH = "Assets/JTool/NativeShare/Plugins/Android/AndroidManifest.xml";
    private const string NATIVESHARE_PROVIDER = "<provider android:name=\"com.yasirkula.unity.UnitySSContentProvider\" android:authorities=\"com.juego.testapp\" android:exported=\"false\" android:grantUriPermissions=\"true\"/>";



    [MenuItem("Assets/JTool/Finalize")]
    static void Init()
    {
        Debug.Log("RUNS");



        if (AssetDatabase.IsValidFolder("Assets/JTool/IAP"))
        {
          
            if (!AssetDatabase.IsValidFolder(IAP_TO_PATH))
            {
                AssetDatabase.CreateFolder("Assets", "Plugins");
            }

            AssetDatabase.MoveAsset(IAP_FROM_PATH + "/" + IAP_UDP, IAP_TO_PATH + "/" + IAP_UDP);
            AssetDatabase.MoveAsset(IAP_FROM_PATH + "/" + IAP_UNITY_CHANNEL, IAP_TO_PATH + "/" + IAP_UNITY_CHANNEL);
            AssetDatabase.MoveAsset(IAP_FROM_PATH + "/" + IAP_UNITY_PURCHASING, IAP_TO_PATH + "/" + IAP_UNITY_PURCHASING);
        }

        if (AssetDatabase.IsValidFolder("Assets/JTool/GoogleMobileAds"))
        {
           

            if (!AssetDatabase.IsValidFolder(ADMOB_TO_PATH))
            {
                AssetDatabase.CreateFolder("Assets", "Plugins");
            }

            if (!AssetDatabase.IsValidFolder("Assets/Plugins/Android"))
            {
                AssetDatabase.CreateFolder("Assets/Plugins", "Android");
            }

          

            AssetDatabase.MoveAsset(ADMOB_FROM_PATH + "/" + ADMOB_IOS, ADMOB_TO_PATH + "/" + ADMOB_IOS);

            AssetDatabase.MoveAsset(ADMOB_FROM_PATH + "/" + ADMOB_ANDROID, ADMOB_TO_PATH + "/" + ADMOB_ANDROID);
        }

        if (AssetDatabase.IsValidFolder("Assets/JTool/NativeShare"))
        {
          


            if (!File.Exists(NATIVESHARE_MAIN_MANIFEST_PATH))
            {
                

                if (!AssetDatabase.IsValidFolder("Assets/Plugins"))
                {
                    AssetDatabase.CreateFolder("Assets", "Plugins");
                }

                if (!AssetDatabase.IsValidFolder("Assets/Plugins/Android"))
                {
                    AssetDatabase.CreateFolder("Assets/Plugins", "Android");
                }

                AssetDatabase.CopyAsset(NATIVESHARE_MANIFEST_PATH, NATIVESHARE_MAIN_MANIFEST_PATH);
            }

            var data = File.ReadAllLines(NATIVESHARE_MAIN_MANIFEST_PATH);

            List<string> manifest = new List<string>(data);

            for (int i = 0; i < manifest.Count; i++)
            {
                manifest[i] = manifest[i].Trim();
            }

            File.WriteAllLines(NATIVESHARE_MAIN_MANIFEST_PATH, manifest);
        }

        if (AssetDatabase.IsValidFolder("Assets/JTool/Firebase"))
        {
    

            if (!AssetDatabase.IsValidFolder(FIREBASE_TO_PATH))
            {
                AssetDatabase.CreateFolder("Assets", "Plugins");
            }

            if (!AssetDatabase.IsValidFolder("Assets/Plugins/Android"))
            {
                AssetDatabase.CreateFolder("Assets/Plugins", "Android");
            }

            if (!AssetDatabase.IsValidFolder("Assets/Plugins/Android/Firebase"))
            {
                AssetDatabase.CreateFolder("Assets/Plugins/Android", "Firebase");
            }

            if (!AssetDatabase.IsValidFolder("Assets/Plugins/iOS"))
            {
                AssetDatabase.CreateFolder("Assets/Plugins", "iOS");

            }

            AssetDatabase.MoveAsset(FIREBASE_FROM_PATH + "/" + FIREBASE_IOS_FIREBASE, FIREBASE_TO_PATH + "/" + FIREBASE_IOS_FIREBASE);
            AssetDatabase.MoveAsset(FIREBASE_FROM_PATH + "/" + FIREBASE__MANIFEST, FIREBASE_TO_PATH + "/" + FIREBASE_MANIFEST);
            AssetDatabase.MoveAsset(FIREBASE_FROM_PATH + "/" + FIREBASE__PROJECT, FIREBASE_TO_PATH + "/" + FIREBASE_PROJECT);
            AssetDatabase.MoveAsset(FIREBASE_FROM_PATH + "/" + FIREBASE_MESSAGING, FIREBASE_TO_PATH + "/" + FIREBASE_MESSAGING);

        }
    }
}