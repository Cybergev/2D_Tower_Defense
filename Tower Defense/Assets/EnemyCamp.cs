using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCamp : MonoSingleton<EnemyCamp>
{
    [SerializeField] private EnemySpawner[] enemySpawners;
    [SerializeField] private Path[] paths;

    private int scenarioIndex;
    public int ScenarionIndex 
    {
        get
        {
            return scenarioIndex;
        }
        private set
        {
            scenarioIndex = value;
            waveIndexUpdate.Invoke(value + 1);
        }
    }
    public SpawnScenarioAsset[] SpawnScenarios { get; private set; }
    public SpawnScenarioAsset CurrentSpawnScenario { get; private set; }
    public Path CurrentPath { get; private set; }


    [SerializeField] private UnityEvent scenarioCompleteEvent;
    public UnityEvent ScenarioCompleteEvent => scenarioCompleteEvent;

    [SerializeField] private UnityEvent scenariosCompleteEvent;
    public UnityEvent ScenariosCompleteEvent => scenariosCompleteEvent;


    public Timer Timer { get; private set; }
    private int curentGoldReward;
    public int CurrentGoldReward 
    {
        get
        {
            return curentGoldReward;
        }
        private set
        {
            curentGoldReward = value;
            waveCallGoldRewardUpdate.Invoke(value);
        }
    }

    private int numSpawnCompleted;

    [SerializeField] private UnityEvent<int> waveIndexUpdate;
    public UnityEvent<int> WaveIndexUpdate => waveIndexUpdate;

    [SerializeField] private UnityEvent<float> waveCallTimerUpdate;
    public UnityEvent<float> WaveCallTimerUpdate => waveCallTimerUpdate;

    [SerializeField] private UnityEvent<int> waveCallGoldRewardUpdate;
    public UnityEvent<int> WaveCallGoldRewardUpdate => waveCallGoldRewardUpdate;

    public bool ScenarioIsComplete
    {
        get
        {
            bool check = numSpawnCompleted == enemySpawners.Length;
            if (check && !scenarioIsComplete)
            {
                scenarioIsComplete = check;
                scenarioCompleteEvent.Invoke();
                if (ScenarionIndex + 1 < SpawnScenarios.Length)
                    Timer ??= new Timer(SpawnScenarios[ScenarionIndex + 1].SecondaryAsset.TimeBetweenScenario);
                return check;
            }
            else
                return check;
        }
    }
    private bool scenarioIsComplete;

    public bool ScenariosAllComplete
    {
        get
        {
            bool check = ScenarioIsComplete && ScenarionIndex + 1 >= SpawnScenarios.Length;
            if (check && check != scenariosAllIsComplete)
            {
                scenariosAllIsComplete = check;
                scenariosCompleteEvent.Invoke();
            }
            return check;
        }
    }
    private bool scenariosAllIsComplete;

    private void Start()
    {
        GetScenarios(LevelsController.Instance.CurrentLevel.LevelSpawnScenarios);
        foreach (var spawner in enemySpawners)
        {
            if (spawner.SpawnIsComplete)
                SpawnComplete();
        }
        waveIndexUpdate.Invoke(scenarioIndex + 1);
        waveCallTimerUpdate.Invoke(Timer != null ? Timer.CurrentTime : 0);
        waveCallGoldRewardUpdate.Invoke(CurrentGoldReward);
    }
    private void FixedUpdate()
    {
        if (ScenarioIsComplete && !ScenariosAllComplete)
        {
            if (Timer != null)
            {
                if (Timer.IsFinished)
                {
                    CurrentGoldReward = 0;
                    Timer = null;
                    AdviceScenario();
                }
                else
                {
                    Timer.RemoveTime(Time.deltaTime);
                    waveCallTimerUpdate.Invoke(Timer.CurrentTime);
                    CurrentGoldReward = (int)(Timer.CurrentTime * 2);
                }
            }
        }
    }
    public void GetScenarios(SpawnScenarioAsset[] scenarios)
    {
        if (scenarios == null)
            return;
        SpawnScenarios = new SpawnScenarioAsset[scenarios.Length];
        for (int i = 0; i < scenarios.Length; i++)
        {
            var scenario = scenarios[i];
            SpawnScenarios[i] = scenario;
        }
    }
    public void SetScenarios(SpawnScenarioAsset scenarios)
    {
        if (scenarios == null)
            return;
        CurrentSpawnScenario = scenarios;
        foreach (var path in paths)
            foreach (var dataAsset in CurrentSpawnScenario.SpawnDataAssets)
                if (path.PathType == dataAsset.SecondaryAsset.PathType)
                    CurrentPath = path;
        numSpawnCompleted = 0;
        SetCurrentScnearioToSpawners();
    }
    public void SetCurrentScnearioToSpawners()
    {
        foreach (var spawner in enemySpawners)
            spawner.SetSpawnScenario(CurrentSpawnScenario, CurrentPath);
    }
    public void AdviceScenario()
    {
        if (CurrentSpawnScenario == null)
            SetScenarios(SpawnScenarios[ScenarionIndex]);
        else
            for (int i = 0; i < SpawnScenarios.Length; i++)
            {
                var scenario = SpawnScenarios[i];
                if (CurrentSpawnScenario == scenario && i + 1 < SpawnScenarios.Length)
                {
                    ScenarionIndex = i + 1;
                    SetScenarios(SpawnScenarios[ScenarionIndex]);
                }
            }
    }
    public void AdviceScenarioWithBonus()
    {
        Player.Instance.ChangeGold(CurrentGoldReward);
        AdviceScenario();
    }
    public void SpawnComplete()
    {
        numSpawnCompleted++;
    }
}
