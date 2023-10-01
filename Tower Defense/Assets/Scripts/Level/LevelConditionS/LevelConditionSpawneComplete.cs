using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConditionSpawneComplete : MonoBehaviour, ILevelCondition
{
    [SerializeField] private Spawner[] m_targetSpawners;
    private bool m_Reached;

    bool ILevelCondition.IsCompleted
    {
        get
        {
            int numCompleted = 0;
            foreach (var v in m_targetSpawners)
            {
                if (v.SpawnIsComplete) numCompleted++;
            }
            if (numCompleted == m_targetSpawners.Length)
            {
                m_Reached = true;
            }
            return m_Reached;
        }
    }
}