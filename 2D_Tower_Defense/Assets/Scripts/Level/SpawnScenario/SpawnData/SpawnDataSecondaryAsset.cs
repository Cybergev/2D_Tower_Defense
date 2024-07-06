using UnityEngine;
[CreateAssetMenu]
public class SpawnDataSecondaryAsset : ScriptableObject
{
    [SerializeField] private EnemyAsset m_EnemySetting;
    [SerializeField] private Path.PathTypes m_PathType;

    public EnemyAsset EnemySetting => m_EnemySetting;
    public Path.PathTypes PathType => m_PathType;
}