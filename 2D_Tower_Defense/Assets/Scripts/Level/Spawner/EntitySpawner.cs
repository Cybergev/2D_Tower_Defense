using UnityEngine;

public class EntitySpawner : Spawner
{
    [SerializeField] private GameObject[] m_EntityPrefab;

    protected override GameObject GenerateSpawnEntity(SpawnData spawnData)
    {
        return Instantiate(spawnData.CurrentSpawnData.NumSpawnObject); ;
    }
}