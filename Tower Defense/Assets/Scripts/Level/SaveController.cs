using System;
using System.IO;
using UnityEngine;

public class SaveController<T>
{
    private static string GetFilename(string filename)
    {
        return $"{filename}.dat";
    }
    private static string GetPath(string filename)
    {
        return $"{Application.persistentDataPath}/{filename}";
    }
    public static T TryLoad(string loadFilename)
    {
        string filename = GetFilename(loadFilename);
        string path = GetPath(filename);
        if (loadFilename == null || File.Exists(path) == false)
            return default;
        string dataString = File.ReadAllText(path);
        SaveData<T> wrapper = JsonUtility.FromJson<SaveData<T>>(dataString);
        return wrapper.saveData;
    }
    public static void Save(string saveFilename, T data)
    {
        string filename = GetFilename(saveFilename);
        string path = GetPath(filename);
        if (saveFilename == null || data == null)
            return;
        var wrapper = new SaveData<T> { saveData = data };
        var dataString = JsonUtility.ToJson(wrapper);
        File.WriteAllText(path, dataString);
    }
}
[Serializable]
public class SaveData<T>
{
    public T saveData;
}