using UnityEngine;

public class LevelPanelController : MonoBehaviour
{
    [SerializeField] private LevelSelectionController[] uiLevelSelectionObjects;
    void Start()
    {
        if (uiLevelSelectionObjects.Length == 0)
            uiLevelSelectionObjects = GetComponentsInChildren<LevelSelectionController>();
        foreach (var levelSelect in uiLevelSelectionObjects)
        {
            int numCompleted = 0;
            foreach (var condition in levelSelect.Level.LevelAccessConditions)
                numCompleted = condition.ConditionIsComplete ? 1 : 0;
            bool isCompleted = numCompleted == levelSelect.Level.LevelAccessConditions.Length;
            levelSelect.Button.interactable = isCompleted;
            numCompleted = 0;
        }
    }
}
