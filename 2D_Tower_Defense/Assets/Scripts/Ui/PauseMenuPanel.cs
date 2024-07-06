using UnityEngine;

public class PauseMenuPanel : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void OnButtonShowPause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnButtonRestart()
    {
        Time.timeScale = 1;
        LevelsController.Instance.RestartLevel();
    }
    public void OnButtonContinue()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void OnButtonMainMenu()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        LevelsController.Instance.CancelLevel();
    }
}