using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanelController : MonoBehaviour
{
    [SerializeField] 
    private LevelSelectionController[] uiLevelSelectionObjects;

    private LevelResult[] LevelResults => LevelResultController.Instance.ArrayLevelResults;

    void Start()
    {
        if (uiLevelSelectionObjects.Length == 0)
            uiLevelSelectionObjects = GetComponentsInChildren<LevelSelectionController>();
        if (LevelResults != null)
        {
            for (int i = 0; i < LevelResults.Length; i++)
            {
                if (i == 0)
                {
                    uiLevelSelectionObjects[i].gameObject.SetActive(true);
                }
                else
                    uiLevelSelectionObjects[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < LevelResults.Length; i++)
            {
                if (uiLevelSelectionObjects[i].Level.LevelName == LevelResults[i].levelName && LevelResults[i].levelSuccess)
                {
                    uiLevelSelectionObjects[i + 1].gameObject.SetActive(true);
                }
            }
        }
    }
}
