using UnityEngine;
[CreateAssetMenu]
public class LevelConditionLife : ConditionAsset
{
    [SerializeField] private int levelConditionLife = 1;
    public override bool ConditionIsComplete => levelConditionLife <= Player.Instance.NumLive;
}
