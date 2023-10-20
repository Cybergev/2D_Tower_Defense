using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionController : MonoBehaviour
{
    [SerializeField] private Level m_Level;
    [SerializeField] private Text m_LevelNickname;
    [SerializeField] private Image m_PreviewImage;

    public Level Level => m_Level;

    private void Start()
    {
        bool l_success;
        int l_score;
        float l_time;
        foreach (LevelResult result in LevelResultController.Instance.ArrayLevelResults)
        {
            if (result.levelName == m_Level.LevelName)
            {
                l_success = result.levelSuccess;
                l_score = result.levelScore;
                l_time = result.levelTime;
                m_PreviewImage.sprite = m_Level.PreviewImage;
                m_LevelNickname.text = $"Name:{m_Level.LevelName}\nComplete:{l_success}\nScore:{l_score}\nTime:{l_time}";
                break;
            }
            else
                continue;
        }
    }

    public void OnStartEpisodeButtonClicked()
    {
        LevelSequenceController.Instance.StartLevel(m_Level);
    }
}