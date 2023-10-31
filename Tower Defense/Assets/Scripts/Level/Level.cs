using UnityEngine;

[CreateAssetMenu]
public class Level : ScriptableObject
{
    [SerializeField] private string m_LevelName;
    [SerializeField] private LevelConditionAsset[] m_LevelCondition;
    [SerializeField] private string m_SceneName;
    [SerializeField] private int m_SceneNumber;
    [SerializeField] private Sprite m_ScenePreviewImage;
    public string LevelName => m_LevelName;
    public LevelConditionAsset[] LevelCondition => m_LevelCondition;
    public string SceneName => m_SceneName;
    public int SceneNumber => m_SceneNumber;
    public Sprite ScenePreviewImage => m_ScenePreviewImage;
}