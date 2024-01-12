using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using SaveSystem;

public class LevelResultController : MonoSingleton<LevelResultController>
{
    private List<LevelResult> arrayLevelResults;
    public List<LevelResult> ArrayLevelResults => arrayLevelResults;
    private void Start()
    {
        LevelResultIni();
    }
    public void HashSaveLevelResult(LevelResult levelResult)
    {
        for (int i = 0; i < arrayLevelResults.Count; i++)
        {
            if (arrayLevelResults[i].LevelName == levelResult.LevelName)
            {
                bool hasLevelResultSuccessIsFalseAndNotSame = arrayLevelResults[i].LevelConditionSuccess == 0 && levelResult.LevelConditionSuccess > 0;
                bool hasLevelResultSuccessIsSame = arrayLevelResults[i].LevelConditionSuccess == levelResult.LevelConditionSuccess;
                if (hasLevelResultSuccessIsFalseAndNotSame)
                {
                    arrayLevelResults[i].LevelConditionSuccess = levelResult.LevelConditionSuccess;
                    arrayLevelResults[i].LevelReward = levelResult.LevelReward;
                    arrayLevelResults[i].LevelScore = levelResult.LevelScore;
                    arrayLevelResults[i].LevelTime = levelResult.LevelTime;
                }
                if (hasLevelResultSuccessIsSame)
                {
                    arrayLevelResults[i].LevelScore = arrayLevelResults[i].LevelScore < levelResult.LevelScore ? levelResult.LevelScore : arrayLevelResults[i].LevelScore;
                    arrayLevelResults[i].LevelTime = arrayLevelResults[i].LevelTime > levelResult.LevelTime ? levelResult.LevelTime : arrayLevelResults[i].LevelTime;
                }
            }
            if (arrayLevelResults[i].LevelName != levelResult.LevelName && i == arrayLevelResults.Count)
            {
                arrayLevelResults.Add(levelResult);
            }
        }
    }
    public void HasSaveLevelResult(LevelResult[] levelResults)
    {
        if (levelResults == null)
            return;
        foreach(var result in levelResults)
            HashSaveLevelResult(result);
    }
    public void HardSaveLevelResult(LevelResult levelResult)
    {
        if (levelResult == null)
            return;
        SaveController<LevelResult>.Save(levelResult.LevelName, levelResult);
    }
    public void HardSaveLevelResult(LevelResult[] levelResult)
    {
        if (levelResult == null)
            return;
        foreach(var result in levelResult)
            SaveController<LevelResult>.Save(result.LevelName, result);
    }
    public LevelResult HardLoadLevelResult(string levelName)
    {
        if (levelName == null)
            return null;
        LevelResult levelResult = SaveController<LevelResult>.TryLoad(levelName);
        return levelResult == null ? new LevelResult(levelName, 0, 0, 1000) : new LevelResult(levelResult);
    }
    public LevelResult[] HardLoadLevelResult(string[] levelNames)
    {
        List<LevelResult> levelResults = new List<LevelResult>();
        foreach (var v in levelNames)
        {
            LevelResult result = HardLoadLevelResult(v);
            levelResults.Add(result == null ? new LevelResult(v, 0, 0, 1000) : new LevelResult(result));
        }
        return levelResults.ToArray();
    }
    public LevelResult GetLevelResult(string LevelName)
    {
        foreach (var result in arrayLevelResults)
        {
            if (result.LevelName == LevelName)
                return result;
        }
        return null;
    }
    public LevelResult[] GetLevelResult(string[] LevelNames)
    {
        List<LevelResult> LevelResults = new List<LevelResult>();
        foreach (var name in LevelNames)
        {
            LevelResults.Add(GetLevelResult(name));
        }
        return LevelResults.ToArray();
    }
    public void ClearAllResults()
    {
        arrayLevelResults.Clear();
        for (int i = 0; i < LevelsController.Instance.AllLevels.Length; i++)
            arrayLevelResults.Add(new LevelResult(LevelsController.Instance.AllLevels[i].LevelName, 0, 0, 0, 1000));
        HardSaveLevelResult(arrayLevelResults.ToArray());
    }
    private void LevelResultIni()
    {
        arrayLevelResults = new List<LevelResult>();
        foreach(var level in LevelsController.Instance.AllLevels)
            arrayLevelResults.Add(HardLoadLevelResult(level.LevelName));
    }
}
[Serializable]
public class LevelResult
{
    public string LevelName;
    public float LevelConditionSuccess;
    public int LevelReward;
    public float LevelScore;
    public float LevelTime;
    public LevelResult()
    {
    }
    public LevelResult(string levelName)
    {
        LevelName = levelName;
    }
    public LevelResult(string levelName, float levelConditionSuccess, int levelReward) : this(levelName)
    {
        LevelConditionSuccess = levelConditionSuccess;
        LevelReward = levelReward;
    }
    public LevelResult(string levelName, float levelConditionSuccess, int levelReward, float levelScore) : this(levelName, levelConditionSuccess, levelReward)
    {
        LevelScore = levelScore;
    }
    public LevelResult(string levelName, float levelConditionSuccess, int levelReward, float levelScore, float levelTime) : this(levelName, levelConditionSuccess, levelReward, levelScore)
    {
        LevelTime = levelTime;
    }
    public LevelResult(LevelResult levelResult) : this(levelResult.LevelName, levelResult.LevelConditionSuccess,  levelResult.LevelReward, levelResult.LevelScore, levelResult.LevelTime)
    {
    }
}
