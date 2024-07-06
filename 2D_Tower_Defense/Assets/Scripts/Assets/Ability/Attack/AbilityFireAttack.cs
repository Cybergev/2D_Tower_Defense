using System;
using UnityEngine;
[CreateAssetMenu]
public class AbilityFireAttack : AbilityAttackAsset
{
    [SerializeField] private ExplosionProperties explosionProperties;
    [SerializeField] private Explosion explosionPrefab;
    public override Action Attack(UpgradeAsset upgrade, Vector2 point)
    {
        return () => 
        {
            Instantiate(explosionPrefab, point, Quaternion.identity).Explode(explosionProperties);
        };
    }
}
