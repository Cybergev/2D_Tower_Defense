using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConditionEnemyDestroyed : MonoBehaviour, ILevelCondition
{
    [SerializeField] private int m_NumDeadEnemies;
    private bool m_Reached;

    bool ILevelCondition.IsCompleted
    {
        get
        {
            if (TDPlayer.Instance.NumKills == m_NumDeadEnemies)
                m_Reached = true;
            return m_Reached;
        }
    }
}