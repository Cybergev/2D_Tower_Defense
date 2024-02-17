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
    [Header("Armor Settings")]
    [SerializeField] private bool physArmorIsRandom = false;
    [SerializeField] private bool piercArmorIsRandom = false;
    [SerializeField] private bool magicArmorIsRandom = false;
    [SerializeField] private int physArmor;
    [SerializeField] private int piercArmor;
    [SerializeField] private int magicArmor;
    [SerializeField] private Vector2Int physArmorRandomRange = new Vector2Int(1, 10);
    [SerializeField] private Vector2Int piercArmorRandomRange = new Vector2Int(1, 10);
    [SerializeField] private Vector2Int magicArmorRandomRange = new Vector2Int(1, 10);

    [Header("Damage Settings")]
    [SerializeField] private bool damageIsRandom = false;
    [SerializeField] private int damage;
    [SerializeField] private Vector2Int damageRandomRange = new Vector2Int(1, 10);

    [Header("Gold Settings")]
    [SerializeField] private bool goldIsRandom = false;
    [SerializeField] private int gold;
    [SerializeField] private Vector2Int goldRandomRange = new Vector2Int(1, 10);

    [Header("Score Settings")]
    [SerializeField] private bool scoreIsRandom = false;
    [SerializeField] private int score;
    [SerializeField] private Vector2Int scoreRandomRange = new Vector2Int(1, 10);

    [Header("Color Settings")]
    public bool colorIsRandom = false;
    [SerializeField] private Color color = Color.white;
    [SerializeField] private Color[] colorsRandomArray;

    [Header("Scale Settings")]
    public bool spriteScaleIsRandom = false;
    [SerializeField] private Vector2 spriteScale = new Vector2(1, 1);
    [SerializeField] private Vector2 spriteScaleRandomRange = new Vector2(1, 10);

    [Header("Animation Settings")]
    public bool animationIsRandom = false;
    [SerializeField] private RuntimeAnimatorController animation;
    [SerializeField] private RuntimeAnimatorController[] animationsRandomArray;

    [Header("Sound Settings")]
    public bool soundDieIsRandom = false;
    public bool soundWinIsRandom = false;
    [SerializeField] private Sound soundDie;
    [SerializeField] private Sound soundWin;
    [SerializeField] private Sound[] soundDieRandomArray;
    [SerializeField] private Sound[] soundWinRandomArray;

    [Header("Move Settings")]
    public bool moveSpeedIsRandom = false;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private Vector2 moveSpeedRandomRange = new Vector2(1, 10);
    #region Public
    public bool PhysArmorIsRandom => physArmorIsRandom;
    public int PhysArmor => physArmor;
    public Vector2Int PhysArmorRandomRange => physArmorRandomRange;
    public bool PiercArmorIsRandom => piercArmorIsRandom;
    public int PiercArmor => piercArmor;
    public Vector2Int PiercArmorRandomRange => piercArmorRandomRange;
    public bool MagicArmorIsRandom => magicArmorIsRandom;
    public int MagicArmor => magicArmor;
    public Vector2Int MagicArmorRandomRange => magicArmorRandomRange;
    public bool DamageIsRandom => damageIsRandom;
    public int Damage => damage;
    public Vector2Int DamageRandomRange => damageRandomRange;
    public bool GoldIsRandom => goldIsRandom;
    public int Gold => gold;
    public Vector2Int GoldRandomRange => goldRandomRange;
    public bool ScoreIsRandom => scoreIsRandom;
    public int Score => score;
    public Vector2Int ScoreRandomRange => scoreRandomRange;
    public bool ColorIsRandom => colorIsRandom;
    public Color Color => color;
    public Color[] ColorsRandomArray => colorsRandomArray;
    public bool ScaleIsRandom => spriteScaleIsRandom;
    public Vector2 SpriteScale => spriteScale;
    public Vector2 ScaleRandomRange => spriteScaleRandomRange;
    public bool AnimationsIsRandom => animationIsRandom;
    public RuntimeAnimatorController Animation => animation;
    public RuntimeAnimatorController[] AnimationsRandomArray => animationsRandomArray;
    public bool SoundDieIsRandom => soundDieIsRandom;
    public Sound SoundDie => soundDie;
    public Sound[] SoundDieRandomArray => soundDieRandomArray;
    public bool SoundWinIsRandom => soundWinIsRandom;
    public Sound SoundWin => soundWin;
    public Sound[] SoundWinRandomArray => soundWinRandomArray;
    public bool MoveSpeedIsRandom => moveSpeedIsRandom;
    public float MoveSpeed => moveSpeed;
    public Vector2 MoveSpeedRandomRange => moveSpeedRandomRange;
    #endregion

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
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.physArmorIsRandom)));
            EditorGUILayout.PropertyField(
                !enemyAsset.physArmorIsRandom ? serializedObject.FindProperty(nameof(enemyAsset.physArmor)) : serializedObject.FindProperty(nameof(enemyAsset.physArmorRandomRange))
            );

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.piercArmorIsRandom)));
            EditorGUILayout.PropertyField(
                !enemyAsset.piercArmorIsRandom ? serializedObject.FindProperty(nameof(enemyAsset.piercArmor)) : serializedObject.FindProperty(nameof(enemyAsset.piercArmorRandomRange))
            );

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.magicArmorIsRandom)));
            EditorGUILayout.PropertyField(
                !enemyAsset.magicArmorIsRandom ? serializedObject.FindProperty(nameof(enemyAsset.magicArmor)) : serializedObject.FindProperty(nameof(enemyAsset.magicArmorRandomRange))
            );

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.damageIsRandom)));
            EditorGUILayout.PropertyField(
                !enemyAsset.damageIsRandom ? serializedObject.FindProperty(nameof(enemyAsset.damage)) : serializedObject.FindProperty(nameof(enemyAsset.damageRandomRange))
            );

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.goldIsRandom)));
            EditorGUILayout.PropertyField(
                !enemyAsset.goldIsRandom ? serializedObject.FindProperty(nameof(enemyAsset.gold)) : serializedObject.FindProperty(nameof(enemyAsset.goldRandomRange))
            );

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.scoreIsRandom)));
            EditorGUILayout.PropertyField(
                !enemyAsset.scoreIsRandom ? serializedObject.FindProperty(nameof(enemyAsset.score)) : serializedObject.FindProperty(nameof(enemyAsset.scoreRandomRange))
            );

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.colorIsRandom)));
            EditorGUILayout.PropertyField(
                !enemyAsset.colorIsRandom ? serializedObject.FindProperty(nameof(enemyAsset.color)) : serializedObject.FindProperty(nameof(enemyAsset.colorIsRandom))
            );

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.spriteScaleIsRandom)));
            EditorGUILayout.PropertyField(
                !enemyAsset.spriteScaleIsRandom ? serializedObject.FindProperty(nameof(enemyAsset.spriteScale)) : serializedObject.FindProperty(nameof(enemyAsset.spriteScaleRandomRange))
            );

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.animationIsRandom)));
            EditorGUILayout.PropertyField(
                !enemyAsset.animationIsRandom ? serializedObject.FindProperty(nameof(enemyAsset.animation)) : serializedObject.FindProperty(nameof(enemyAsset.animationsRandomArray))
            );

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.soundDieIsRandom)));
            EditorGUILayout.PropertyField(
                !enemyAsset.soundDieIsRandom ? serializedObject.FindProperty(nameof(enemyAsset.soundDie)) : serializedObject.FindProperty(nameof(enemyAsset.soundDieRandomArray))
            );

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.soundWinIsRandom)));
            EditorGUILayout.PropertyField(
                !enemyAsset.soundWinIsRandom ? serializedObject.FindProperty(nameof(enemyAsset.soundWin)) : serializedObject.FindProperty(nameof(enemyAsset.soundWinRandomArray))
            );

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(enemyAsset.moveSpeedIsRandom)));
            EditorGUILayout.PropertyField(
                !enemyAsset.moveSpeedIsRandom ? serializedObject.FindProperty(nameof(enemyAsset.moveSpeed)) : serializedObject.FindProperty(nameof(enemyAsset.moveSpeedRandomRange))
            );
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
    #endregion
}