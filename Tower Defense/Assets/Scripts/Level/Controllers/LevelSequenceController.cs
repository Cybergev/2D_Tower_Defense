using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSequenceController : MonoSingleton<LevelSequenceController>
{
    [SerializeField] private Level m_MainMenuLevel;
    [SerializeField] private Level[] m_AllLevels;
    public Level MainMenuLevel => m_MainMenuLevel;
    public Level[] AllLevels => m_AllLevels;
    public Level CurrentLevel { get; private set; }
    public SpaceShip PlayerShip;
    public void StartLevel(Level level)
    {
        CurrentLevel = level;
        SceneManager.LoadScene(level.SceneNumber);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(CurrentLevel.SceneNumber);
    }
    public void AvanceLevel()
    {
        if (CurrentLevel.SceneNumber + 1 < m_AllLevels.Length)
        {
            StartLevel(m_AllLevels[CurrentLevel.SceneNumber + 1]);
        }
        else
        {
            SceneManager.LoadScene(m_AllLevels[0].SceneNumber);
        }
    }
    public void LoadMineMenu()
    {
        SceneManager.LoadScene(m_MainMenuLevel.SceneNumber);
    }
}