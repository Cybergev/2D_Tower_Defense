using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoSingleton<Player>
{
    [SerializeField] private UnityEvent<int> changeLivesAmount;
    [HideInInspector] public UnityEvent<int> ChangeLivesAmount => changeLivesAmount;

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

    protected virtual void Update()
    {
        if(PastNumLives != NumLives)
        {
            PastNumLives = NumLives;
            changeLivesAmount.Invoke(NumLives);
        }
    }

    private void OnPlayerDeath()
    {
        m_EventOnPlayerDeath.Invoke();
        LevelController.Instance.FinishLevel(false, Score);
    }

    private void OnShipDeath()
    {
        m_NumLives--;

        m_Ship.EventOnDeath.RemoveListener(OnShipDeath);
        if (m_NumLives > 0) 
            Respawn();
        else
            LevelController.Instance.FinishLevel(false, Score);
    }

    private void Respawn()
    {
        if (LevelSequenceController.Instance.PlayerShip != null)
        {
            var newPlayerShip = Instantiate(LevelSequenceController.Instance.PlayerShip, m_SpawnPoint);

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