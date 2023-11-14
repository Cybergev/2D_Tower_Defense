using UnityEngine;

public abstract class LevelCondiionAsset : ScriptableObject
{
    public virtual bool ConditionIsComplete
    {
        get
        {
            return false;
        }
    }
}
