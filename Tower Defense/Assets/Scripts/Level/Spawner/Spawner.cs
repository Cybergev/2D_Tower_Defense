using System;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Events;

public abstract class Spawner : MonoBehaviour
{
    protected class SpawnData
    {
        public SpawnDataAsset CurrentSpawnData { get; private set; }
        public int m_NumCurrentSpawnIterations;
        public Timer m_Timer = new Timer(0);
        public bool SpawnIsComplete => m_NumCurrentSpawnIterations >= CurrentSpawnData.NumSpawnIterations;
        public SpawnData(SpawnDataAsset asset)
        {
            if (!asset)
                return;
            CurrentSpawnData = asset;
        }
    }
    protected SpawnData[] CurrentSpawnDataArray;
    [SerializeField] private CircleArea m_Area;

    public bool SpawnIsComplete 
    {
        get
        {
            if (CurrentSpawnDataArray != null)
            {
                int numComplete = 0;
                foreach (var spawner in CurrentSpawnDataArray)
                    numComplete += spawner.SpawnIsComplete ? 1 : 0;
                return numComplete == CurrentSpawnDataArray.Length;
            }
            else
                return true;
        }
    }
    public static bool AllSpawnsIsComplete 
    {
        get
        {
            int numComplete = 0;
            foreach(var spawner in AllSpawners)
                numComplete += spawner.SpawnIsComplete ? 1 : 0;
            return numComplete == AllSpawners.Count;
        }
    }
    [SerializeField] protected UnityEvent m_EventOnSpawnObject;
    [HideInInspector] public UnityEvent EventOnSpawnObject => m_EventOnSpawnObject;

    [SerializeField] protected UnityEvent m_EventOnSpawnIteration;
    [HideInInspector] public UnityEvent EventOnSpawnIteration => m_EventOnSpawnIteration;

    [SerializeField] protected UnityEvent m_EventOnSpawnComplete;
    [HideInInspector] public UnityEvent EventOnSpawnComplete => m_EventOnSpawnComplete;
    public static HashSet<Spawner> AllSpawners { get; protected set; }
    public static HashSet<GameObject> SpawnedObjects { get; protected set; }
    protected virtual void OnEnable()
    {
        AllSpawners ??= new HashSet<Spawner>();
        AllSpawners.Add(this);
    }
    protected virtual void OnDestroy()
    {
        AllSpawners.Remove(this);
    }
    protected virtual void FixedUpdate()
    {
        TrySpawn();
    }
    protected virtual void TrySpawn()
    {
        if(CurrentSpawnDataArray != null)
            for (int i = 0; i < CurrentSpawnDataArray.Length; i++)
            {
                var spawnData = CurrentSpawnDataArray[i];
                if (!spawnData.m_Timer.IsFinished)
                    spawnData.m_Timer.RemoveTime(Time.deltaTime);
                if (spawnData.CurrentSpawnData.NumSpawnMode == SpawnDataAsset.SpawnMode.Limit && !spawnData.SpawnIsComplete && spawnData.m_Timer.IsFinished)
                {
                    Spawn(spawnData);
                    m_EventOnSpawnObject.Invoke();
                    spawnData.m_Timer = new Timer(spawnData.CurrentSpawnData.NumRespawnTime);
                    spawnData.m_NumCurrentSpawnIterations++;
                    m_EventOnSpawnIteration.Invoke();
                    if (SpawnIsComplete)
                        m_EventOnSpawnComplete.Invoke();
                }
                if (spawnData.CurrentSpawnData.NumSpawnMode == SpawnDataAsset.SpawnMode.Loop && spawnData.m_Timer.IsFinished)
                {
                    Spawn(spawnData);
                    spawnData.m_Timer = new Timer(spawnData.CurrentSpawnData.NumRespawnTime);
                }
            }
    }
    protected virtual void Spawn(SpawnData spawnData)
    {
        GameObject spawnObj;
        for (int j = 0; j < spawnData.CurrentSpawnData.NumSpawnObjects; j++)
        {
            spawnObj = GenerateSpawnEntity(spawnData);
            spawnObj.transform.position = m_Area.GetRandomInsideZone();
            if (SpawnedObjects == null)
                SpawnedObjects = new HashSet<GameObject>();
            SpawnedObjects.Add(spawnObj);
        }
    }
    protected abstract GameObject GenerateSpawnEntity(SpawnData spawnData);
    public virtual void SetSpawnDataArray(SpawnScenarioAsset[] data)
    {
        if (data == null)
            return;
        int newlengt = 0;
        foreach (SpawnScenarioAsset asset in data)
        {
            newlengt += asset.SpawnDataAssets.Length;
        }
        CurrentSpawnDataArray = new SpawnData[newlengt];
        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data[i].SpawnDataAssets.Length; j++)
            {
                CurrentSpawnDataArray[j] = new SpawnData(data[i].SpawnDataAssets[j]);
            }
        }
    }
    public virtual void SetSpawnData(SpawnScenarioAsset data)
    {
        if (data == null)
            return;
        CurrentSpawnDataArray = new SpawnData[data.SpawnDataAssets.Length];
        for (int i = 0; i < data.SpawnDataAssets.Length; i++)
        {
            CurrentSpawnDataArray[i] = new SpawnData(data.SpawnDataAssets[i]);
        }
    }
}
