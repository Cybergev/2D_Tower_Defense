using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileProperties m_ProjectileProperties;

    [SerializeField] private Rigidbody2D m_Rigid;
    private int m_BounceNum;
    private int m_lostDamge;
    private class ProjectileTarget
    {
        private Transform targetTransform;
        private Rigidbody2D targetRigid;

        public Transform TargetTransform => targetTransform;
        public Rigidbody2D TargetRigid => targetRigid;

        public ProjectileTarget(Transform transform)
        {
            if (!transform)
                return;
            targetTransform = transform;
        }
        public ProjectileTarget(Transform transform, Rigidbody2D rigid) : this(transform)
        {
            if (!transform)
                return;
            if (rigid)
                targetRigid = rigid;
        }
    }
    private ProjectileTarget m_Target;
    private Destructible m_Parent;


    [SerializeField] private UnityEvent m_ImpactEffect;
    [SerializeField] private UnityEvent m_DestroyEffect;

    private void Start()
    {
        if (!m_Rigid)
            transform.root.GetComponent<Rigidbody2D>();

        m_Rigid.mass = m_ProjectileProperties.Mass;
        m_Rigid.angularDrag = m_ProjectileProperties.AngularDrag;
        m_Rigid.gravityScale = m_ProjectileProperties.GravityScale;
        if (m_ProjectileProperties.IsHoming)
        {
            if (m_Target.TargetTransform)
                m_Rigid.AddTorque(CalculateAngle(m_Target.TargetTransform.position) * Time.fixedDeltaTime, ForceMode2D.Force);
            else
                Destroy(gameObject);
        }
        else
        {
            Vector2 stepLenght = (m_ProjectileProperties.ThrustForce / m_ProjectileProperties.MaxLinearVelocity) * Time.fixedDeltaTime * transform.up;
            m_Rigid.AddForce(stepLenght, ForceMode2D.Impulse);
        }

        Destroy(gameObject, m_ProjectileProperties.Lifetime);
    }

    private void FixedUpdate()
    {
        if(m_ProjectileProperties.IsHoming)
        {
            if (m_Target.TargetTransform)
            {
                m_Rigid.AddTorque(CalculateAngle(m_Target.TargetTransform.position), ForceMode2D.Force);
                transform.position = Vector3.MoveTowards(transform.position, m_Target.TargetTransform.position, m_ProjectileProperties.MaxLinearVelocity * Time.fixedDeltaTime);
            }
            else
                Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        m_DestroyEffect.Invoke();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.root.gameObject)
            return;
        Impact(collision.transform.root.gameObject);
    }
    private void Impact(GameObject v_object)
    {
        if (!v_object)
            return;
        var dest = v_object.transform.root.GetComponent<Destructible>();
        var rigid = v_object.transform.root.GetComponent<Rigidbody2D>();
        if (rigid && m_ProjectileProperties.HasImpactForce)
            rigid.AddForce(m_Rigid.mass * m_Rigid.velocity * m_ProjectileProperties.ImpactForceModifier, ForceMode2D.Impulse);
        if (dest && dest != m_Parent)
        {
            if (m_Parent)
            {
                dest.GetComponent<SpaceShip>()?.SetLastDamger(m_Parent.gameObject);
            }
            dest.ApplyDamage(m_ProjectileProperties.Damage - m_lostDamge);
        }
        if (m_ProjectileProperties.CanBounce && m_BounceNum < m_ProjectileProperties.MaxBounceNum)
        {
            m_BounceNum++;
            m_lostDamge -= m_ProjectileProperties.DamadeLossPerBounce;
        }
        else
            Destroy(gameObject);
        m_ImpactEffect.Invoke();
    }
    private float CalculateAngle(Vector3 targetPosition)
    {
        Vector2 localTargetPosition = transform.InverseTransformPoint(targetPosition);
        float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);
        angle = Mathf.Clamp(angle, -m_ProjectileProperties.HomingAngle, m_ProjectileProperties.HomingAngle) / m_ProjectileProperties.HomingAngle;
        return -angle;
    }
    private Vector3 CalculateLead()
    {
        Vector2 xk0 = m_Target.TargetTransform.position;
        Vector2 vk = m_Target.TargetRigid.velocity;

        Vector2 xs0 = transform.position;
        Vector2 vs = m_Rigid.velocity;

        float t = (xk0.magnitude - xs0.magnitude) / (vs.magnitude - vk.magnitude);
        Vector3 X = (xk0 + vk * t) + (xs0 + vs * t);


        return X;
    }
    public void SetTarget(Transform target)
    {
        m_Target = new ProjectileTarget(target, target.GetComponent<Rigidbody2D>());
    }
    public void SetParentShooter(Destructible parent)
    {
        m_Parent = parent;
    }
    public void SetDestructible(Destructible parent)
    {

    }
}
