using UnityEngine;

[CreateAssetMenu]
public class SpawnScenarioAsset : ScriptableObject
{
    [SerializeField] protected SpawnDataAsset[] spawnDataAssets;
    [SerializeField] protected float timeBetweenScenario;
    public SpawnDataAsset[] SpawnDataAssets => spawnDataAssets;
    public float TimeBetweenScenario => timeBetweenScenario;

    //Secondary specific data
    [SerializeField] protected SpawnScenarioSecondaryAsset secondaryAsset;
    public SpawnScenarioSecondaryAsset SecondaryAsset => secondaryAsset;
}