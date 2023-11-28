using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ExplosionProperties : ScriptableObject
{
    [SerializeField] private int m_Damage;
    [SerializeField] private float m_ImpactForce;
    [SerializeField] private float m_ExplosionRadius;
    public int Damage => m_Damage;
    public float ImpactForce => m_ImpactForce;
    public float ExplosionRadius => m_ExplosionRadius;
}
