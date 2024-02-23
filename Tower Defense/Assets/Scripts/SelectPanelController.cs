using UnityEngine;
using UnityEngine.UI;

public class SelectPanelController : MonoSingleton<SelectPanelController>
{
    [SerializeField] private Image m_image;
    [SerializeField] private Text m_text;

    private LevelAsset selectedLevel;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void ShowSlectedLevel (LevelAsset level)
    {
        gameObject.SetActive(true);
        foreach (var result in LevelResultController.Instance.ArrayLevelResults)
        {
            if(result.LevelName == level.LevelName)
            {
                selectedLevel = level;
                m_image.sprite = level.ScenePreviewImage;
                m_text.text = $"Name:{result.LevelName}\nComplete:{result.LevelConditionSuccess}\nScore:{result.LevelScore}\nTime:{result.LevelTime}";
            }
        }
    }

    public void StartSelectionLevel()
    {
        LevelsController.Instance.StartLevel(selectedLevel);
    }
}
