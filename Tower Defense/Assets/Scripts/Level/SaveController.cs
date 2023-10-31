using System;
using System.IO;
using UnityEngine;

public class SaveController<T>
{
    private static string Path(string filename)
    {
        return $"{Application.persistentDataPath}/{filename}";
    }
    public static void TryLoad(string filename, ref T data)
    {
        var path = Path(filename);
        if (File.Exists(path))
        {
            var dataString = File.ReadAllText(path);
            var wrapper = JsonUtility.FromJson<Save<T>>(dataString);
            data = wrapper.saveData;
        }
    }
    public static void Save(string filename, T data)
    {
        var wrapper = new Save<T> { saveData = data };
        var dataString = JsonUtility.ToJson(wrapper);
        File.WriteAllText(Path(filename), dataString);
    }
}
[Serializable]
public class Save<T>
{
    public const string filename = nameof(Save<T>) + ".dat";
    public T saveData;
}