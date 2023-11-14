using UnityEngine;

[CreateAssetMenu]
public class SpawnScenarioAsset : ScriptableObject
{
    [SerializeField] protected SpawnDataAsset[] spawnDataAssets;
    public SpawnDataAsset[] SpawnDataAssets => spawnDataAssets;
}