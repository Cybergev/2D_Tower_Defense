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
    private UpgradeAsset upgrade;

    #region
    private float mass => m_ProjectileProperties.Mass;
    private float linearDrag => m_ProjectileProperties.LinearDrag;
    private float angularDrag => m_ProjectileProperties.AngularDrag;
    private float gravityScale => m_ProjectileProperties.GravityScale;
    private float thrustForce => m_ProjectileProperties.ThrustForce;
    private float maxLinearVelocity => m_ProjectileProperties.MaxLinearVelocity;
    private bool isHoming => m_ProjectileProperties.IsHoming;
    private float homingAngle => m_ProjectileProperties.HomingAngle;
    private float impactCheckLineLenght => m_ProjectileProperties.ImpactCheckLineLenght;
    private bool hasImpactForce => m_ProjectileProperties.HasImpactForce;
    private float ImpactForceModifier => m_ProjectileProperties.ImpactForceModifier;
    private int damage => (int)(m_ProjectileProperties.Damage * (upgrade != null ? upgrade.DamageModifier : 1));
    private DamageType damageType => m_ProjectileProperties.DamageType;
    private float lifetime => m_ProjectileProperties.Lifetime;
    private bool canBounce => m_ProjectileProperties.CanBounce;
    private int maxBounceNum => m_ProjectileProperties.MaxBounceNum;
    private int damadeLossPerBounce => m_ProjectileProperties.DamadeLossPerBounce;
    #endregion

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

        m_Rigid.mass = mass;
        m_Rigid.angularDrag = angularDrag;
        m_Rigid.gravityScale = gravityScale;
        if (isHoming)
        {
            if (m_Target.TargetTransform)
                m_Rigid.AddTorque(CalculateAngle(m_Target.TargetTransform.position) * Time.fixedDeltaTime, ForceMode2D.Force);
            else
                Destroy(gameObject);
        }
        else
        {
            Vector2 stepLenght = (thrustForce / maxLinearVelocity) * Time.fixedDeltaTime * transform.up;
            m_Rigid.AddForce(stepLenght, ForceMode2D.Impulse);
        }

        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        if (isHoming)
        {
            if (m_Target.TargetTransform)
            {
                m_Rigid.AddTorque(CalculateAngle(m_Target.TargetTransform.position), ForceMode2D.Force);
                transform.position = Vector3.MoveTowards(transform.position, m_Target.TargetTransform.position, maxLinearVelocity * Time.fixedDeltaTime);
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
        if (rigid && hasImpactForce)
            rigid.AddForce(m_Rigid.mass * m_Rigid.velocity * ImpactForceModifier, ForceMode2D.Impulse);
        if (dest && dest != m_Parent)
        {
            if (m_Parent)
            {
                dest.GetComponent<SpaceShip>()?.SetLastDamger(m_Parent.gameObject);
            }
            if (dest is Enemy)
                (dest as Enemy).ApplyDamage(damage - m_lostDamge, damageType);
            else
                dest.ApplyDamage(damage - m_lostDamge);
        }
        if (canBounce && m_BounceNum < maxBounceNum)
        {
            m_BounceNum++;
            m_lostDamge -= damadeLossPerBounce;
        }
        else
            Destroy(gameObject);
        m_ImpactEffect.Invoke();
    }
    private float CalculateAngle(Vector3 targetPosition)
    {
        Vector2 localTargetPosition = transform.InverseTransformPoint(targetPosition);
        float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);
        angle = Mathf.Clamp(angle, -homingAngle, homingAngle) / homingAngle;
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
    public void SetUpgrade(UpgradeAsset asset)
    {
        if (asset == null)
            return;
        upgrade = asset;
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
