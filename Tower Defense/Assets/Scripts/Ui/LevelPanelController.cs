using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanelController : MonoBehaviour
{
    private Level[] Levels => LevelSequenceController.Instance.AllLevels;
    [SerializeField] 
    private LevelSelectionController[] uiLevelSelectionObjects;

    void Start()
    {
        if (uiLevelSelectionObjects.Length == 0)
            uiLevelSelectionObjects = GetComponentsInChildren<LevelSelectionController>();

        var drawLevel = 0;
        foreach (var hashResults in LevelResultController.Instance.ArrayLevelResults)
        {
            if (hashResults.levelName == Levels[drawLevel].LevelName)
            {
                if (hashResults.levelSuccess)
                    drawLevel++;
                else
                    break;
            }
            else
                continue;
        }
        for (int i = drawLevel; i < Levels.Length; i++)
        {
            uiLevelSelectionObjects[i].gameObject.SetActive(false);
        }

    }

}
