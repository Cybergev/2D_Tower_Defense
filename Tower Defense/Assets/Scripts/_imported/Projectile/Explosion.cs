using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ExplosionProperties m_ExplosionProperties;
    [SerializeField] private UnityEvent m_ExplosionEffect;
    [SerializeField] private UnityEvent m_DestroyEffect;
    private void OnDestroy()
    {
        m_DestroyEffect.Invoke();
    }
    public void Explode()
    {
        foreach (var dest in Destructible.AllDestructibles)
        {
            float dist = (dest.transform.position - transform.position).magnitude;
            if (dist <= m_ExplosionProperties.ExplosionRadius)
                Impact(dest.gameObject, dist);
        }
        m_ExplosionEffect.Invoke();
    }
    private void Impact(GameObject v_object, float dist)
    {
        if (!v_object)
            return;
        var dest = v_object.transform.root.GetComponent<Destructible>();
        if (dest)
        {
            dest.ApplyDamage(m_ExplosionProperties.Damage);
            Vector2 force = (dest.transform.position - transform.position).normalized * m_ExplosionProperties.ImpactForce * (dist / m_ExplosionProperties.ExplosionRadius);
            dest.transform.root.GetComponent<Rigidbody2D>()?.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
