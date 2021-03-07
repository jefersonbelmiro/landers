using UnityEngine;

[System.Serializable]
public class StorageData
{
    public int level;

    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }

}
