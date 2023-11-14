using UnityEngine;

[CreateAssetMenu]
public class Level : ScriptableObject
{
    #region DataFields
    //SceneData v
    [SerializeField] private string m_SceneName;
    [SerializeField] private int m_SceneNumber;
    [SerializeField] private Sprite m_ScenePreviewImage;
    //^
    //LevelData v
    [SerializeField] private string m_LevelName;
    [SerializeField] private LevelCondiionAsset[] m_LevelCompleteCondition;
    [SerializeField] private LevelCondiionAsset[] m_LevelBonusCondition;
    [SerializeField] private SpawnScenarioAsset[] m_LevelSpawnScenarios;
    //^
    #endregion

    #region PublicFields
    //ScenePublic v
    public string SceneName => m_SceneName;
    public int SceneNumber => m_SceneNumber;
    public Sprite ScenePreviewImage => m_ScenePreviewImage;
    //^
    //LevelPublic v
    public string LevelName => m_LevelName;
    public LevelCondiionAsset[] LevelCompleteCondition => m_LevelCompleteCondition;
    public LevelCondiionAsset[] LevelBonusCondition => m_LevelBonusCondition;
    public SpawnScenarioAsset[] LevelSpawnScenarios => m_LevelSpawnScenarios;
    //^
    #endregion
}