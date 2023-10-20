using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultPanelController : MonoSingleton<ResultPanelController>
{
    [SerializeField] private Text m_Score;
    [SerializeField] private Text m_Time;
    [SerializeField] private Text m_Result;
    [SerializeField] private Text m_ButtonNextText;

    private bool m_Success;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShownResult(LevelResult result)
    {
        gameObject.SetActive(true);
        m_Score.text = result.levelScore.ToString();
        m_Time.text = result.levelTime.ToString();
        m_Result.text = result.levelSuccess ? "Win" : "Lose";
        m_ButtonNextText.text = result.levelSuccess ? "Next" : "Restart";
        m_Success = result.levelSuccess;
        Time.timeScale = 0;
    }

    public void OnButtonNextAction()
    {
        gameObject.SetActive(false);

        Time.timeScale = 1;

        if (m_Success)
        {
            LevelSequenceController.Instance.AvanceLevel();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}