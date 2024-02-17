using System;
using UnityEngine;

public abstract class AbilityAttackAsset : AbilityAsset
{
    public abstract Action Attack(UpgradeAsset upgrade, Vector2 point);
}
