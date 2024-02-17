using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelsController : MonoSingleton<LevelsController>
{
    #region LevelData
    [SerializeField] private Level m_MainMenuLevel;
    [SerializeField] private Level[] m_MainLevels;
    [SerializeField] private Level[] m_BranchLevels;
    public Level MainMenuLevel => m_MainMenuLevel;
    public Level[] MainLevels => m_MainLevels;
    public Level[] BranchLevels => m_BranchLevels;
    public Level[] AllLevels
    {
        get
        {
            List<Level> levels = new List<Level>();
            foreach (Level level in m_MainLevels)
                levels.Add(level);
            foreach (Level level in m_BranchLevels)
                levels.Add(level);
            return levels.ToArray();
        }
    }
    public Level CurrentLevel { get; private set; }
    #endregion

    #region LevelControllData
    [SerializeField] private UnityEvent m_EventLevelCompleted;
    [HideInInspector] public UnityEvent EventLevelCompleted => m_EventLevelCompleted;

    [SerializeField] private UnityEvent m_EventLevelCanceled;
    [HideInInspector] public UnityEvent EventLevelCanceled => m_EventLevelCanceled;

    [SerializeField] private UnityEvent m_EventLevelRestarted;
    [HideInInspector] public UnityEvent EventLevelRestarted => m_EventLevelRestarted;

    [SerializeField] private UnityEvent m_EventLevelStarted;
    [HideInInspector] public UnityEvent EventLevelStarted => m_EventLevelStarted;
    public ConditionAsset[] CompleteConditions { get; private set; }
    public ConditionAsset[] BonusConditions { get; private set; }
    public bool LevelIsComplete
    {
        get
        {
            if (CompleteConditions == null || CompleteConditions.Length == 0)
                return false;
            int numCompleted = 0;
            foreach (var v in CompleteConditions)
                numCompleted += v.ConditionIsComplete ? 1 : 0;
            if (numCompleted == CompleteConditions.Length)
            {
                int numBonus = 0;
                foreach (var v in BonusConditions)
                    numBonus += v.ConditionIsComplete ? 1 : 0;
                float succes = (numCompleted + numBonus) / ((CompleteConditions.Length + BonusConditions.Length) / 100f);
                int reward = numCompleted + numBonus;
                FinishLevel(succes, reward);
                return true;
            }
            return false;
        }
    }
    public float LevelTime { get; private set; }
    #endregion

    private void FixedUpdate()
    {
        LevelTime += !LevelIsComplete ? Time.deltaTime : 0;
    }

    #region LevelControllTolls
    public void FinishLevel(float conditionSuccess, int reward)
    {
        LevelResult result = new LevelResult(CurrentLevel.LevelName, conditionSuccess, reward, Player.Instance.NumScore, LevelTime);
        LevelResultController.Instance.HashSaveLevelResult(result);
        LevelResultController.Instance.HardSaveLevelResult(LevelResultController.Instance.ArrayLevelResults.ToArray());
        ResultPanelController.Instance.ShownResult(result);
        m_EventLevelCompleted.Invoke();
    }
    #endregion

    #region LevelSequenceTools
    private void EventsIni(UnityAction action)
    {
        m_EventLevelCompleted.RemoveAllListeners();
        m_EventLevelCanceled.RemoveAllListeners();
        m_EventLevelRestarted.RemoveAllListeners();
        m_EventLevelStarted.RemoveAllListeners();
        m_EventLevelCompleted.AddListener(action);
        m_EventLevelCanceled.AddListener(action);
        m_EventLevelRestarted.AddListener(action);
        m_EventLevelStarted.AddListener(action);
    }
    public void StartLevel(Level level)
    {
        if (!level)
            return;
        CurrentLevel = level;
        CompleteConditions = null;
        CompleteConditions = level.LevelCompleteConditions.Length > 0 ? level.LevelCompleteConditions : null;
        BonusConditions = null;
        BonusConditions = level.LevelBonusConditions;
        LevelTime = 0;
        Destructible.ClearNumDestroyed();
        SceneManager.LoadScene(level.SceneNumber);
        EventsIni(Destructible.ClearNumDestroyed);
        m_EventLevelStarted.Invoke();
    }
    public void StartLevel(string levelName)
    {
        for (int i = 0; i < m_MainLevels.Length; i++)
        {
            if (m_MainLevels[i].name == levelName)
                StartLevel(m_MainLevels[i]);
        }
    }
    public void StartLevel(int leveNumber)
    {
        for (int i = 0; i < m_MainLevels.Length; i++)
        {
            if (m_MainLevels[i].SceneNumber == leveNumber)
                StartLevel(m_MainLevels[i]);
        }
    }
    public void CancelLevel()
    {
        foreach (var result in LevelResultController.Instance.ArrayLevelResults)
        {
            if (result.LevelName == CurrentLevel.LevelName)
            {
                if (result.LevelConditionSuccess <= 0)
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
        m_EventLevelRestarted.Invoke();
        StartLevel(CurrentLevel);
    }
    public void AvanceLevel()
    {
        if (CurrentLevel.NextLevel != null)
            StartLevel(CurrentLevel.NextLevel);
        else
            LoadMainMenu();
    }
    public void LoadMainMenu()
    {
        StartLevel(m_MainMenuLevel);
    }
    #endregion
}