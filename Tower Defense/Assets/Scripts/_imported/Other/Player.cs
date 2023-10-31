using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoSingleton<Player>
{
    [SerializeField] private UnityEvent m_EventOnPlayerDeath;
    [HideInInspector] public UnityEvent EventOnPlayerDeath => m_EventOnPlayerDeath;

    [SerializeField] private UnityEvent m_EventOnShipRespawn;
    [HideInInspector] public UnityEvent EventOnShipRespawn => m_EventOnShipRespawn;

    [SerializeField] private SpaceShip m_Ship;
    [SerializeField] private GameObject m_PlayerShipPrefab;
    public SpaceShip ActiveShip => m_Ship;
    [SerializeField] private Transform[] m_SpawnPoints;
    private Transform m_SpawnPoint;

    //[SerializeField] private CameraController m_CameraController;
    //[SerializeField] private MovementController m_MovementController;


    protected override void Awake()
    {
        base.Awake();
        if (m_Ship)
        {
            Destroy(m_Ship.gameObject);
            m_SpawnPoint = m_SpawnPoints[m_Ship.TeamId];
            Respawn();
        }
        else
        {

        }
    }

    private void Update()
    {
        CheckPlayerDataChenges();
    }

    private void OnPlayerDeath()
    {
        m_EventOnPlayerDeath.Invoke();
        LevelsController.Instance.FinishLevel(false);
    }

    private void OnShipDeath()
    {
        m_NumLives--;

        m_Ship.EventOnDeath.RemoveListener(OnShipDeath);
        if (m_NumLives > 0) 
            Respawn();
        else
            LevelsController.Instance.FinishLevel(false);
    }

    private void Respawn()
    {
        if (LevelsController.Instance.PlayerShip != null)
        {
            var newPlayerShip = Instantiate(LevelsController.Instance.PlayerShip, m_SpawnPoint);

            m_Ship = newPlayerShip.GetComponent<SpaceShip>();
            m_Ship.EventOnDeath.AddListener(OnShipDeath);
            m_Ship.SetOwner(gameObject);

            //m_CameraController.SetTarget(m_Ship.transform);
            //m_MovementController.SetTargetShip(m_Ship);
            m_EventOnShipRespawn.Invoke();
        }
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

    [SerializeField] private UnityEvent<int> changeScoreAmount;
    [HideInInspector] public UnityEvent<int> ChangeScoreAmount => changeScoreAmount;

    [SerializeField] private UnityEvent<int> changeKillsAmount;
    [HideInInspector] public UnityEvent<int> ChangeKillsAmount => changeLivesAmount;

    [SerializeField] private UnityEvent<int> changeGoldAmount;
    [HideInInspector] public UnityEvent<int> ChangeGoldAmount => changeGoldAmount;


    [SerializeField] private int m_NumLives;
    public int NumLives => m_NumLives;
    public int PastNumLives { get; private set; }

    public int NumScore { get; private set; }
    public int PastNumScore { get; private set; }


    [SerializeField] private int m_NumGold;
    public int NumGold => m_NumGold;
    public int PastNumGold { get; private set; }

    public int NumKills { get; private set; }
    public int PastNumKills { get; private set; }

    private void CheckPlayerDataChenges()
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

    public void ChangeScore(int num)
    {
        NumScore += num;
    }
    public void ChangeGold(int value)
    {
        m_NumGold += value;
    }
    public void AddKill()
    {
        NumKills++;
    }
    #endregion
}