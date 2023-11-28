using UnityEngine;
[CreateAssetMenu]
public class SpawnScenarioSecondaryAsset : ScriptableObject
{
    [SerializeField] protected float timeBetweenScenario;
    [SerializeField] protected int callReaward;
    public float TimeBetweenScenario => timeBetweenScenario;
    public int CallReaward => callReaward;
}