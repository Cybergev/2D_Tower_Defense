using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoSingleton<MainMenuController>
{
    [SerializeField] private GameObject m_EpisodeSelection;
    public void OnButtonStartNew()
    {
        m_EpisodeSelection.gameObject.SetActive(true);

        gameObject.SetActive(false);
    }
    public void OnButtonResumeGame()
    {
        gameObject.SetActive(false);
    }
    public void OnButtonClearSaves()
    {
        LevelResultController.Instance.ClearAllResults();
        LevelsController.Instance.LoadMainMenu();
    }
    public void OnButtonExit()
    {
        Application.Quit();
    }
}