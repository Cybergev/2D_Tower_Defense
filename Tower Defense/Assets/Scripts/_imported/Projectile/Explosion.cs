using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ExplosionProperties m_ExplosionProperties;
    [SerializeField] private UnityEvent m_ImpactEffect;
    [SerializeField] private UnityEvent m_DestroyEffect;
    private void Start()
    {
        Destroy(gameObject, m_ExplosionProperties.Lifetime);
        m_ImpactEffect.Invoke();
        CheckCollision();
    }
    private void OnDestroy()
    {
        m_DestroyEffect.Invoke();
    }
    private void CheckCollision()
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, m_ExplosionProperties.ExplosionRadius, transform.forward);

        foreach(var v in hit)
        {
            if (v.collider)
            {
                Impact(v.collider.gameObject);
                Debug.Log(v.transform.gameObject.name);
            }
            else
                continue;
        }
    }

    private void Impact(GameObject v_object)
    {
        if (!v_object)
            return;
        var dest = v_object.transform.root.GetComponent<Destructible>();
        if (dest)
            dest.ApplyDamage(m_ExplosionProperties.Damage);
        /*
        if (m_ExplosionProperties.HasImpactForce)
            dest.transform.root.GetComponent<Rigidbody2D>()?.AddForceAtPosition((m_Rigid.mass * m_Rigid.velocity) * m_ExplosionProperties.ImpactForceModifier, transform.position);
        */
    }
}
