using UnityEngine;
[CreateAssetMenu]
public class LevelConditionSpawnComplete : ConditionAsset
{
    [SerializeField] private bool levelConditionSpawnComplete = true;
    public override bool ConditionIsComplete => levelConditionSpawnComplete == Spawner.AllSpawnsIsComplete;
}
