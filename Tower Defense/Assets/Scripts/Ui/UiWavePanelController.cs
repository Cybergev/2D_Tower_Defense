using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UiWavePanelController : UiTextController
{
    [SerializeField] private GameObject callButton;
    [SerializeField] private string waveCompleteText = "Waves complete!";

    private int wave;
    private float time;
    private int reward;
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
            text = waveCompleteText;
        }
        else
        {
            if (scenarioComplete)
                text = $"Wave:{wave}\nTime:{time}\nIf call:+{reward}G";
            else
                text = $"Wave:{wave}";
            callButton.SetActive(scenarioComplete);
        }
        Text = text;
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
    public override void OnChangeTargetValueAmount(string value)
    {
        return;
    }
    public override void OnChangeTargetValueAmount(int iValue)
    {
        return;
    }
    public override void OnChangeTargetValueAmount(float fValue)
    {
        return;
    }
    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(UiWavePanelController))]
    public class UiWavePanelControllerInspector : Editor
    {
        private UiWavePanelController wavePanel;
        private void OnEnable()
        {
            wavePanel = (UiWavePanelController)target;
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(wavePanel.targetExitText)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(wavePanel.callButton)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(wavePanel.waveCompleteText)));
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
    #endregion
}
