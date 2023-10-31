using UnityEngine;
using UnityEngine.UI;

public class SelectPanelController : MonoSingleton<SelectPanelController>
{
    [SerializeField] private Image m_image;
    [SerializeField] private Text m_text;

    private Level selectedLevel;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void ShowSlectedLevel (Level level)
    {
        gameObject.SetActive(true);
        foreach (var result in LevelResultController.Instance.ArrayLevelResults)
        {
            if(result.levelName == level.LevelName)
            {
                selectedLevel = level;
                m_image.sprite = level.ScenePreviewImage;
                m_text.text = $"Name:{result.levelName}\nComplete:{result.levelSuccess}\nScore:{result.levelScore}\nTime:{result.levelTime}";
            }
        }
    }

    public void StartSelectionLevel()
    {
        LevelsController.Instance.StartLevel(selectedLevel);
    }
}
