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
    public bool isIndestructible = false;
    public bool isIndamageble = false;
    public bool healthPointsIsRandom = false;
    public int healthPoints = 1;
    public Vector2Int healthPointsRandomRange = new Vector2Int(1, 10);
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
            destructibleAsset.isIndestructible = EditorGUILayout.Toggle(nameof(destructibleAsset.isIndestructible), destructibleAsset.isIndestructible);
            destructibleAsset.isIndamageble = EditorGUILayout.Toggle(nameof(destructibleAsset.isIndamageble), destructibleAsset.isIndamageble);
            destructibleAsset.healthPointsIsRandom = EditorGUILayout.Toggle(nameof(destructibleAsset.healthPointsIsRandom), destructibleAsset.healthPointsIsRandom);
            if (!destructibleAsset.healthPointsIsRandom)
                destructibleAsset.healthPoints = EditorGUILayout.IntField(nameof(destructibleAsset.healthPoints), destructibleAsset.healthPoints);
            else
                destructibleAsset.healthPointsRandomRange = EditorGUILayout.Vector2IntField(nameof(destructibleAsset.healthPointsRandomRange), destructibleAsset.healthPointsRandomRange);
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
