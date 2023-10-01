using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConditionAllEnemiesDestroyed : MonoBehaviour, ILevelCondition
{
    [SerializeField] private Spawner[] spawners;
    private int sholdBeKill = 0;
    private bool m_Reached;

    private void Start()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            sholdBeKill += spawners[i].NumSpawnObjects * spawners[i].NumSpawnIterations;
        }
    }

    bool ILevelCondition.IsCompleted
    {
        get
        {
            if (TDPlayer.Instance.NumKills == sholdBeKill)
                m_Reached = true;
            return m_Reached;
        }
    }
}