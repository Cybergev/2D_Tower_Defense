using System;

public class LevelResultController : MonoSingleton<LevelResultController>
{
    private LevelResult[] arrayLevelResults;
    public LevelResult[] ArrayLevelResults => arrayLevelResults;
    private void Start()
    {
        arrayLevelResults = new LevelResult[LevelsController.Instance.AllLevels.Length];
        HardLoadLevelResult(ref arrayLevelResults);
        for (int i = 0; i < arrayLevelResults.Length; i++)
        {
            if (arrayLevelResults[i] == null || arrayLevelResults[i].LevelName == null)
                arrayLevelResults[i] = new LevelResult(LevelsController.Instance.AllLevels[i].LevelName, 0, 0, 1000);
        }
    }
    public void HashSaveLevelResult(LevelResult levelResult)
    {
        for (int i = 0; i < arrayLevelResults.Length; i++)
        {
            if (arrayLevelResults[i].LevelName == levelResult.LevelName)
            {
                bool hasLevelResultSuccessIsFalseAndNotSame = arrayLevelResults[i].LevelConditionSuccess == 0 && levelResult.LevelConditionSuccess > 0;
                bool hasLevelResultSuccessIsSame = arrayLevelResults[i].LevelConditionSuccess == levelResult.LevelConditionSuccess;

                if (hasLevelResultSuccessIsFalseAndNotSame)
                {
                    arrayLevelResults[i].LevelConditionSuccess = levelResult.LevelConditionSuccess;
                    arrayLevelResults[i].LevelScore = levelResult.LevelScore;
                    arrayLevelResults[i].LevelTime = levelResult.LevelTime;
                }
                if (hasLevelResultSuccessIsSame)
                {
                    arrayLevelResults[i].LevelScore = arrayLevelResults[i].LevelScore < levelResult.LevelScore ? levelResult.LevelScore : arrayLevelResults[i].LevelScore;
                    arrayLevelResults[i].LevelTime = arrayLevelResults[i].LevelTime > levelResult.LevelTime ? levelResult.LevelTime : arrayLevelResults[i].LevelTime;
                }
            }
            if (arrayLevelResults[i].LevelName != levelResult.LevelName && i == arrayLevelResults.Length)
            {
                LevelResult[] ArrayLevelResultsNew = new LevelResult[arrayLevelResults.Length + 1];
                ArrayLevelResultsNew = arrayLevelResults;
                arrayLevelResults = new LevelResult[ArrayLevelResultsNew.Length];
                arrayLevelResults = ArrayLevelResultsNew;
                arrayLevelResults[i + 1] = levelResult;
            }
        }
    }
    public void HardSaveLevelResult(LevelResult[] levelResult)
    {
        SaveController<LevelResult[]>.Save(Save<LevelResult>.filename, levelResult);
    }
    public void HardLoadLevelResult(ref LevelResult[] levelResult)
    {
        SaveController<LevelResult[]>.TryLoad(Save<LevelResult>.filename, ref levelResult);
    }
    public void ClearAllResults()
    {
        arrayLevelResults = new LevelResult[LevelsController.Instance.AllLevels.Length];
        for (int i = 0; i < arrayLevelResults.Length; i++)
        {
            arrayLevelResults[i] = new LevelResult(LevelsController.Instance.AllLevels[i].LevelName, 0, 0, 1000);
        }
        HardSaveLevelResult(arrayLevelResults);
    }
}
[Serializable]
public class LevelResult
{
    public string LevelName;
    public float LevelConditionSuccess;
    public float LevelScore;
    public float LevelTime;
    public LevelResult()
    {
    }
    public LevelResult(string levelName)
    {
        LevelName = levelName;
    }
    public LevelResult(string levelName, float levelConditionSuccess) : this(levelName)
    {
        LevelConditionSuccess = levelConditionSuccess;
    }
    public LevelResult(string levelName, float levelConditionSuccess, float levelScore) : this(levelName, levelConditionSuccess)
    {
        LevelScore = levelScore;
    }
    public LevelResult(string levelName, float levelConditionSuccess, float levelScore, float levelTime) : this(levelName, levelConditionSuccess, levelScore)
    {
        LevelTime = levelTime;
    }
    public LevelResult(LevelResult levelResult) : this(levelResult.LevelName, levelResult.LevelConditionSuccess, levelResult.LevelScore, levelResult.LevelTime)
    {
    }
}
