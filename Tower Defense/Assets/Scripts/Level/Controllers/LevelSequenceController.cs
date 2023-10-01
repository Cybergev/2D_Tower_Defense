using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSequenceController : MonoSingleton<LevelSequenceController>
{
    /// <summary>
    /// Первый элемент массива(0) всегда сцена главного меню.
    /// </summary>
    [SerializeField] private Level[] m_AllLevels;
    public Level[] AllLevels => m_AllLevels;
    public Level CurrentLevel { get; private set; }

    public bool LastLevelResult { get; private set; }

    public PlayerStatistics LevelStatistics { get; private set; }

    public static SpaceShip PlayerShip { get; set; }

    public void StartLevel(Level level)
    {
        CurrentLevel = level;

        LevelStatistics = new PlayerStatistics();
        LevelStatistics.Reset();

        SceneManager.LoadScene(level.SceneNumber);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(CurrentLevel.SceneNumber);
    }

    public void FinishCurrentLevel(bool success)
    {
        LastLevelResult = success;
        LevelStatistics.CalculateLevelStatistic();
        LevelStatistics.SaveLevelStatistic();

        ResultPanelController.Instance.ShownResults(LevelStatistics, success);
    }

    public void AvanceLevel()
    {
        LevelStatistics.Reset();

        if (CurrentLevel.SceneNumber + 1 < m_AllLevels.Length)
        {
            StartLevel(m_AllLevels[CurrentLevel.SceneNumber + 1]);
        }
        else
        {
            SceneManager.LoadScene(m_AllLevels[0].SceneNumber);
        }
    }

}