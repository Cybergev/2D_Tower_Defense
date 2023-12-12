using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionController : MonoBehaviour
{
    [SerializeField] private Level m_Level;
    [SerializeField] private Button m_Button;
    [SerializeField] private RectTransform[] m_ResultShowImages;

    public Level Level => m_Level;
    public Button Button => m_Button;

    private void Start()
    {
        foreach(var image in m_ResultShowImages)
        {
            image.gameObject.SetActive(false);
        }
        var result = LevelResultController.Instance.GetLevelResult(m_Level.LevelName);
        if (result != null)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == 0 && result.LevelConditionSuccess >= 25f)
                    m_ResultShowImages[i].gameObject.SetActive(true);
                if (i == 1 && result.LevelConditionSuccess >= 50f)
                    m_ResultShowImages[i].gameObject.SetActive(true);
                if (i == 2 && result.LevelConditionSuccess == 100f)
                    m_ResultShowImages[i].gameObject.SetActive(true);
            }
        }
    }

    public void OnSelectionClicked()
    {
        SelectPanelController.Instance.ShowSlectedLevel(m_Level);
    }
}