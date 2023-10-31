using UnityEngine;
using UnityEngine.UI;

public class ResultPanelController : MonoSingleton<ResultPanelController>
{
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
        m_Success.text = result.levelSuccess ? "Win" : "Lose";
        m_Score.text = result.levelScore.ToString();
        m_Time.text = result.levelTime.ToString();
        m_ButtonNextText.text = result.levelSuccess ? "Next" : "Restart";
        Time.timeScale = 0;
    }

    public void OnButtonNextAction()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        if (currentResult.levelSuccess)
            LevelsController.Instance.AvanceLevel();
        else
            LevelsController.Instance.RestartLevel();
    }
}