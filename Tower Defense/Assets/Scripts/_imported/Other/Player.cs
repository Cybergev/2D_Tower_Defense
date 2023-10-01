using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoSingleton<Player>
{
    private static event Action<int> ChangeLivesAmount;

    [SerializeField] private UnityEvent m_EventOnPlayerDeath;
    [HideInInspector] public UnityEvent EventOnPlayerDeath => m_EventOnPlayerDeath;

    [SerializeField] private UnityEvent m_EventOnShipRespawn;
    [HideInInspector] public UnityEvent EventOnShipRespawn => m_EventOnShipRespawn;

    [SerializeField] private SpaceShip m_Ship;
    [SerializeField] private GameObject m_PlayerShipPrefab;
    public SpaceShip ActiveShip => m_Ship;
    [SerializeField] private Transform[] m_SpawnPoints;
    private Transform m_SpawnPoint;

    [SerializeField] private int m_NumLives;
    public int NumLives => m_NumLives;
    public int PastNumLives { get; private set; }
    public static void LifeUpdateSubscribe(Action<int> act)
    {
        ChangeLivesAmount += act;
        act(Instance.m_NumLives);
    }
    public static void LifeUpdateDisscribe(Action<int> act)
    {
        ChangeLivesAmount -= act;
        act(Instance.m_NumLives);
    }


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

    private void Start()
    {
        ChangeLivesAmount(m_NumLives);
    }

    protected virtual void Update()
    {
        if(PastNumLives != NumLives)
        {
            PastNumLives = NumLives;
            ChangeLivesAmount(NumLives);
        }
    }

    private void onPlayerDeath()
    {
        m_EventOnPlayerDeath.Invoke();
        LevelSequenceController.Instance.FinishCurrentLevel(false);
    }

    private void onShipDeath()
    {
        m_NumLives--;

        m_Ship.EventOnDeath.RemoveListener(onShipDeath);
        if (m_NumLives > 0) 
            Respawn();
        else 
            LevelSequenceController.Instance.FinishCurrentLevel(false);
    }

    private void Respawn()
    {
        if (LevelSequenceController.PlayerShip != null)
        {
            var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip, m_SpawnPoint);

            m_Ship = newPlayerShip.GetComponent<SpaceShip>();
            m_Ship.EventOnDeath.AddListener(onShipDeath);
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
                onPlayerDeath();
        }
    }

    #region Score
    [HideInInspector] public UnityEvent ChangeScoreAmount;
    [HideInInspector] public UnityEvent ChangeKillsAmount;
    public int Score { get; private set; }
    public int NumKills { get; private set; }

    public void AddScore(int num)
    {
        Score += num;
        ChangeScoreAmount.Invoke();
    }
    public void AddKill()
    {
        NumKills++;
        ChangeKillsAmount.Invoke();
    }

    #endregion
}