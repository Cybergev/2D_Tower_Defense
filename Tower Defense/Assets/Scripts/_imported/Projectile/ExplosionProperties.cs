using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ExplosionProperties : ScriptableObject
{
    [SerializeField] private float m_ExplosionRadius;
    //[SerializeField] private bool m_HasImpactForce = true;
    //[SerializeField][Range(0.0f, 2.0f)] private float m_ImpactForceModifier = 1;
    public float ExplosionRadius => m_ExplosionRadius;
    //public bool HasImpactForce => m_HasImpactForce;
    //public float ImpactForceModifier => m_ImpactForceModifier;


    [SerializeField] private int m_Damage;
    [SerializeField] private float m_Lifetime;
    public int Damage => m_Damage;
    public float Lifetime => m_Lifetime;
}
