using UnityEngine;
[CreateAssetMenu]
public class LevelConditionLife : LevelCondiionAsset
{
    [SerializeField] private int levelConditionLife = 1;
    public override bool ConditionIsComplete => levelConditionLife <= Player.Instance.NumLives;
}
