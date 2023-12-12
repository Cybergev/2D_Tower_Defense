using UnityEngine;
[CreateAssetMenu]
public class LevelConditionGold : ConditionAsset
{
    [SerializeField] private int levelConditionGold = 1;
    public override bool ConditionIsComplete => levelConditionGold <= Player.Instance.NumGold;
}
