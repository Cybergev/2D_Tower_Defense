using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(TDPatrolController))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private string m_visualObjectName = "Visual";
    [Header("")]
    [SerializeField] private int m_damage = 1;
    [SerializeField] private int m_gold = 1;
    public void UseAsset(EnemyAsset asset)
    {
        var sprite = transform.Find(m_visualObjectName).GetComponent<SpriteRenderer>();

        if (asset.collorIsRandom)
            sprite.color = asset.colorsRandomArray[Random.Range(0, asset.colorsRandomArray.Length)];
        else
            sprite.color = asset.color;

        if (asset.scaleIsRandom)
        {
            float size = Random.Range(asset.scaleRandomRange.x, asset.scaleRandomRange.y);
            sprite.transform.localScale = new Vector2(size, size);
        }
        else
            sprite.transform.localScale = asset.spriteScale;

        if (asset.animationIsRandom)
            sprite.GetComponent<Animator>().runtimeAnimatorController = asset.animationsRandomArray[Random.Range(0, asset.animationsRandomArray.Length)];
        else
            sprite.GetComponent<Animator>().runtimeAnimatorController = asset.animation;

        m_damage = asset.damage;
        m_gold = asset.gold;

        GetComponent<SpaceShip>().UseAsset(asset);
    }
    public void DamagePlayer()
    {
        TDPlayer.Instance.TakeDamage(m_damage);
    }
    public void GivePlayerGold()
    {
        TDPlayer.Instance.ChangeGold(m_gold);
    }
    private void OnDestroy()
    {
        TDPlayer.Instance.AddKill();
    }
    #if UNITY_EDITOR
    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector: Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EnemyAsset a = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;
            if (a)
            {
                (target as Enemy).UseAsset(a);
            }
        }
    }
    #endif
}
