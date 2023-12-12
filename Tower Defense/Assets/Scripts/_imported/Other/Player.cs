using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoSingleton<Player>
{
    [SerializeField] private UnityEvent m_EventOnPlayerDeath;
    [HideInInspector] public UnityEvent EventOnPlayerDeath => m_EventOnPlayerDeath;

    [SerializeField] private UnityEvent m_EventOnShipRespawn;
    [HideInInspector] public UnityEvent EventOnShipRespawn => m_EventOnShipRespawn;

    [SerializeField] private Transform[] m_SpawnPoints;

    //[SerializeField] private CameraController m_CameraController;
    //[SerializeField] private MovementController m_MovementController;


    protected override void Awake()
    {
        base.Awake();
        PlayerIni();
    }
    private void OnPlayerDeath()
    {
        m_EventOnPlayerDeath.Invoke();
        LevelsController.Instance.FinishLevel(0);
    }
    internal void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;
        else
        {
            NumLive -= damage;

            if (NumLive <= 0)
                OnPlayerDeath();
        }
    }

    #region Lives&Score&Money&Kills
    [SerializeField] private UnityEvent<int> changeLivesAmount;
    [HideInInspector] public UnityEvent<int> ChangeLivesAmount => changeLivesAmount;

    [SerializeField] private UnityEvent<int> changeKillsAmount;
    [HideInInspector] public UnityEvent<int> ChangeKillsAmount => changeLivesAmount;

    [SerializeField] private UnityEvent<int> changeGoldAmount;
    [HideInInspector] public UnityEvent<int> ChangeGoldAmount => changeGoldAmount;

    [SerializeField] private UnityEvent<float> changeScoreAmount;
    [HideInInspector] public UnityEvent<float> ChangeScoreAmount => changeScoreAmount;

    private int m_NumLive;
    public int NumLive
    {
        get
        {
            return m_NumLive;
        }
        private set
        {
            changeLivesAmount.Invoke(value);
            m_NumLive = value;
        }
    }

    private int m_NumKills;
    public int NumKills
    {
        get
        {
            return m_NumKills;
        }
        private set
        {
            changeKillsAmount.Invoke(value);
            m_NumKills = value;
        }
    }

    private int m_NumGold;
    public int NumGold 
    {
        get
        {
            return m_NumGold;
        }
        private set
        {
            changeGoldAmount.Invoke(value);
            m_NumGold = value;
        }
    }
    public int SpentGold { get; private set; }

    public float NumScore
    {
        get
        {
            float score = (NumGold + SpentGold) * NumLive / LevelsController.Instance.LevelTime;
            changeScoreAmount.Invoke(score);
            return score;
        }
    }

    private void PlayerIni()
    {
        NumLive = LevelsController.Instance.CurrentLevel.StartLive;
        NumGold = LevelsController.Instance.CurrentLevel.StartGold;
    }
    public void ChangeGold(int value)
    {
        if (value < 0)
            SpentGold += -value;
        NumGold += value;
    }
    public void AddKill()
    {
        NumKills++;
    }
    #endregion
}