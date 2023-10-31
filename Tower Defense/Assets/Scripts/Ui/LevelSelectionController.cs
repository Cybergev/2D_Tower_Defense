using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionController : MonoBehaviour
{
    [SerializeField] private Level m_Level;
    [SerializeField] private Text m_LevelText;

    public Level Level => m_Level;

    private void Start()
    {
        foreach (LevelResult result in LevelResultController.Instance.ArrayLevelResults)
        {
            if (result.levelName == m_Level.LevelName)
            {
                m_LevelText.text = $"Name:{result.levelName}\nComplete:{result.levelSuccess}";
                break;
            }
            else
                continue;
        }
    }

    public void OnSelectionClicked()
    {
        SelectPanelController.Instance.ShowSlectedLevel(m_Level);
    }
}