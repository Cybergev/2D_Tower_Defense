using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Level : ScriptableObject
{
    [SerializeField] private int m_SceneNumber;
    [SerializeField] private string m_SceneName;
    [SerializeField] private string m_LevelName;
    [SerializeField] private Sprite m_PreviewImage;
    public int SceneNumber => m_SceneNumber;
    public string SceneName => m_SceneName;
    public string LevelName => m_LevelName;
    public Sprite PreviewImage => m_PreviewImage;
}