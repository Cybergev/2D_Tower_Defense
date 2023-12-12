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
    public ConditionAsset[] CompleteConditions { get; private set; }
    public ConditionAsset[] BonusConditions { get; private set; }
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
    private void EventsIni()
    {
        m_EventLevelCompleted.RemoveAllListeners();
        m_EventLevelCanceled.RemoveAllListeners();
        m_EventLevelCompleted.AddListener(Destructible.ClearNumDestroyed);
        m_EventLevelCanceled.AddListener(Destructible.ClearNumDestroyed);
    }

    private void FixedUpdate()
    {
        LevelTime += !LevelIsComlpete ? Time.deltaTime : 0;
    }

    #region LevelControllTolls
    public void FinishLevel(float conditionSuccess)
    {
        LevelResult result = new LevelResult(CurrentLevel.LevelName, conditionSuccess, Player.Instance.NumScore, LevelTime);
        LevelResultController.Instance.HashSaveLevelResult(result);
        LevelResultController.Instance.HardSaveLevelResult(LevelResultController.Instance.ArrayLevelResults.ToArray());
        ResultPanelController.Instance.ShownResult(result);
    }
    #endregion

    #region LevelSequenceTools
    public void StartLevel(Level level)
    {
        CurrentLevel = level;
        CompleteConditions = level.LevelCompleteConditions.Length > 0 ? level.LevelCompleteConditions : null;
        BonusConditions = level.LevelBonusConditions;
        LevelTime = 0;
        SceneManager.LoadScene(level.SceneNumber);
        EventsIni();
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