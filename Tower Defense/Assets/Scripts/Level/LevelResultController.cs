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
            if (arrayLevelResults[i] == null || arrayLevelResults[i].levelName == null)
                arrayLevelResults[i] = new LevelResult(LevelsController.Instance.AllLevels[i].LevelName, false, 0, 1000);
        }
    }
    public void HashSaveLevelResult(LevelResult levelResult)
    {
        for (int i = 0; i < arrayLevelResults.Length; i++)
        {
            if (arrayLevelResults[i].levelName == levelResult.levelName)
            {
                bool hasLevelResultSuccessIsFalseAndNotSame = arrayLevelResults[i].levelSuccess == false && levelResult.levelSuccess == true;
                bool hasLevelResultSuccessIsSame = arrayLevelResults[i].levelSuccess == levelResult.levelSuccess;

                if (hasLevelResultSuccessIsFalseAndNotSame)
                {
                    arrayLevelResults[i].levelSuccess = levelResult.levelSuccess;
                    arrayLevelResults[i].levelScore = levelResult.levelScore;
                    arrayLevelResults[i].levelTime = levelResult.levelTime;
                }
                if (hasLevelResultSuccessIsSame)
                {
                    arrayLevelResults[i].levelScore = arrayLevelResults[i].levelScore < levelResult.levelScore ? levelResult.levelScore : arrayLevelResults[i].levelScore;
                    arrayLevelResults[i].levelTime = arrayLevelResults[i].levelTime > levelResult.levelTime ? levelResult.levelTime : arrayLevelResults[i].levelTime;
                }
            }
            if (arrayLevelResults[i].levelName != levelResult.levelName && i == arrayLevelResults.Length)
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
            arrayLevelResults[i] = new LevelResult(LevelsController.Instance.AllLevels[i].LevelName, false, 0, 1000);
        }
        HardSaveLevelResult(arrayLevelResults);
    }
}
[Serializable]
public class LevelResult
{
    public string levelName;
    public bool levelSuccess;
    public int levelScore;
    public float levelTime;
    public LevelResult()
    {
    }
    public LevelResult(string LevelName)
    {
        levelName = LevelName;
    }
    public LevelResult(string LevelName, bool LevelSuccess) : this(LevelName)
    {
        levelSuccess = LevelSuccess;
    }
    public LevelResult(string LevelName, bool LevelSuccess, int LevelScore) : this(LevelName, LevelSuccess)
    {
        levelScore = LevelScore;
    }
    public LevelResult(string LevelName, bool LevelSuccess, int LevelScore, float LevelTime) : this(LevelName, LevelSuccess, LevelScore)
    {
        levelTime = LevelTime;
    }
    public LevelResult(LevelResult levelResult) : this(levelResult.levelName, levelResult.levelSuccess, levelResult.levelScore, levelResult.levelTime)
    {
    }
}
