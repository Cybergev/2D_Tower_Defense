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
        LevelSequenceController.Instance.PlayerShip = m_DefaultSpaceShip;
    }

    public void OnButtonStartNew()
    {
        m_EpisodeSelection.gameObject.SetActive(true);

        gameObject.SetActive(false);
    }

    public void OnSelectShip()
    {
        m_ShipSelection.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnButtonExit()
    {
        Application.Quit();
    }
}