using System;
using System.IO;
using UnityEngine;

[Serializable]
public class Saver<T>
{
    public T data;
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
            data = JsonUtility.FromJson<T>(dataString);
        }
    }
    public static void Save(string filename, T data)
    {
        var wrapper = new Saver<T> { data = data };
        var dataString = JsonUtility.ToJson(wrapper);
        File.WriteAllText(Path(filename), dataString);
    }
}