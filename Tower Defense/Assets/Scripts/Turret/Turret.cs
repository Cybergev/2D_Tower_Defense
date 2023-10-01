using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private TurretMode m_Mode;
    public TurretMode Mode => m_Mode;

    [SerializeField] private TuerretProperties m_TuerretProperties;
    public TuerretProperties TuerretProperties => m_TuerretProperties;

    private float m_RefireTimer;

    public bool CanFire => m_RefireTimer <= 0;

    [SerializeField] private SpaceShip m_Ship;

    private void Start()
    {
        if (m_Ship != null)
            m_Ship = transform.root.GetComponent<SpaceShip>();
    }

    private void Update()
    {
        if (!CanFire)
            m_RefireTimer -= Time.deltaTime;
    }

    public void Fire(Transform target)
    {
        if (!m_TuerretProperties || !CanFire)
            return;

        if (m_Ship)
        {
            if (!m_Ship.DrawEnergy(m_TuerretProperties.EnergyUsage) || !m_Ship.DrawAmmo(m_TuerretProperties.AmmoUsage))
                return;
        }

        Projectile projectile = Instantiate(m_TuerretProperties.PerojectilePrefab).GetComponent<Projectile>();
        projectile.transform.position = transform.position;
        projectile.transform.rotation = transform.rotation;
        projectile.SetTarget(target);
        projectile.SetParentShooter(m_Ship);

        m_RefireTimer = m_TuerretProperties.RateOfFire;
    }

    public void AssignLoadut(TuerretProperties props)
    {
        if (m_Mode != props.Mode) return;

        m_RefireTimer = 0;
        m_TuerretProperties = props;
    }
}
