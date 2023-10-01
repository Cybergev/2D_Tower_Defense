using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionController : MonoBehaviour
{
    [SerializeField] private Level m_Level;
    [SerializeField] private Text m_LevelNickname;
    [SerializeField] private Image m_PreviewImage;

    private void Start()
    {
        m_LevelNickname.text = m_Level.LevelName;
        m_PreviewImage.sprite = m_Level.PreviewImage;
    }

    public void OnStartEpisodeButtonClicked()
    {
        LevelSequenceController.Instance.StartLevel(m_Level);
    }
}