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
            if (levelConditionLife == "Full" || levelConditionLife == "full")
                return Player.Instance.NumLive >= Player.Instance.NumStartLive;
            else
                return Player.Instance.NumLive >= int.Parse(levelConditionLife);
        }
    }
}
