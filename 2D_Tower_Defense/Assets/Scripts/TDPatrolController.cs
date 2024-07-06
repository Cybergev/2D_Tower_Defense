using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TDPatrolController : AIController
{
    private Path m_Path;
    private int pathIndex;
    [SerializeField] private UnityEvent OnEndPath;
    public void SetPath(Path Path)
    {
        m_Path = Path;
        pathIndex = 0;
        SetPatrolBehaviour(m_Path[pathIndex]);
    }

    protected override void GetNewPoint()
    {
        if (m_Path.Length > ++pathIndex)
            SetPatrolBehaviour(m_Path[pathIndex]);
        else
        {
            OnEndPath.Invoke();
            Destroy(gameObject);
        }
    }
}
