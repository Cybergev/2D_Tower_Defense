using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
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
        completeObject.GetComponent<Enemy>().UseAsset(data.CurrentSpawnData.SpawnDataSecond.EnemySetting);
        completeObject.GetComponent<TDPatrolController>().SetPath(CurrentPath);
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