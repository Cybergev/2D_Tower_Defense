using UnityEngine;
using UnityEngine.UI;

public class ResultPanelController : MonoSingleton<ResultPanelController>
{
    [SerializeField] private Text m_CompleteText;
    [SerializeField] private Text m_ResultText;
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
        m_CompleteText.text = result.LevelConditionSuccess > 0 ? "Win" : "Lose";
        m_ResultText.text = $"Condition succes:{result.LevelConditionSuccess}%\nLevel score:{result.LevelScore}\nLevel time:{result.LevelTime}";
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