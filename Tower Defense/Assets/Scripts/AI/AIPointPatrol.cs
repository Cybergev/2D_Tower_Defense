using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPointPatrol : MonoBehaviour
{
    [SerializeField] private float m_Radius;
    public float Radius => m_Radius;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, m_Radius);
    }
}