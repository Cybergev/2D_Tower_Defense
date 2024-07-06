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
            if (levelConditionEnemyDestroyed == "All" || levelConditionEnemyDestroyed == "all")
                foreach (var levelSpawnScenarios in LevelsController.Instance.CurrentLevel.LevelSpawnScenarios)
                    foreach (var spawnDataAssets in levelSpawnScenarios.SpawnDataAssets)
                        numDestroyed += spawnDataAssets.NumSpawnObjects * spawnDataAssets.NumSpawnIterations;
            else
                numDestroyed = int.Parse(levelConditionEnemyDestroyed);
            return numDestroyed == Destructible.NumDestroyed;
        }
    }
}
