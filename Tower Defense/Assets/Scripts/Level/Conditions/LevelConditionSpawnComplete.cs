using UnityEngine;
[CreateAssetMenu]
public class LevelConditionSpawnComplete : LevelCondiionAsset
{
    [SerializeField] private bool levelConditionSpawnComplete = true;
    public override bool ConditionIsComplete => levelConditionSpawnComplete == Spawner.AllSpawnsIsComplete;
}
