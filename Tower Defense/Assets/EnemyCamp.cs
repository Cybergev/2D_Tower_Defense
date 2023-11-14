using System.Collections.Generic;
using UnityEngine;

public class EnemyCamp : MonoSingleton<EnemyCamp>
{
    [SerializeField] private EnemySpawner[] enemySpawners;
    [SerializeField] private Path[] paths;
    private SpawnScenarioAsset[] spawnScenarios;

    public int ScenarionIndex { get; private set; } = 0;
    public SpawnScenarioAsset CurrentSpawnScenario { get; private set; }
    public Path CurrentPath { get; private set; }

    private void Start()
    {
        SetScenarios(LevelsController.Instance.CurrentLevel.LevelSpawnScenarios);
    }
    public void SetScenarios(SpawnScenarioAsset[] scenarios)
    {
        if (scenarios == null)
            return;
        spawnScenarios = new SpawnScenarioAsset[scenarios.Length];
        for(int i = 0; i < scenarios.Length; i++)
        {
            var scenario = scenarios[i];
            spawnScenarios[i] = scenario;
        }
        CurrentSpawnScenario = spawnScenarios[ScenarionIndex];
        foreach (var path in paths)
            foreach (var dataAsset in CurrentSpawnScenario.SpawnDataAssets)
                if (path.PathType == dataAsset.SpawnDataSecond.PathType)
                    CurrentPath = path;
        SetCurrentScnearioToSpawners();
    }
    public void SetCurrentScnearioToSpawners()
    {
        foreach (var spawner in enemySpawners)
            spawner.SetSpawnScenario(CurrentSpawnScenario, CurrentPath);
    }
    public void AdviceScenario()
    {
        if (ScenarionIndex + 1 < spawnScenarios.Length)
        {
            ScenarionIndex++;
            CurrentSpawnScenario = spawnScenarios[ScenarionIndex];
        }
        else
            return;
        foreach (var path in paths)
            foreach (var dataAsset in CurrentSpawnScenario.SpawnDataAssets)
                if (path.PathType == dataAsset.SpawnDataSecond.PathType)
                    CurrentPath = path;
        SetCurrentScnearioToSpawners();
    }
}
