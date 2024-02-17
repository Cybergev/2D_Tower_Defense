using UnityEngine;

[CreateAssetMenu]
public class Level : ScriptableObject
{
    #region DataFields

    #region SceneData
    [Header("Scene Data")]
    [SerializeField] private string m_SceneName;
    [SerializeField] private int m_SceneNumber;
    [SerializeField] private Sprite m_ScenePreviewImage;
    #endregion

    #region LevelData
    [Header("Level Data")]
    [SerializeField] private string m_LevelName;
    [SerializeField] private LevelAccessConditionAsset[] m_LevelAccessConditions;
    [SerializeField] private ConditionAsset[] m_LevelCompleteConditions;
    [SerializeField] private ConditionAsset[] m_LevelBonusConditions;
    [SerializeField] private SpawnScenarioAsset[] m_LevelSpawnScenarios;
    [SerializeField] private Level m_PrevLevel;
    [SerializeField] private Level m_NextLevel;
    #endregion

    #region GameplayData
    [Header("Gameplay Data")]
    [SerializeField] private int m_StartLive;
    [SerializeField] private int m_StartMage;
    [SerializeField] private int m_StartGold;
    #endregion

    #endregion

    #region PublicFields

    #region ScenePublic
    public string SceneName => m_SceneName;
    public int SceneNumber => m_SceneNumber;
    public Sprite ScenePreviewImage => m_ScenePreviewImage;
    #endregion

    #region LevelPublic
    public string LevelName => m_LevelName;
    public LevelAccessConditionAsset[] LevelAccessConditions => m_LevelAccessConditions;
    public ConditionAsset[] LevelCompleteConditions => m_LevelCompleteConditions;
    public ConditionAsset[] LevelBonusConditions => m_LevelBonusConditions;
    public SpawnScenarioAsset[] LevelSpawnScenarios => m_LevelSpawnScenarios;
    public Level PrevtLevel => m_PrevLevel;
    public Level NextLevel => m_NextLevel;
    #endregion

    #region GameplayData
    public int StartLive => m_StartLive;
    public int StartMage => m_StartMage;
    public int StartGold => m_StartGold;
    #endregion

    #endregion
}