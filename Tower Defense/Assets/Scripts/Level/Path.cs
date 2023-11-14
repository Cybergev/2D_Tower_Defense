using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public enum PathTypes { Primary, Secondary }
    [SerializeField] private PathTypes pathType;
    [SerializeField] private AIPointPatrol[] points;

    public PathTypes PathType => pathType;
    public int Length => points.Length;


    public AIPointPatrol this[int i] { get => points[i]; }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        foreach(var point in points)
            Gizmos.DrawSphere(point.transform.position, point.Radius);
    }
}
