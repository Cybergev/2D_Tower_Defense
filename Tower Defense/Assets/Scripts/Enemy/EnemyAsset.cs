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
            enemyAsset.damageIsRandom = EditorGUILayout.Toggle("Damage Is Random", enemyAsset.damageIsRandom);
            if (!enemyAsset.damageIsRandom)
                enemyAsset.damage = EditorGUILayout.IntField("Damage", enemyAsset.damage);
            else
                enemyAsset.damageRandomRange = EditorGUILayout.Vector2IntField("Damage Random Range", enemyAsset.damageRandomRange);

            enemyAsset.goldIsRandom = EditorGUILayout.Toggle("Gold Is Random", enemyAsset.goldIsRandom);
            if (!enemyAsset.goldIsRandom)
                enemyAsset.gold = EditorGUILayout.IntField("Gold", enemyAsset.gold);
            else
                enemyAsset.goldRandomRange = EditorGUILayout.Vector2IntField("Gold Random Range", enemyAsset.goldRandomRange);

            enemyAsset.scoreIsRandom = EditorGUILayout.Toggle("Score Is Random", enemyAsset.scoreIsRandom);
            if (!enemyAsset.scoreIsRandom)
                enemyAsset.score = EditorGUILayout.IntField("Score", enemyAsset.score);
            else
                enemyAsset.scoreRandomRange = EditorGUILayout.Vector2IntField("Score Random Range", enemyAsset.scoreRandomRange);

            enemyAsset.collorIsRandom = EditorGUILayout.Toggle("Collor Is Random", enemyAsset.collorIsRandom);
            if (!enemyAsset.collorIsRandom)
                enemyAsset.color = EditorGUILayout.ColorField("Color", enemyAsset.color);

            enemyAsset.scaleIsRandom = EditorGUILayout.Toggle("Scale Is Random", enemyAsset.scaleIsRandom);
            if (!enemyAsset.scaleIsRandom)
                enemyAsset.spriteScale = EditorGUILayout.Vector2Field("Sprite Scale", enemyAsset.spriteScale);
            else
                enemyAsset.scaleRandomRange = EditorGUILayout.Vector2Field("Scale Random Range", enemyAsset.scaleRandomRange);

            enemyAsset.animationIsRandom = EditorGUILayout.Toggle("Animation Is Random", enemyAsset.animationIsRandom);
            if(!enemyAsset.animationIsRandom)
                enemyAsset.animation = EditorGUILayout.ObjectField("Animation", enemyAsset.animation, typeof(RuntimeAnimatorController), false) as RuntimeAnimatorController;
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}