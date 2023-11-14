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
    private void Update()
    {
        CheckPlayerDataChanges();
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
            m_NumLives -= damage;

            if (m_NumLives <= 0)
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

    [SerializeField] private int m_StartLives;
    public int StartLives => m_StartLives;

    private int m_NumLives;
    public int NumLives => m_NumLives;
    public int PastNumLives { get; private set; }

    public int NumKills { get; private set; }
    public int PastNumKills { get; private set; }

    [SerializeField] private int m_StartGold;
    public int StartGold => m_StartGold;

    private int m_NumGold;
    public int NumGold => m_NumGold;
    public int PastNumGold { get; private set; }
    public int SpentGold { get; private set; }

    public float NumScore
    {
        get
        {
            return (NumGold + SpentGold) * NumLives / LevelsController.Instance.LevelTime;
        }
    }
    public float PastNumScore { get; private set; }

    private void PlayerIni()
    {
        m_NumLives = m_StartLives;
        m_NumGold = m_StartGold;
    }
    private void CheckPlayerDataChanges()
    {
        if (PastNumLives != NumLives)
        {
            PastNumLives = NumLives;
            changeLivesAmount.Invoke(NumLives);
        }
        if (PastNumScore != NumScore)
        {
            PastNumScore = NumScore;
            changeScoreAmount.Invoke(NumScore);
        }
        if (PastNumGold != NumGold)
        {
            PastNumGold = NumGold;
            changeGoldAmount.Invoke(NumGold);
        }
        if (PastNumKills != NumKills)
        {
            PastNumKills = NumKills;
            changeKillsAmount.Invoke(NumKills);
        }
    }

    public void ChangeGold(int value)
    {
        if (value < 0)
            SpentGold += -value;
        m_NumGold += value;
    }
    public void AddKill()
    {
        NumKills++;
    }
    #endregion
}