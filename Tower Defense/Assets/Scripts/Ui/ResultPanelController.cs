using UnityEngine;
using UnityEngine.UI;

public class ResultPanelController : MonoSingleton<ResultPanelController>
{
    [SerializeField] private Text m_Complete;
    [SerializeField] private Text m_Success;
    [SerializeField] private Text m_Score;
    [SerializeField] private Text m_Time;
    [SerializeField] private Text m_ButtonNextText;

    private LevelResult currentResult;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShownResult(LevelResult result)
    {
        gameObject.SetActive(true);
        currentResult = result;
        m_Complete.text = result.LevelConditionSuccess > 0 ? "Win" : "Lose";
        m_Success.text = "Condition succes: " + result.LevelConditionSuccess + "%";
        m_Score.text = "Score: " + result.LevelScore.ToString();
        m_Time.text = "Time: " + result.LevelTime.ToString();
        m_ButtonNextText.text = result.LevelConditionSuccess > 0 ? "Next" : "Restart";
        Time.timeScale = 0;
    }

    public void OnButtonNextAction()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        if (currentResult.LevelConditionSuccess > 0)
            LevelsController.Instance.AvanceLevel();
        else
            LevelsController.Instance.RestartLevel();
    }
}