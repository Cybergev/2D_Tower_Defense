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
    public LevelConditionAsset[] LevelConditions { get; private set; }
    public bool LevelIsComlpete 
    {
        get
        {
            if (LevelConditions == null || LevelConditions.Length == 0) 
                return true;
            int numCompleted = 0;
            foreach (var v in LevelConditions)
            {
                numCompleted += v.LevelConditionIsCompleted ? 1 : 0;
            }
            if (numCompleted == LevelConditions.Length)
            {
                FinishLevel(true);
                m_EventLevelCompleted?.Invoke();
                return true;
            }
            return false;
        }
    }
    public float LevelTime { get; private set; }
    public SpaceShip PlayerShip;
    #endregion

    private void Start()
    {
        m_EventLevelCompleted.AddListener(Destructible.ClearNumDestroyed);
        m_EventLevelCanceled.AddListener(Destructible.ClearNumDestroyed);
    }
    private void Update()
    {
        if (!LevelIsComlpete)
        {
            LevelTime += Time.deltaTime;
        }
    }

    #region LevelControllTolls
    public void FinishLevel(bool success)
    {
        LevelResult result = new LevelResult(CurrentLevel.LevelName, success, Player.Instance.NumScore, LevelTime);
        LevelResultController.Instance.HashSaveLevelResult(result);
        LevelResultController.Instance.HardSaveLevelResult(LevelResultController.Instance.ArrayLevelResults);
        ResultPanelController.Instance.ShownResult(result);
    }
    #endregion

    #region LevelSequenceTools
    public void StartLevel(Level level)
    {
        CurrentLevel = level;
        LevelConditions = level.LevelCondition.Length > 0 ? level.LevelCondition : null;
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
            if (result.levelName == CurrentLevel.LevelName)
            {
                if (result.levelSuccess)
                {
                    m_EventLevelCanceled.Invoke();
                    LoadMainMenu();
                }
                else
                {
                    if (result.levelScore < Player.Instance.NumScore)
                        result.levelScore = Player.Instance.NumScore;
                    if (result.levelTime > LevelTime)
                        result.levelTime = LevelTime;
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