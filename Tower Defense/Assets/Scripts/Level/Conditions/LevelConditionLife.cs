using UnityEngine;
[CreateAssetMenu]
public class LevelConditionLife : ConditionAsset
{
    [SerializeField] private string levelConditionLife = "Full";
    public override bool ConditionIsComplete
    {
        get
        {
            if (levelConditionLife == null)
                return false;
            return levelConditionLife == "Full" || levelConditionLife == "full" ? Player.Instance.NumLive >= Player.Instance.NumStartLive : Player.Instance.NumLive >= int.Parse(levelConditionLife);
        }
    }
}
