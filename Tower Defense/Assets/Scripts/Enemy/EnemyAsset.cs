using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public sealed class EnemyAsset : DestructibleAsset
{
    [Header("Damage Settings")]
    public bool damageIsRandom = false;
    public int damage;
    public Vector2Int damageRandomRange = new Vector2Int(1, 10);

    [Header("Gold Settings")]
    public bool goldIsRandom = false;
    public int gold;
    public Vector2Int goldRandomRange = new Vector2Int(1, 10);

    [Header("Score Settings")]
    public bool scoreIsRandom = false;
    public int score;
    public Vector2Int scoreRandomRange = new Vector2Int(1, 10);

    [Header("Color Settings")]
    public bool collorIsRandom = false;
    public Color color = Color.white;
    public Color[] colorsRandomArray;

    [Header("Scale Settings")]
    public bool scaleIsRandom = false;
    public Vector2 spriteScale = new Vector2(1, 1);
    public Vector2 scaleRandomRange = new Vector2(1, 10);

    [Header("Animation Settings")]
    public bool animationIsRandom = false;
    public RuntimeAnimatorController animation;
    public RuntimeAnimatorController[] animationsRandomArray;

    [Header("Move Settings")]
    public bool moveSpeedIsRandom = false;
    public float moveSpeed = 1;
    public Vector2 moveSpeedRandomRange = new Vector2(1, 10);
/*
#if UNITY_EDITOR
    [CustomEditor(typeof(EnemyAsset))]
    public class EnemyAssetInspector : Editor
    {
        private EnemyAsset enemyAsset;

        
        private void OnGUI()
        {
            EnemyAsset enemyAsset = (EnemyAsset)target;
        }

        public override void OnInspectorGUI()
        {
            enemyAsset.damageIsRandom = EditorGUILayout.Toggle(name, enemyAsset.damageIsRandom);
            if (!enemyAsset.damageIsRandom)
                enemyAsset.damage = EditorGUILayout.IntField(name, enemyAsset.damage);
            else
                enemyAsset.damageRandomRange = EditorGUILayout.Vector2IntField(name, enemyAsset.damageRandomRange);

            enemyAsset.goldIsRandom = EditorGUILayout.Toggle(name, enemyAsset.goldIsRandom);
            if (!enemyAsset.goldIsRandom)
                enemyAsset.gold = EditorGUILayout.IntField(name, enemyAsset.gold);
            else
                enemyAsset.goldRandomRange = EditorGUILayout.Vector2IntField(name, enemyAsset.goldRandomRange);

            enemyAsset.scoreIsRandom = EditorGUILayout.Toggle(name, enemyAsset.scoreIsRandom);
            if (!enemyAsset.scoreIsRandom)
                enemyAsset.score = EditorGUILayout.IntField(name, enemyAsset.score);
            else
                enemyAsset.scoreRandomRange = EditorGUILayout.Vector2IntField(name, enemyAsset.scoreRandomRange);

            enemyAsset.collorIsRandom = EditorGUILayout.Toggle(name, enemyAsset.collorIsRandom);
            if (!enemyAsset.collorIsRandom)
                enemyAsset.color = EditorGUILayout.ColorField(name, enemyAsset.color);

            enemyAsset.scaleIsRandom = EditorGUILayout.Toggle(name, enemyAsset.scaleIsRandom);
            if (!enemyAsset.scaleIsRandom)
                enemyAsset.spriteScale = EditorGUILayout.Vector2Field(name, enemyAsset.spriteScale);
            else
                enemyAsset.scaleRandomRange = EditorGUILayout.Vector2Field(name, enemyAsset.scaleRandomRange);

            enemyAsset.animationIsRandom = EditorGUILayout.Toggle(name, enemyAsset.animationIsRandom);

        }
    }
#endif
*/
}