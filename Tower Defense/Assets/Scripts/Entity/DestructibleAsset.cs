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
            destructibleAsset.isIndestructible = EditorGUILayout.Toggle("Is Indestructible", destructibleAsset.isIndestructible);
            destructibleAsset.isIndamageble = EditorGUILayout.Toggle("Is Indamageble", destructibleAsset.isIndamageble);
            destructibleAsset.healthPointsIsRandom = EditorGUILayout.Toggle("Health Points Is Random", destructibleAsset.healthPointsIsRandom);
            if (!destructibleAsset.healthPointsIsRandom)
                destructibleAsset.healthPoints = EditorGUILayout.IntField("Health Points", destructibleAsset.healthPoints);
            else
                destructibleAsset.healthPointsRandomRange = EditorGUILayout.Vector2IntField("Health Points Random Range", destructibleAsset.healthPointsRandomRange);
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
