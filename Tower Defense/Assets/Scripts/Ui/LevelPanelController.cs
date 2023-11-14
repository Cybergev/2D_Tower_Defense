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
                    uiLevelSelectionObjects[i].gameObject.SetActive(true);
                else
                    uiLevelSelectionObjects[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < LevelResults.Length; i++)
            {
                if (uiLevelSelectionObjects[i].Level.LevelName == LevelResults[i].LevelName && LevelResults[i].LevelConditionSuccess > 0)
                    if (i + 1 < LevelResults.Length)
                        uiLevelSelectionObjects[i + 1].gameObject.SetActive(true);
            }
        }
    }
}
