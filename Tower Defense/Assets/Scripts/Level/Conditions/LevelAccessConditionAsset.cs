using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelAccessConditionAsset : ConditionAsset
{
    /// <summary>
    /// Уровень, который должен быть выполнен
    /// </summary>
    [SerializeField] private Level targetLevel;
    [SerializeField] private float targetLevelSuccess;
    [SerializeField] private float targetLevelScore;
    [SerializeField] private float targetLevelTime;
    public override bool ConditionIsComplete 
    { 
        get
        {
            bool result = false;
            if (targetLevel)
            {
                var levelResult = LevelResultController.Instance.GetLevelResult(targetLevel.LevelName);
                if (levelResult != null)
                    result = levelResult.LevelConditionSuccess >= targetLevelSuccess && levelResult.LevelScore >= targetLevelScore && levelResult.LevelTime <= targetLevelTime;
            }
            return result;
        }
    }
}
