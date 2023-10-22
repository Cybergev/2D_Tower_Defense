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
    public void StartLevel(string levelName)
    {
        for (int i = 0; i < m_AllLevels.Length; i++)
        {
            if (m_AllLevels[i].name == levelName)
                StartLevel(m_AllLevels[i]);
        }
    }
    public void StartLevel(int leveNumber)
    {
        for (int i = 0; i < m_AllLevels.Length; i++)
        {
            if (m_AllLevels[i].SceneNumber == leveNumber)
                StartLevel(m_AllLevels[i]);
        }
    }
    public void RestartLevel()
    {
        StartLevel(CurrentLevel);
    }
    public void AvanceLevel()
    {
        for (int i = 0; i < m_AllLevels.Length; i++)
        {
            if (m_AllLevels[i].LevelName == CurrentLevel.LevelName)
            {
                if (i == m_AllLevels.Length)
                    LoadMineMenu();
                else
                    StartLevel(CurrentLevel.SceneNumber + 1);
            }
        }
    }
    public void LoadMineMenu()
    {
        StartLevel(m_MainMenuLevel);
    }
}