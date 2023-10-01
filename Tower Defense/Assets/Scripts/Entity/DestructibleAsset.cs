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
            EnemyAsset destructibleAsset = (EnemyAsset)target;
        }

        public override void OnInspectorGUI()
        {
            destructibleAsset.isIndestructible = EditorGUILayout.Toggle(name, destructibleAsset.isIndestructible);
            destructibleAsset.isIndamageble = EditorGUILayout.Toggle(name, destructibleAsset.isIndamageble);
            destructibleAsset.healthPointsIsRandom = EditorGUILayout.Toggle(name, destructibleAsset.healthPointsIsRandom);
            if (!destructibleAsset.healthPointsIsRandom)
                destructibleAsset.healthPoints = EditorGUILayout.IntField(name, destructibleAsset.healthPoints);
            else
                destructibleAsset.healthPointsRandomRange = EditorGUILayout.Vector2IntField(name, destructibleAsset.healthPointsRandomRange);
        }
    }
#endif
}
