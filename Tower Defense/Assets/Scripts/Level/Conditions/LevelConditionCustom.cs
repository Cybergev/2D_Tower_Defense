using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class LevelConditionCustom : ConditionAsset
{
    [SerializeField] private bool conditionComplete;
    public override bool ConditionIsComplete => conditionComplete;
}
