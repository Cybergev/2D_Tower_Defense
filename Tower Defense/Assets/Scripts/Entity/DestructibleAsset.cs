using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class DestructibleAsset : ScriptableObject
{
    [Header("Health Settings")]
    [SerializeField] private bool isIndestructible = false;
    [SerializeField] private bool isIndamageble = false;
    [SerializeField] private bool healthPointsIsRandom = false;
    [SerializeField] private int healthPoints = 1;
    [SerializeField] private Vector2Int healthPointsRandomRange = new Vector2Int(1, 10);
    #region Public
    public bool IsIndestructible => isIndestructible;
    public bool IsIndamageble => isIndamageble;
    public bool HealthPointsIsRandom => healthPointsIsRandom;
    public int HealthPoints => healthPoints;
    public Vector2Int HealthPointsRandomRange => healthPointsRandomRange;
    #endregion

    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(DestructibleAsset))]
    public class DestructibleAssetInspector : Editor
    {
        private DestructibleAsset destructibleAsset;

        private void OnEnable()
        {
            destructibleAsset = (DestructibleAsset)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(destructibleAsset.isIndestructible)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(destructibleAsset.isIndamageble)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(destructibleAsset.healthPointsIsRandom)));
            EditorGUILayout.PropertyField(
                !destructibleAsset.healthPointsIsRandom ? serializedObject.FindProperty(nameof(destructibleAsset.healthPoints)) : serializedObject.FindProperty(nameof(destructibleAsset.healthPointsRandomRange))
            );
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
    #endregion
}
