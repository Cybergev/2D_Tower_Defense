using UnityEngine;

public abstract class ConditionAsset : ScriptableObject
{
    public virtual bool ConditionIsComplete
    {
        get
        {
            return false;
        }
    }
}
