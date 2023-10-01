using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] private float m_LifeTime;
    private void Start()
    {
        Destroy(gameObject, m_LifeTime);
    }
}