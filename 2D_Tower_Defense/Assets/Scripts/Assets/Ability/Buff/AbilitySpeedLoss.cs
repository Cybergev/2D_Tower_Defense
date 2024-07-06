using System;
using UnityEngine;
[CreateAssetMenu]
public class AbilitySpeedLoss : AbilityBuffAsset
{
    [SerializeField, Range(1.0f, 0.0f)] private float speedDebuff;
    public override Action Buff(UpgradeAsset upgrade)
    {
        return () =>
        {
            foreach (var enemy in Enemy.AllDestructibles)
                if (enemy is Enemy)
                    Slow(enemy as Enemy);
            foreach (var spawner in EnemySpawner.AllEnemySpawners)
                spawner.EventOnSpawnEnemyRef.AddListener(Slow);
        };
    }
    public override Action Unbuff(UpgradeAsset upgrade)
    {
        return () =>
        {
            foreach (var enemy in Enemy.AllDestructibles)
                if (enemy is Enemy)
                    UnSlow(enemy as Enemy);
            foreach (var spawner in EnemySpawner.AllEnemySpawners)
                spawner.EventOnSpawnEnemyRef.RemoveListener(Slow);
        };
    }
    private void Slow(Enemy enemy)
    {
        if (!enemy)
            return;
        enemy.ChangeVelocity(speedDebuff);
    }
    private void UnSlow(Enemy enemy)
    {
        if (!enemy)
            return;
        enemy.BackupVelocity();
    }
}
