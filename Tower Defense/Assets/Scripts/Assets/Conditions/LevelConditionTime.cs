using UnityEngine;
[CreateAssetMenu]
public class LevelConditionTime : ConditionAsset
{
    [SerializeField] private float levelConditionTime = 1;
    public override bool ConditionIsComplete => levelConditionTime <= LevelsController.Instance.LevelTime;
}
