using System;
using UnityEngine;

public abstract class AbilityBuffAsset : AbilityAsset
{
    [SerializeField] private float buffTime;
    public float BuffTIme => buffTime;
    public abstract Action Buff(UpgradeAsset upgrade);
    public abstract Action Unbuff(UpgradeAsset upgrade);
}
