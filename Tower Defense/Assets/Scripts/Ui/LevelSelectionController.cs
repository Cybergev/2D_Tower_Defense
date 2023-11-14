using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionController : MonoBehaviour
{
    [SerializeField] private Level m_Level;
    [SerializeField] private RectTransform[] m_ResultShowImages;

    public Level Level => m_Level;

    private void Start()
    {
        foreach(var image in m_ResultShowImages)
        {
            image.gameObject.SetActive(false);
        }
        foreach (LevelResult result in LevelResultController.Instance.ArrayLevelResults)
        {
            if (result.LevelName == m_Level.LevelName)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0 && result.LevelConditionSuccess >= 50)
                        m_ResultShowImages[i].gameObject.SetActive(true);
                    if (i == 1 && result.LevelConditionSuccess >= 50)
                        m_ResultShowImages[i].gameObject.SetActive(true);
                    if (i == 2 && result.LevelConditionSuccess == 100)
                        m_ResultShowImages[i].gameObject.SetActive(true);
                }
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