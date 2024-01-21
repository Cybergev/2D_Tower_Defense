using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class UnlockerAsset : ItemAsset
{
    public enum UnlockerType
    {
        Other,
        Tower,
        Player
    }
    [SerializeField] private UnlockerType unlockTarget;
    [SerializeField] private TowerAsset unlockTowerAsset;
    public UnlockerType UnlockTarget => unlockTarget;
    public TowerAsset UnlockTowerAsset => unlockTowerAsset;
}
