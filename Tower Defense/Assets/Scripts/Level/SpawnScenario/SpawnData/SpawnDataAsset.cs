using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpawnDataAsset : ScriptableObject
{
    public enum SpawnMode { Limit, Loop }

    [SerializeField] protected GameObject m_SpawnObject;
    [SerializeField] protected SpawnMode m_SpawnMode;
    [SerializeField] protected int m_NumSpawnObjects;
    [SerializeField] protected int m_NumSpawnIterations;
    [SerializeField] protected float m_RespawnTime;

    public GameObject NumSpawnObject => m_SpawnObject;
    public SpawnMode NumSpawnMode => m_SpawnMode;
    public int NumSpawnObjects => m_NumSpawnObjects;
    public int NumSpawnIterations => m_NumSpawnIterations;
    public float NumRespawnTime => m_RespawnTime;

    //Secondary specific data
    [SerializeField] protected SpawnDataSecondAsset spawnDataSecond;
    public SpawnDataSecondAsset SpawnDataSecond => spawnDataSecond;
}
