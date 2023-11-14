using UnityEngine;
[CreateAssetMenu]
public class LevelConditionGold : LevelCondiionAsset
{
    [SerializeField] private int levelConditionGold = 1;
    public override bool ConditionIsComplete => levelConditionGold <= Player.Instance.NumGold;
}
