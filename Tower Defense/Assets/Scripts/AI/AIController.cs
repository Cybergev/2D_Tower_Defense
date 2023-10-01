using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpaceShip))]
public class AIController : MonoBehaviour
{

    public enum AIFireMode
    {
        All,
        Primary,
        Secondary
    }

    public enum AIBehaviour
    {
        Null,
        Patrol
    }

    [SerializeField] private AIFireMode m_AIFireMode;
    [SerializeField] private AIBehaviour m_AIBehaviour;
    [SerializeField] private AIPointPatrol m_PatrolPoint;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float m_NavigationLinear;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float m_NavigationAngular;
    [SerializeField] private float m_MaxAngle = 45.0f;
    [SerializeField] private float m_RandomSelectMovePointTime;
    [SerializeField] private float m_FindNewTargetTime;
    [SerializeField] private float m_ShootDelay;
    [SerializeField] private float m_ShootDistance;
    [SerializeField] private float m_EvadeRayLength;

    private SpaceShip m_SpaceShip;
    private Vector3 m_MovePosition;
    private Destructible m_SelectedTarget;

    private Timer m_RandomizeDirectionTimer;
    private Timer m_FireTimer;
    private Timer m_FindNewTargetTimer;

    private void Start()
    {
        m_SpaceShip = GetComponent<SpaceShip>();

        InitTimers();
    }

    private void Update()
    {
        UpdateTimers();

        UpdateAI();
    }

    private void UpdateAI()
    {
        if (m_AIBehaviour == AIBehaviour.Patrol)
        {
            UpdateBehaviourPatrol();
        }
    }

    private void UpdateBehaviourPatrol()
    {
        ActionFindNewMovePosition();
        ActionControlShip();
        ActionFindNewAttackTarget();
        ActionFire();
        ActionAwadeCollision();
    }

    private void ActionFindNewMovePosition()
    {
        if (m_AIBehaviour == AIBehaviour.Patrol)
        {
            if (m_SelectedTarget != null)
            {
                m_MovePosition = m_SelectedTarget.transform.position;
            }
            else
            {
                if (m_PatrolPoint != null)
                {
                    bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius;

                    if (isInsidePatrolZone == true)
                    {
                        GetNewPoint();
                    }
                    else
                    {
                        m_MovePosition = m_PatrolPoint.transform.position;
                    }
                }
            }
        }
    }

    protected virtual void GetNewPoint()
    {
        if (m_RandomizeDirectionTimer.IsFinished == true)
        {
            Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoint.Radius + m_PatrolPoint.transform.position;
            m_MovePosition = newPoint;
            m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
        }
    }

    private void ActionAwadeCollision()
    {
        if (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength) == true)
        {
            m_MovePosition = transform.position + transform.right * 100.0f;
        }
    }

    private void ActionControlShip()
    {
        m_SpaceShip.ThrustControl = m_NavigationLinear;
        m_SpaceShip.TorqueControl = ComputeAlinginTorqueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
    }

    private float ComputeAlinginTorqueNormalized(Vector3 targetPositin, Transform ship)
    {
        Vector2 localTargetPosition = ship.InverseTransformPoint(targetPositin);
        float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);
        angle = Mathf.Clamp(angle, -m_MaxAngle, m_MaxAngle) / m_MaxAngle;
        return -angle;
    }

    private void ActionFindNewAttackTarget()
    {
        if (m_FindNewTargetTimer.IsFinished == true)
        {
            m_SelectedTarget = FindNearestDestructibleTarget();
            m_FindNewTargetTimer.Start(m_FindNewTargetTime);
        }
    }

    private SpaceShip ActionCheeckTarget(GameObject v_gameObject)
    {
        SpaceShip target = v_gameObject.transform.root.GetComponent<SpaceShip>();

        if (target != null && target.TeamId != SpaceShip.TeamIdNeutral && target.TeamId != m_SpaceShip.TeamId)
        {
            return target;
        }
        else
        {
            target = null;
        }
        return target;
    }

    private void ActionFire()
    {
        if (m_SelectedTarget != null)
        {
            if (m_FireTimer.IsFinished == true)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, m_ShootDistance);
                if (hit)
                {
                    Destructible target = ActionCheeckTarget(hit.collider.gameObject);

                    if (target != null)
                    {
                        m_SelectedTarget = target;
                        if (m_AIFireMode == AIFireMode.All || m_AIFireMode == AIFireMode.Primary) m_SpaceShip.Fire(TurretMode.Primary);
                        if (m_AIFireMode == AIFireMode.All || m_AIFireMode == AIFireMode.Secondary) m_SpaceShip.Fire(TurretMode.Secondary);
                        m_FireTimer.Start(m_ShootDelay);
                    }
                }
            }
        }
    }

    private Destructible FindNearestDestructibleTarget()
    {
        float maxDist = float.MaxValue;

        Destructible potentialTarget = null;

        foreach (var v in Destructible.AllDestructibles)
        {
            if (ActionCheeckTarget(v.gameObject) != null)
            {
                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                }
            }
            else
            {
                continue;
            }
        }
        return potentialTarget;
    }

    #region Timers

    public void InitTimers()
    {
        m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
        m_FireTimer = new Timer(m_ShootDelay);
        m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
    }

    public void UpdateTimers()
    {
        m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
        m_FireTimer.RemoveTime(Time.deltaTime);
        m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
    }

    public void SetPatrolBehaviour(AIPointPatrol point)
    {
        m_AIBehaviour = AIBehaviour.Patrol;
        m_PatrolPoint = point;
    }

    #endregion
}