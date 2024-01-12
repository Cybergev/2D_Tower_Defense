using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProjectileProperties : ScriptableObject
{
    [SerializeField] private float m_Mass = 1;
    [SerializeField] private float m_LinearDrag;
    [SerializeField] private float m_AngularDrag;
    [SerializeField] private float m_GravityScale;
    [SerializeField] private float m_ThrustForce = 1;
    [SerializeField] private float m_MaxLinearVelocity = 10;
    [SerializeField] private bool m_IsHoming = false;
    [SerializeField] private float m_HomingAngle = 45;
    [SerializeField] private float m_ImpactCheckLineLenght;
    [SerializeField] private bool m_HasImpactForce = true;
    [SerializeField, Range(0.0f, 2.0f)] private float m_ImpactForceModifier = 1;
    public float Mass => m_Mass;
    public float LinearDrag => m_LinearDrag;
    public float AngularDrag => m_AngularDrag;
    public float GravityScale => m_GravityScale;
    public float ThrustForce => m_ThrustForce;
    public float MaxLinearVelocity => m_MaxLinearVelocity;
    public bool IsHoming => m_IsHoming;
    public float HomingAngle => m_HomingAngle;
    public float ImpactCheckLineLenght => m_ImpactCheckLineLenght;
    public bool HasImpactForce => m_HasImpactForce;
    public float ImpactForceModifier => m_ImpactForceModifier;


    [SerializeField] private int m_Damage;
    [SerializeField] private float m_Lifetime;
    public int Damage => m_Damage;
    public float Lifetime => m_Lifetime;


    [SerializeField] private bool m_CanBounce = false;
    [SerializeField] private int m_MaxBounceNum;
    [SerializeField] private int m_DamadeLossPerBounce;
    public bool CanBounce => m_CanBounce;
    public int MaxBounceNum => m_MaxBounceNum;
    public int DamadeLossPerBounce => m_DamadeLossPerBounce;
}