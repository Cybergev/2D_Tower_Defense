using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpawnDataAsset : ScriptableObject
{
    public enum SpawnMode { Limit, Loop }

    [SerializeField] protected SpawnMode m_SpawnMode;
    [SerializeField] protected GameObject m_SpawnObject;
    [SerializeField] protected int m_NumSpawnObjects;
    [SerializeField] protected int m_NumSpawnIterations;
    [SerializeField] protected float m_RespawnTime;

    public SpawnMode NumSpawnMode => m_SpawnMode;
    public GameObject NumSpawnObject => m_SpawnObject;
    public int NumSpawnObjects => m_NumSpawnObjects;
    public int NumSpawnIterations => m_NumSpawnIterations;
    public float NumRespawnTime => m_RespawnTime;

    //Secondary specific data
    [SerializeField] protected SpawnDataSecondaryAsset secondaryAsset;
    public SpawnDataSecondaryAsset SecondaryAsset => secondaryAsset;
}
