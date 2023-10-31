using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] private Enemy m_EnemyPrefab;
    [SerializeField] private EnemyAsset[] m_EnemySettings;
    [SerializeField] private Path m_Path;
    public static HashSet<Enemy> SpawnedEnemies { get; private set; }
    public static HashSet<EnemySpawner> AllEnemySpawners { get; private set; }
    protected override void Start()
    {
        base.Start();
        AllEnemySpawners.Add(this);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        if (AllEnemySpawners == null) AllEnemySpawners = new HashSet<EnemySpawner>();
        AllEnemySpawners.Add(this);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        AllEnemySpawners.Remove(this);
    }
    protected override GameObject GenerateSpawnEntity()
    {
        var enemy = Instantiate(m_EnemyPrefab);
        if (SpawnedEnemies == null)
            SpawnedEnemies = new HashSet<Enemy>();
        else
            SpawnedEnemies.Add(enemy);
        enemy.UseAsset(m_EnemySettings[Random.Range(0, m_EnemySettings.Length)]);
        enemy.GetComponent<TDPatrolController>().SetPath(m_Path);
        return enemy.gameObject;
    }
}