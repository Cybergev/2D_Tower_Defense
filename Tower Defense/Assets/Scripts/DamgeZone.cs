using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamgeZone : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float damageRate;

    private Destructible destructible;
    private Timer m_Timer;

    private void Update()
    {
        if (!destructible) 
            return;
        if (!m_Timer.IsFinished)
            m_Timer.RemoveTime(Time.deltaTime);
        if (destructible)
        {
            destructible.ApplyDamage(damage);
            m_Timer = new Timer(damageRate);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        destructible = collision.transform.root.GetComponent<Destructible>();
        m_Timer = new Timer(damageRate);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.root.GetComponent<Destructible>() == destructible) 
            destructible = null;
    }
}
