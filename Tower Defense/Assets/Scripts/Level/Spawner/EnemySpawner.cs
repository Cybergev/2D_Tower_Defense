using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : Spawner
{
    [SerializeField] protected UnityEvent m_EventOnSpawnEnemy;
    [HideInInspector] public UnityEvent EventOnSpawnEnemy => m_EventOnSpawnEnemy;

    [SerializeField] protected UnityEvent<Enemy> m_EventOnSpawnEnemyRef;
    [HideInInspector] public UnityEvent<Enemy> EventOnSpawnEnemyRef => m_EventOnSpawnEnemyRef;
    public Path CurrentPath { get; private set; }
    public static HashSet<EnemySpawner> AllEnemySpawners { get; private set; }
    protected override void OnEnable()
    {
        base.OnEnable();
        AllEnemySpawners ??= new HashSet<EnemySpawner>();
        AllEnemySpawners.Add(this);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        AllEnemySpawners.Remove(this);
    }
    protected override GameObject GenerateSpawnEntity(SpawnData spawnData)
    {
        var data = spawnData;
        GameObject completeObject;
        completeObject = Instantiate(data.CurrentSpawnData.NumSpawnObject);
        completeObject.GetComponent<Enemy>().UseAsset(data.CurrentSpawnData.SecondaryAsset.EnemySetting);
        completeObject.GetComponent<TDPatrolController>().SetPath(CurrentPath);
        m_EventOnSpawnEnemy.Invoke();
        m_EventOnSpawnEnemyRef.Invoke(completeObject.GetComponent<Enemy>());
        return completeObject;
    }
    public void SetSpawnScenario(SpawnScenarioAsset scenarios, Path path)
    {
        if (!scenarios || !path)
            return;
        SetSpawnData(scenarios);
        CurrentPath = path;
    }
}