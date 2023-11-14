using UnityEngine;
[CreateAssetMenu]
public class LevelConditionTime : LevelCondiionAsset
{
    [SerializeField] private float levelConditionTime = 1;
    public override bool ConditionIsComplete => levelConditionTime <= LevelsController.Instance.LevelTime;
}
