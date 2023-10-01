using UnityEngine;

public class LevelBoundary : MonoSingleton<LevelBoundary>
{
    [SerializeField] private float m_Radius;
    public float Radius => m_Radius;

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, m_Radius);
    }
#endif
}