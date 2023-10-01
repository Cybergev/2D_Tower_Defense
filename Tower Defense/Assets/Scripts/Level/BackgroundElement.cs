using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BackgroundElement : MonoBehaviour
{
    [SerializeField] private float m_ParalaxPower;

    [SerializeField] private float m_TextureScale;

    private Material m_QadMaterial;
    private Vector2 m_InitialOffset;

    private void Start()
    {
        m_QadMaterial = GetComponent<MeshRenderer>().material;
        m_InitialOffset = Random.insideUnitCircle;
    }

    private void Update()
    {
        Vector2 offset = m_InitialOffset;

        offset.x += transform.position.x / transform.localScale.x / m_ParalaxPower;
        offset.y += transform.position.y / transform.localScale.y / m_ParalaxPower;

        m_QadMaterial.mainTextureOffset = offset;
        m_QadMaterial.mainTextureScale = Vector2.one * m_TextureScale;
    }
}