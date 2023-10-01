using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private class TurretTarget
    {
        public Transform targetTransform;
        public Rigidbody2D targetRigit;
    }

    [SerializeField] private float m_Radius = 1.0f;
    private Turret[] turrets;
    private TuerretProperties tuerretProperties;
    private TurretTarget turretTarget = new TurretTarget();



    private void Start()
    {
        if (turrets == null)
        {
            turrets = GetComponentsInChildren<Turret>();
            tuerretProperties = turrets[0].TuerretProperties;
        }
    }

    private void Update()
    {
        ActionFire();
    }
    private void ActionFire()
    {
        Vector3 Predction = GetPrediction();
        if (Predction != Vector3.zero)
            foreach (var turret in turrets)
            {
                turret.transform.up = Predction;
                turret.Fire(turretTarget.targetTransform);
            }
        else
            turretTarget = FindNewTarget(m_Radius);
    }
    private Vector3 GetPrediction()
    {
        if (turretTarget.targetTransform)
        {
            if ((turretTarget.targetTransform.position - transform.position).magnitude <= m_Radius)
            {
                return turretTarget.targetTransform.position - transform.position;
            }
            else
            {
                return Vector3.zero;
            }
        }
        else
        {
            return Vector3.zero;
        }
    }
    private TurretTarget FindNewTarget(float v_radius)
    {
        float minDist = m_Radius;
        TurretTarget turretTarget = new TurretTarget();
        foreach (var v in Destructible.AllDestructibles)
        {
            float dist = (v.transform.position - transform.position).magnitude;
            if (dist < m_Radius && dist < minDist)
            {
                minDist = dist;

                turretTarget.targetTransform = v.transform;
                turretTarget.targetRigit = v.GetComponent<Rigidbody2D>();
            }
            else
            {
                continue;
            }
        }
        return turretTarget;
    }
    public void SetRadius(float radius)
    {
        if (radius <= 0)
            return;
        m_Radius = radius;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, m_Radius);
    }
}
