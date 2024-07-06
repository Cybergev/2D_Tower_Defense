using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamgeZone : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float damageRadius;
    private void Start()
    {
        foreach (var dest in Destructible.AllDestructibles)
        {
            float dist = (dest.transform.position - transform.position).magnitude;
            if (dist <= damageRadius)
                dest.ApplyDamage(damage);
        }
        Destroy(gameObject);
    }
}
