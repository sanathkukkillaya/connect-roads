using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayerData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/test.fun";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData();

        formatter.Serialize(stream, playerData);
        stream.Close();

        Debug.Log("Succesfully saved file to " + path);
    }

    public static PlayerData LoadPlayerData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/test.fun";

        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData playerData = (PlayerData)formatter.Deserialize(stream);
            stream.Close();

            Debug.Log("Succesfully loaded file from " + path);
            return playerData;
        }
        else
        {
            Debug.LogError("No save file found at " + path);
            return null;
        }
    }
}
