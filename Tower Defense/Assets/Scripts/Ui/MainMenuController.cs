using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoSingleton<MainMenuController>
{
    [SerializeField] private SpaceShip m_DefaultSpaceShip;
    [SerializeField] private GameObject m_EpisodeSelection;
    [SerializeField] private GameObject m_ShipSelection;
    private void Start()
    {
        LevelsController.Instance.PlayerShip = m_DefaultSpaceShip;
    }
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