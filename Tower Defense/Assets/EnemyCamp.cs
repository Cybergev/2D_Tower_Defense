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
            waveIndexUpdate.Invoke(value);
        }
    }
    public SpawnScenarioAsset[] SpawnScenarios { get; private set; }
    public SpawnScenarioAsset CurrentSpawnScenario { get; private set; }
    public Path CurrentPath { get; private set; }


    [SerializeField] private UnityEvent scenarioCompleteEvent;
    public UnityEvent ScenarioCompleteEvent => scenarioCompleteEvent;

    [SerializeField] private UnityEvent scenariosCompleteEvent;
    public UnityEvent ScenariosCompleteEvent => scenariosCompleteEvent;


    public Timer Timer { get; private set; } = new Timer(0);
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
            bool check = numSpawnCompleted == enemySpawners.Length || CurrentSpawnScenario == null;
            if (check)
            {
                if (check != scenarioIsComplete)
                {
                    scenarioIsComplete = check;
                    scenarioCompleteEvent.Invoke();
                    Timer = new Timer(CheckNextScenario() ? CheckNextScenario().TimeBetweenScenario : 0);
                    return check;
                }
                else
                    return check;
            }
            else
            {
                if (check != scenarioIsComplete)
                {
                    scenarioIsComplete = check;
                    return check;
                }
                else
                    return check;
            }
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
        SetScenarios(LevelsController.Instance.CurrentLevel.LevelSpawnScenarios);
        foreach (var spawner in enemySpawners)
            if (spawner.SpawnIsComplete)
                SpawnComplete();
        waveIndexUpdate.Invoke(scenarioIndex + 1);
        waveCallTimerUpdate.Invoke(Timer.CurrentTime);
        CurrentGoldReward = (int)(Timer.CurrentTime * 2);
    }
    private void FixedUpdate()
    {
        if (ScenarioIsComplete)
        {
            if (!Timer.IsFinished)
            {
                Timer.RemoveTime(Time.deltaTime);
                waveCallTimerUpdate.Invoke(Timer.CurrentTime);
                CurrentGoldReward = (int)(Timer.CurrentTime * 2);
            }
            else
            {
                AdviceScenario();
                waveCallTimerUpdate.Invoke(Timer.CurrentTime);
                CurrentGoldReward = (int)(Timer.CurrentTime * 2);
            }
        }
    }
    public void SetScenarios(SpawnScenarioAsset[] scenarios)
    {
        if (scenarios == null)
            return;
        SpawnScenarios = scenarios;
    }
    public void SetCurrentScenario(SpawnScenarioAsset scenario)
    {
        if (scenario == null)
            return;
        CurrentSpawnScenario = scenario;
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
    public SpawnScenarioAsset CheckNextScenario()
    {
        SpawnScenarioAsset result = null;
        if (CurrentSpawnScenario == result)
            result = SpawnScenarios[0];
        else
            for (int i = 0; i < SpawnScenarios.Length; i++)
                if (CurrentSpawnScenario == SpawnScenarios[i] && i + 1 < SpawnScenarios.Length)
                    result = SpawnScenarios[i + 1];
        return result;
    }
    public void AdviceScenario()
    {
        SetCurrentScenario(CheckNextScenario());
        ScenarionIndex++;
        Timer = new Timer(0);
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
