using UnityEngine;
[CreateAssetMenu]
public class SpawnScenarioSecondaryAsset : ScriptableObject
{
    [SerializeField] protected int callReaward;
    public int CallReaward => callReaward;
}