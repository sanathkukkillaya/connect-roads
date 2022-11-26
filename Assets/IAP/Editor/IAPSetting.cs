using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IAPSetting : ScriptableObject
{
    private const string OBJECT_PATH = "Assets/IAP/Editor/IAPSetting.asset";
    private static IAPSetting instance;

    private bool isEnabled;

    public bool IsIAPEnabled
    {
        get
        {
            return Instance.isEnabled;
        }
        set
        {
            Instance.isEnabled = value;
        }
    }

    public static IAPSetting Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (IAPSetting)AssetDatabase.LoadAssetAtPath(OBJECT_PATH, typeof(IAPSetting));
                
                if(instance == null)
                {
                    instance = ScriptableObject.CreateInstance<IAPSetting>();
                    AssetDatabase.CreateAsset(instance, OBJECT_PATH);
                }
                
            }


            return instance;
        }
        set
        {
            instance = value;
        }
    }

}
