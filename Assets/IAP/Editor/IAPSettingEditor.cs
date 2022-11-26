using UnityEditor;
using UnityEngine;


[InitializeOnLoad]
[CustomEditor(typeof(IAPSetting))]
public class IAPSettingEditor : Editor
{
    private bool move;

    private const string FROM_PATH = "Assets/IAP/Plugins";
    private const string TO_PATH = "Assets/Plugins";
    private const string UDP = "UDP";
    private const string UNITY_CHANNEL = "UnityChannel";
    private const string UNITY_PURCHASING = "UnityPurchasing";


    [MenuItem("Assets/JTool/IAP")]
    static void Init()
    {

        if (IAPSetting.Instance != null)
            Selection.activeObject = IAPSetting.Instance;
    }


    public override void OnInspectorGUI()
    {
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("IAP", EditorStyles.boldLabel);

        IAPSetting.Instance.IsIAPEnabled= EditorGUILayout.Toggle(new GUIContent("Enabled"), IAPSetting.Instance.IsIAPEnabled);

        if (IAPSetting.Instance.IsIAPEnabled && !move)
        {
            MoveToPlugIns();
            Debug.Log("MOve");
            move = true;
        }
        else if (!IAPSetting.Instance.IsIAPEnabled && move)
        {
			RemoveFromPLugIns();

			Debug.Log("Remove");
            move = false;
        }
    }


    private void MoveToPlugIns()
    {
        if (!AssetDatabase.IsValidFolder(TO_PATH))
        {
            AssetDatabase.CreateFolder("Assets", "Plugins");
        }

        AssetDatabase.MoveAsset(FROM_PATH + "/" + UDP , TO_PATH + "/" + UDP);
        AssetDatabase.MoveAsset(FROM_PATH + "/" + UNITY_CHANNEL, TO_PATH + "/" + UNITY_CHANNEL);
        AssetDatabase.MoveAsset(FROM_PATH + "/" + UNITY_PURCHASING, TO_PATH + "/" + UNITY_PURCHASING);
    }

    private void RemoveFromPLugIns()
    {
		if (!AssetDatabase.IsValidFolder(FROM_PATH))
		{
			AssetDatabase.CreateFolder("Assets/IAP", "Plugins");
		}

		AssetDatabase.MoveAsset(TO_PATH + "/" + UDP, FROM_PATH + "/" + UDP);
		AssetDatabase.MoveAsset(TO_PATH + "/" + UNITY_CHANNEL, FROM_PATH + "/" + UNITY_CHANNEL);
		AssetDatabase.MoveAsset(TO_PATH + "/" + UNITY_PURCHASING, FROM_PATH + "/" + UNITY_PURCHASING);
	}
}
