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
    #region Editor
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
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.damageIsRandom)));
            if (!enemyAsset.damageIsRandom)
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.damage)));
            else
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.damageRandomRange)));

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.goldIsRandom)));
            if (!enemyAsset.goldIsRandom)
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.gold)));
            else
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.goldRandomRange)));

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.scoreIsRandom)));
            if (!enemyAsset.scoreIsRandom)
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.score)));
            else
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.scoreRandomRange)));

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.collorIsRandom)));
            if (!enemyAsset.collorIsRandom)
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.color)));
            else
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.collorIsRandom)));

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.scaleIsRandom)));
            if (!enemyAsset.scaleIsRandom)
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.spriteScale)));
            else
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.scaleRandomRange)));

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.animationIsRandom)));
            if (!enemyAsset.animationIsRandom)
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.animation)));
            else
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.animationsRandomArray)));
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
    #endregion
}