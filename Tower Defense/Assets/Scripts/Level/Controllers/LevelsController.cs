using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelsController : MonoSingleton<LevelsController>
{
    #region LevelData
    [SerializeField] private Level m_MainMenuLevel;
    [SerializeField] private Level[] m_AllLevels;
    public Level MainMenuLevel => m_MainMenuLevel;
    public Level[] AllLevels => m_AllLevels;
    public Level CurrentLevel { get; private set; }
    #endregion

    #region LevelControllData
    [SerializeField] private UnityEvent m_EventLevelCompleted;
    [HideInInspector] public UnityEvent EventLevelCompleted => m_EventLevelCompleted;

    [SerializeField] private UnityEvent m_EventLevelCanceled;
    [HideInInspector] public UnityEvent EventLevelCanceled => m_EventLevelCanceled;
    public LevelCondiionAsset[] CompleteConditions { get; private set; }
    public LevelCondiionAsset[] BonusConditions { get; private set; }
    public bool LevelIsComlpete 
    {
        get
        {
            if (CompleteConditions == null || CompleteConditions.Length == 0) 
                return true;
            int numCompleted = 0;
            foreach (var v in CompleteConditions)
                numCompleted += v.ConditionIsComplete ? 1 : 0;
            if (numCompleted == CompleteConditions.Length)
            {
                int numBonus = 0;
                foreach (var v in BonusConditions)
                    numBonus += v.ConditionIsComplete ? 1 : 0;
                float succes = numBonus / (BonusConditions.Length / 100f);
                FinishLevel(succes);
                m_EventLevelCompleted.Invoke();
                return true;
            }
            return false;
        }
    }
    public float LevelTime { get; private set; }
    #endregion
    private void Start()
    {
        LevelIni();
    }
    private void LevelIni()
    {
        m_EventLevelCompleted.AddListener(Destructible.ClearNumDestroyed);
        m_EventLevelCanceled.AddListener(Destructible.ClearNumDestroyed);
    }

    private void Update()
    {
        LevelTime += !LevelIsComlpete ? Time.deltaTime : 0;
    }

    #region LevelControllTolls
    public void FinishLevel(float conditionSuccess)
    {
        LevelResult result = new LevelResult(CurrentLevel.LevelName, conditionSuccess, Player.Instance.NumScore, LevelTime);
        LevelResultController.Instance.HashSaveLevelResult(result);
        LevelResultController.Instance.HardSaveLevelResult(LevelResultController.Instance.ArrayLevelResults);
        ResultPanelController.Instance.ShownResult(result);
    }
    #endregion

    #region LevelSequenceTools
    public void StartLevel(Level level)
    {
        CurrentLevel = level;
        CompleteConditions = level.LevelCompleteCondition.Length > 0 ? level.LevelCompleteCondition : null;
        BonusConditions = level.LevelBonusCondition;
        LevelTime = 0;
        SceneManager.LoadScene(level.SceneNumber);
    }
    public void StartLevel(string levelName)
    {
        for (int i = 0; i < m_AllLevels.Length; i++)
        {
            if (m_AllLevels[i].name == levelName)
                StartLevel(m_AllLevels[i]);
        }
    }
    public void StartLevel(int leveNumber)
    {
        for (int i = 0; i < m_AllLevels.Length; i++)
        {
            if (m_AllLevels[i].SceneNumber == leveNumber)
                StartLevel(m_AllLevels[i]);
        }
    }
    public void CancelLevel()
    {
        foreach (var result in LevelResultController.Instance.ArrayLevelResults)
        {
            if (result.LevelName == CurrentLevel.LevelName)
            {
                if (result.LevelConditionSuccess > 0)
                {
                    m_EventLevelCanceled.Invoke();
                    LoadMainMenu();
                }
                else
                {
                    if (result.LevelScore < Player.Instance.NumScore)
                        result.LevelScore = Player.Instance.NumScore;
                    if (result.LevelTime > LevelTime)
                        result.LevelTime = LevelTime;
                    m_EventLevelCanceled.Invoke();
                    LoadMainMenu();
                }
            }
        }
    }
    public void RestartLevel()
    {
        m_EventLevelCanceled.Invoke();
        StartLevel(CurrentLevel);
    }
    public void AvanceLevel()
    {
        if (CurrentLevel.SceneNumber + 1 < SceneManager.sceneCountInBuildSettings)
            StartLevel(CurrentLevel.SceneNumber + 1);
        else
            LoadMainMenu();
    }
    public void LoadMainMenu()
    {
        StartLevel(m_MainMenuLevel);
    }
    #endregion
}