using UnityEngine;

[CreateAssetMenu]
public class SpawnScenarioAsset : ScriptableObject
{
    [SerializeField] protected SpawnDataAsset[] spawnDataAssets;
    public SpawnDataAsset[] SpawnDataAssets => spawnDataAssets;

    //Secondary specific data
    [SerializeField] protected SpawnScenarioSecondaryAsset secondaryAsset;
    public SpawnScenarioSecondaryAsset SecondaryAsset => secondaryAsset;
}