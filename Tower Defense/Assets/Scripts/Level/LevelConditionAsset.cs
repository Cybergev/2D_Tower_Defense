using UnityEngine;
using System;

[CreateAssetMenu]
public class LevelConditionAsset : ScriptableObject
{
    [SerializeField] private float levelConditionTime = 1;
    //[SerializeField] private int levelConditionScore = 1;
    [SerializeField] private int levelConditionGold = 1;
    [SerializeField] private int levelConditionLife = 1;
    [SerializeField] private string levelConditionEnemyDestroyed = "All";
    [SerializeField] private bool levelConditionSpawnComplete = true;

    public float LevelConditionTime => levelConditionTime;
    //public int LevelConditionScore => levelConditionScore;
    public int LevelConditionGold => levelConditionGold;
    public int LevelConditionLife => levelConditionLife;
    public string LevelConditionEnemyDestroyed => levelConditionEnemyDestroyed;
    public bool LevelConditionSpawnComplete => levelConditionSpawnComplete;

    #region ConditionCheckers
    public bool LevelConditionTimeIsComplete
    {
        get 
        { 
            return levelConditionTime <= LevelsController.Instance.LevelTime;
        }
    }
    /*
    public bool LevelScoreIsComplete
    {
        get
        {
            return levelConditionScore <= Player.Instance.NumScore;
        }
    }
    */
    public bool LevelConditionGoldIsComplete
    {
        get
        {
            return levelConditionGold <= Player.Instance.NumGold;
        }
    }
    public bool LevelConditionLifeIsComplete
    {
        get
        {
            return levelConditionLife <= Player.Instance.NumLives;
        }
    }
    public bool LevelConditionEnemyDestroyedIsComplete
    {
        get
        {
            int numDestroyed = 0;
            if (levelConditionEnemyDestroyed == "All" || levelConditionEnemyDestroyed == "all")
                foreach (var spawner in Spawner.AllSpawners)
                    numDestroyed += spawner.NumSpawnObjects * spawner.NumSpawnIterations;
            else
                numDestroyed = int.Parse(levelConditionEnemyDestroyed);
            return numDestroyed == Destructible.NumDestroyed;
        }
    }
    public bool LevelConditionSpawnCompleteIsComplete
    {
        get
        {
            int numCompleted = 0;
            foreach (var spawner in Spawner.AllSpawners)
                numCompleted += spawner.SpawnIsComplete ? 1 : 0;
            return numCompleted == Spawner.AllSpawners.Count;
        }
    }
    public bool LevelConditionIsCompleted 
    { 
        get 
        { 
            return LevelConditionTimeIsComplete && /*LevelScoreIsComplete &&*/LevelConditionGoldIsComplete && LevelConditionLifeIsComplete && LevelConditionEnemyDestroyedIsComplete && LevelConditionSpawnCompleteIsComplete; 
        } 
    }
    #endregion
}
