using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Объект имеющий хитпоинты.
/// </summary>
public class Destructible : Entity
{
    #region Properties
    /// <summary>
    /// Своиство уничтожаемости объекта.
    /// Если true - объект не может быть уничтожен.
    /// Если false - объект может быть уничтожен.
    /// </summary>
    [SerializeField] private bool m_Indestructible;
    public bool Indestuctible => m_Indestructible;
    /// <summary>
    /// Своиство повреждаемости объекта.
    /// Если true - объект не может быть повреждён
    /// Если true - объект может быть повреждён
    /// </summary>
    [SerializeField] private bool m_Indamageble;
    public bool Indamageble => m_Indamageble;
    /// <summary>
    /// Текущее количество хитпоинтов.
    /// </summary>
    [SerializeField] protected int m_HitPoints;
    /// <summary>
    /// Текущие хитпоинты.
    /// </summary>
    [SerializeField] private int m_CurrentHitPoints;
    public int HitPoints => m_CurrentHitPoints;
    [SerializeField] private UnityEvent m_ChangeHitPoints;
    public UnityEvent ChangeHitPoints => m_ChangeHitPoints;
    #endregion

    #region Unity Events
    protected virtual void Start()
    {
        SetCurrentHitPoints(m_HitPoints);
        ChangeHitPoints.Invoke();
    }
    /// <summary>
    /// Переопределяемое событие уничтожения объекта, когда хитпоинты ниже или равны нулю.
    /// </summary>
    protected virtual void OnDeath()
    {
        if (m_Indestructible)
            return;

        m_EventOnDeath?.Invoke();
        Destroy(gameObject);
    }
    protected virtual void OnEnable()
    {
        if (AllDestructibles == null) 
            AllDestructibles = new HashSet<Destructible>();
        AllDestructibles.Add(this);
    }
    protected virtual void OnDestroy()
    {
        AllDestructibles.Remove(this);
        NumDestroyed++;
    }
    [SerializeField] private UnityEvent m_EventOnDeath;
    public UnityEvent EventOnDeath => m_EventOnDeath;
    #endregion

    #region Public API
    public void SetIndestructible(bool value)
    {
        m_Indestructible = value;
    }
    public void SetIndamageble(bool value)
    {
        m_Indamageble = value;
    }
    /// <summary>
    /// Назначение количества хитпоинтов.
    /// </summary>
    public void SetMaxHitPoints(int v_hitPoints)
    {
        if (v_hitPoints <= 0) 
            return;

        m_HitPoints = v_hitPoints;
    }
    public void SetCurrentHitPoints(int v_hitPoints)
    {
        if (v_hitPoints <= 0) 
            return;

        m_CurrentHitPoints = v_hitPoints;
    }
    /// <summary>
    /// Применение урона к объекту.
    /// </summary>
    /// <param name="damage"> Урон наносимый объекту.</param>
    public void ApplyDamage(int damage)
    {
        if (m_Indamageble) 
            return;

        m_CurrentHitPoints -= damage;

        if (m_CurrentHitPoints <= 0)
        {
            m_CurrentHitPoints = 0;
            OnDeath();
        }

        ChangeHitPoints.Invoke();

    }
    #endregion

    #region Statics
    public static HashSet<Destructible> AllDestructibles { get; private set; }
    public static int NumDestroyed { get; private set; }
    public static void ClearNumDestroyed()
    {
        NumDestroyed = 0;
    }
    #endregion
}