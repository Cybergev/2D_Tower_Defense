using UnityEngine;

public class VisualEffectScript : MonoBehaviour
{
    [SerializeField] public GameObject ObjectPrefab;
    [SerializeField] public Transform EffectBaseScale;
    [SerializeField] public float EffectScaleModifier = 1;
    [SerializeField] public Transform EffectSpawnTarget;

    public void SpawnEffect()
    {
        GameObject gameObject = Instantiate(ObjectPrefab, EffectSpawnTarget.position, EffectSpawnTarget.rotation);
        gameObject.transform.localScale = EffectBaseScale.localScale * EffectScaleModifier;
    }
}