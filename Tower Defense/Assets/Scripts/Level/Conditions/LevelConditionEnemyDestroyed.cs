using UnityEngine;
[CreateAssetMenu]
public class LevelConditionEnemyDestroyed : ConditionAsset
{
    [SerializeField] private string levelConditionEnemyDestroyed = "All";
    public override bool ConditionIsComplete
    {
        get
        {
            int numDestroyed = 0;
            SpawnScenarioAsset levelSpawnScenarios;
            SpawnDataAsset spawnDataAssets;
            if (levelConditionEnemyDestroyed == "All" || levelConditionEnemyDestroyed == "all")
                for (int i = 0; i < LevelsController.Instance.CurrentLevel.LevelSpawnScenarios.Length; i++)
                {
                    levelSpawnScenarios = LevelsController.Instance.CurrentLevel.LevelSpawnScenarios[i];
                    for (int j = 0; j < levelSpawnScenarios.SpawnDataAssets.Length; j++)
                    {
                        spawnDataAssets = levelSpawnScenarios.SpawnDataAssets[j];
                        numDestroyed += spawnDataAssets.NumSpawnObjects * spawnDataAssets.NumSpawnIterations;
                    }
                }
            else
                numDestroyed = int.Parse(levelConditionEnemyDestroyed);
            return numDestroyed == Destructible.NumDestroyed;
        }
    }
}
