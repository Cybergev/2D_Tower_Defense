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
    private void Impact(GameObject obj, float dist, ExplosionProperties prop)
    {
        if (!obj)
            return;
        var dest = obj.transform.root.GetComponent<Destructible>();
        if (dest)
        {
            dest.ApplyDamage(prop.Damage);
            Vector2 force = (dest.transform.position - transform.position).normalized * prop.ImpactForce * (dist / prop.ExplosionRadius);
            dest.transform.root.GetComponent<Rigidbody2D>()?.AddForce(force, ForceMode2D.Impulse);
        }
    }
    public void Explode(ExplosionProperties prop)
    {
        foreach (var dest in Destructible.AllDestructibles)
        {
            float dist = (dest.transform.position - transform.position).magnitude;
            if (dist <= prop.ExplosionRadius)
                Impact(dest.gameObject, dist, prop);
        }
        SoundController.Instance.Play(prop.Sound);
        m_ExplosionEffect.Invoke();
    }
    public void Explode()
    {
        Explode(m_ExplosionProperties);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, (m_ExplosionProperties ? m_ExplosionProperties.ExplosionRadius : 0));
    }
}
