using UnityEngine;
using System.IO;

public class StorageSystem
{
    static string path = Application.persistentDataPath + "/data.json";

    public static void Save(StorageData data)
    {
        string json = JsonUtility.ToJson(data);
        StreamWriter file = new StreamWriter(path);
        file.Write(json);
        file.Close();
    }

    public static StorageData Load()
    {
        Debug.Log("Load");
        if (!File.Exists(path))
        {
            return null;
        }
        string json = File.ReadAllText(path);
        StorageData data = JsonUtility.FromJson<StorageData>(json);
        return data;
    }
}
