using System.Collections;
using System.Collections.Generic;
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

    public void OnButtonContinue()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void OnButtonMainMenu()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        LevelController.Instance.CancelLevel();
    }
}