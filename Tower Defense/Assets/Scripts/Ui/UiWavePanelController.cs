using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

public class UiWavePanelController : MonoSingleton<UiWavePanelController>
{
    [SerializeField] private GameObject callButton;
    [SerializeField] private UnityEvent<string> uiPanelTextUpdate;
    public UnityEvent<string> UiWavePanelUpdate => uiPanelTextUpdate;

    int wave;
    float time;
    int reward;
    private void FixedUpdate()
    {
        UiUpdate();
    }
    private void UiUpdate()
    {
        string text;
        bool scenarioComplete = EnemyCamp.Instance.ScenarioIsComplete;
        bool scenariosComplete = EnemyCamp.Instance.ScenariosAllComplete;
        if (scenariosComplete)
        {
            text = "Waves complete!";
        }
        else
        {
            if (scenarioComplete)
                text = $"Wave:{wave}\nTime:{time}\nIf call:+{reward}G";
            else
                text = $"Wave:{wave}";
            callButton.gameObject.SetActive(scenarioComplete);
        }
        uiPanelTextUpdate.Invoke(text);
    }
    public void WaveStringUpdate(int w)
    {
        if (w < 0)
            return;
        wave = w;
    }
    public void TimeStringUpdate(float t)
    {
        if (t < 0)
            return;
        time = t;
    }
    public void RewardStringUpdate(int r)
    {
        if (r < 0)
            return;
        reward = r;
    }
    public void CallWave()
    {
        EnemyCamp.Instance.AdviceScenarioWithBonus();
    }
}
