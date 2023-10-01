using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum SpawnMode
{
    Limit,
    Loop
}

public abstract class Spawner : MonoBehaviour
{
    protected abstract GameObject GenerateSpawnEntity();
    [SerializeField] private CircleArea m_Area;
    [SerializeField] private SpawnMode m_SpawnMode;
    [SerializeField] private float m_RespawnTime;

    [SerializeField] private int m_NumSpawnObjects;
    [SerializeField] private int m_NumSpawnIterations;
    private int m_NumCurrentSpawnIterations = 0;

    public int NumSpawnObjects => m_NumSpawnObjects;
    public int NumSpawnIterations => m_NumSpawnIterations;

    public bool SpawnIsComplete { get; private set; }
    public static HashSet<GameObject> SpawnedObjects { get; private set; }

    private Timer m_Timer;

    [SerializeField] private UnityEvent m_EventOnSpawnObject;
    [SerializeField] public UnityEvent EventOnSpawnObject => m_EventOnSpawnObject;

    [SerializeField] private UnityEvent m_EventOnSpawnIteration;
    [SerializeField] public UnityEvent EventOnSpawnIteration => m_EventOnSpawnIteration;

    [SerializeField] private UnityEvent m_EventOnSpawnComplete;
    [SerializeField] public UnityEvent EventOnSpawnComplete => m_EventOnSpawnComplete;

    protected virtual void Start()
    {
        m_Timer = new Timer(m_RespawnTime);
        m_NumCurrentSpawnIterations = 0;

        Spawn(m_NumSpawnObjects);
        m_NumCurrentSpawnIterations++;
        if (m_NumCurrentSpawnIterations == m_NumSpawnIterations)
        {
            m_EventOnSpawnComplete.Invoke();
            SpawnIsComplete = true;
        }
        else
            SpawnIsComplete = false;
    }
    protected virtual void Update()
    {
        if (!m_Timer.IsFinished)
            m_Timer.RemoveTime(Time.deltaTime);

        if (m_SpawnMode == SpawnMode.Limit && m_NumCurrentSpawnIterations < m_NumSpawnIterations && m_Timer.IsFinished)
        {
            Spawn(m_NumSpawnObjects);
            m_Timer = new Timer(m_RespawnTime);
            m_NumCurrentSpawnIterations++;
            m_EventOnSpawnIteration.Invoke();
            if (m_NumCurrentSpawnIterations == m_NumSpawnIterations)
            {
                m_EventOnSpawnComplete.Invoke();
                SpawnIsComplete = true;
            }
            else
                SpawnIsComplete = false;
        }
        if (m_SpawnMode == SpawnMode.Loop && m_Timer.IsFinished)
        {
            Spawn(m_NumSpawnObjects);
            m_Timer = new Timer(m_RespawnTime);
        }
    }

    protected virtual void Spawn(int numSpawnObjects)
    {
        if (numSpawnObjects <= 0)
            return;
        for (int i = 0; i < numSpawnObjects; i++)
        {
            var entity = GenerateSpawnEntity();
            entity.transform.position = m_Area.GetRandomInsideZone();
            if (SpawnedObjects == null) 
                SpawnedObjects = new HashSet<GameObject>();
            else
                SpawnedObjects.Add(entity);
            m_EventOnSpawnObject.Invoke();
        }
    }

    public void SetSpawnMode(string v_SpawnMode)
    {
        if(v_SpawnMode == "Limit")
            m_SpawnMode = SpawnMode.Limit;
        if (v_SpawnMode == "Loop")
            m_SpawnMode = SpawnMode.Loop;
    }
    public void SetRespawnTime(float v_RespawnTime)
    {
        if (v_RespawnTime < 0)
            return;
        m_RespawnTime = v_RespawnTime;
    }
    public void SetNumSpawnObjects(int v_NumSpawnObjects)
    {
        if (v_NumSpawnObjects <= 0)
            return;
        m_NumSpawnObjects = v_NumSpawnObjects;
    }
    public void SetNumSpawnIterations(int v_NumSpawnIterations)
    {
        if (v_NumSpawnIterations < 0)
            return;
        m_NumSpawnIterations = v_NumSpawnIterations;
    }
    public void SetNumCurrentSpawnIterations(int v_NumCurrentSpawnIterations)
    {
        if (v_NumCurrentSpawnIterations < 0)
            return;
        m_NumCurrentSpawnIterations = v_NumCurrentSpawnIterations;
    }

}
