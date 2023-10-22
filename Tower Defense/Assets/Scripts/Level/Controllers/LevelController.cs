using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoSingleton<LevelController>
{
    [SerializeField] private int m_referenceTime;
    public int ReferenceTime => m_referenceTime;

    [SerializeField] private UnityEvent m_EventLevelCompleted;
    [SerializeField] private UnityEvent m_EventLevelCanceled;

    private ILevelCondition[] m_Conditions;

    private bool m_IsLevelCompleted;
    private float m_LevelTime;
    public float LevelTime => m_LevelTime;

    private void Start()
    {
        m_Conditions = GetComponentsInChildren<ILevelCondition>();
    }

    private void Update()
    {
        if (!m_IsLevelCompleted)
        {
            m_LevelTime += Time.deltaTime;

            CheckLevelConditions();
        }
    }
    private void CheckLevelConditions()
    {
        if (m_Conditions == null || m_Conditions.Length == 0) return;

        int numCompleted = 0;

        foreach (var v in m_Conditions)
        {
            if (v.IsCompleted) 
                numCompleted++;
        }

        if (numCompleted == m_Conditions.Length)
        {
            m_IsLevelCompleted = true;
            FinishLevel(true);
            m_EventLevelCompleted?.Invoke();
        }
    }
    public void FinishLevel(bool success)
    {
        LevelResult result = new LevelResult(LevelSequenceController.Instance.CurrentLevel.LevelName, success, Player.Instance.NumKills, LevelTime);
        LevelResultController.Instance.HashSaveLevelResult(result);
        LevelResultController.Instance.HardSaveLevelResult(LevelResultController.Instance.ArrayLevelResults);
        ResultPanelController.Instance.ShownResult(result);
    }
    public void CancelLevel()
    {
        foreach(var result in LevelResultController.Instance.ArrayLevelResults)
        {
            if(result.levelName == LevelSequenceController.Instance.CurrentLevel.LevelName)
            {
                if (result.levelSuccess)
                {
                    m_EventLevelCanceled.Invoke();
                    LevelSequenceController.Instance.LoadMineMenu();
                }
                else
                {
                    if (result.levelScore < Player.Instance.NumScore)
                        result.levelScore = Player.Instance.NumScore;
                    if (result.levelTime > LevelTime)
                        result.levelTime = LevelTime;
                    m_EventLevelCanceled.Invoke();
                    LevelSequenceController.Instance.LoadMineMenu();
                }
            }
        }
    }
}