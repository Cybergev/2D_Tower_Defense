using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[CreateAssetMenu]
public class UnlockerAsset : ItemAsset
{
    public enum UnlockerType
    {
        Other,
        Tower,
        Player,
        Ability
    }
    [SerializeField] private UnlockerType unlockTarget;
    [SerializeField] private TowerAsset unlockTowerAsset;
    [SerializeField] private AbilityAsset unlockAbilityAsset;
    public UnlockerType UnlockTarget => unlockTarget;
    public TowerAsset UnlockTowerAsset => unlockTowerAsset;
    public AbilityAsset UnlockAbilityAsset => unlockAbilityAsset;

    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(UnlockerAsset))]
    public class UnlockerAssetInspector : Editor
    {
        private UnlockerAsset unlockerAsset;
        private void OnEnable()
        {
            unlockerAsset = (UnlockerAsset)target;
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(unlockerAsset.itemName)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(unlockerAsset.itemDescription)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(unlockerAsset.itemImage)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(unlockerAsset.cost)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(unlockerAsset.unlockTarget)));
            if (unlockerAsset.unlockTarget == UnlockerType.Other)
            {
            }
            if (unlockerAsset.unlockTarget == UnlockerType.Tower)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(unlockerAsset.unlockTowerAsset)));
            }
            if (unlockerAsset.unlockTarget == UnlockerType.Player)
            {
            }
            if (unlockerAsset.unlockTarget == UnlockerType.Ability)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(unlockerAsset.unlockAbilityAsset)));
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
    #endregion
}
