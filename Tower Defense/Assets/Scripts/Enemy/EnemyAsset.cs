using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
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
#if UNITY_EDITOR
    [CustomEditor(typeof(EnemyAsset))]
    public class EnemyAssetInspector : Editor
    {
        private EnemyAsset enemyAsset;

        
        private void OnEnable()
        {
            enemyAsset = (EnemyAsset)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            enemyAsset.damageIsRandom = EditorGUILayout.Toggle(nameof(enemyAsset.damageIsRandom), enemyAsset.damageIsRandom);
            if (!enemyAsset.damageIsRandom)
                enemyAsset.damage = EditorGUILayout.IntField(nameof(enemyAsset.damage), enemyAsset.damage);
            else
                enemyAsset.damageRandomRange = EditorGUILayout.Vector2IntField(nameof(enemyAsset.damageRandomRange), enemyAsset.damageRandomRange);

            enemyAsset.goldIsRandom = EditorGUILayout.Toggle(nameof(enemyAsset.goldIsRandom), enemyAsset.goldIsRandom);
            if (!enemyAsset.goldIsRandom)
                enemyAsset.gold = EditorGUILayout.IntField(nameof(enemyAsset.gold), enemyAsset.gold);
            else
                enemyAsset.goldRandomRange = EditorGUILayout.Vector2IntField(nameof(enemyAsset.goldRandomRange), enemyAsset.goldRandomRange);

            enemyAsset.scoreIsRandom = EditorGUILayout.Toggle(nameof(enemyAsset.scoreIsRandom), enemyAsset.scoreIsRandom);
            if (!enemyAsset.scoreIsRandom)
                enemyAsset.score = EditorGUILayout.IntField(nameof(enemyAsset.score), enemyAsset.score);
            else
                enemyAsset.scoreRandomRange = EditorGUILayout.Vector2IntField(nameof(enemyAsset.scoreRandomRange), enemyAsset.scoreRandomRange);

            enemyAsset.collorIsRandom = EditorGUILayout.Toggle(nameof(enemyAsset.collorIsRandom), enemyAsset.collorIsRandom);
            if (!enemyAsset.collorIsRandom)
                enemyAsset.color = EditorGUILayout.ColorField(nameof(enemyAsset.color), enemyAsset.color);

            enemyAsset.scaleIsRandom = EditorGUILayout.Toggle(nameof(enemyAsset.scaleIsRandom), enemyAsset.scaleIsRandom);
            if (!enemyAsset.scaleIsRandom)
                enemyAsset.spriteScale = EditorGUILayout.Vector2Field(nameof(enemyAsset.spriteScale), enemyAsset.spriteScale);
            else
                enemyAsset.scaleRandomRange = EditorGUILayout.Vector2Field(nameof(enemyAsset.scaleRandomRange), enemyAsset.scaleRandomRange);

            enemyAsset.animationIsRandom = EditorGUILayout.Toggle(nameof(enemyAsset.animationIsRandom), enemyAsset.animationIsRandom);
            if(!enemyAsset.animationIsRandom)
                enemyAsset.animation = EditorGUILayout.ObjectField(nameof(enemyAsset.animation), enemyAsset.animation, typeof(RuntimeAnimatorController), false) as RuntimeAnimatorController;
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}