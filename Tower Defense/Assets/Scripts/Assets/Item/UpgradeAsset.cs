using UnityEngine;
using static AbilityAsset;

#if UNITY_EDITOR
using UnityEditor;
#endif
[CreateAssetMenu]
public class UpgradeAsset : ItemAsset
{
    #region Base
    public enum UpgradeType
    {
        Other,
        Tower,
        Player,
        Ability
    }
    [SerializeField] private UpgradeType upgradeTarget;
    [SerializeField] private int upgradeLevel;
    #region Public
    public UpgradeType UpgradeTarget => upgradeTarget;
    public int UpgradeLevel => upgradeLevel;
    #endregion

    #endregion

    #region Common Fields
    [SerializeField] private int glodCost;
    [SerializeField, Range(1.0f, 2.0f)] private float damageModifier = 1;
    #region Public
    public int GlodCost => glodCost;
    public float DamageModifier => damageModifier;
    #endregion

    #endregion

    #region Tower
    [SerializeField] private TowerAsset.TowerType towerUpgradeTarget;
    [SerializeField, Range(1.0f, 0.0f)] private float fireRateModifier = 1;
    [SerializeField, Range(1.0f, 2.0f)] private float fireRadiusModifier = 1;
    #region Public
    public TowerAsset.TowerType TowerUpgradeTarget => towerUpgradeTarget;
    public float FireRateModifier => fireRateModifier;
    public float FireRadiusModifier => fireRadiusModifier;
    #endregion

    #endregion

    #region Player
    public enum PlayerUpgrade
    {
        Live,
        Mage,
        Gold
    }
    [SerializeField] private PlayerUpgrade playerUpgradeTarget;
    [SerializeField, Range(10, 40)] private int liveUpgrade;
    [SerializeField, Range(10, 40)] private int mageUpgrade;
    [SerializeField, Range(10, 90)] private int goldUpgrade;
    #region Public
    public PlayerUpgrade PlayerUpgradeTaget => playerUpgradeTarget;
    public int LiveUpgrade => liveUpgrade;
    public int MageUpgrade => mageUpgrade;
    public int GoldUpgrade => goldUpgrade;
    #endregion

    #endregion

    #region Ability
    [SerializeField] private AbilityType abilityUpgradeTarget;
    [SerializeField, Range(1.0f, 0.1f)] private float reuseTimeModifier;
    [SerializeField, Range(1.0f, 2.0f)] private float BuffTimeModifier;
    #region Public
    public AbilityType AbilityUpgradeTarget => abilityUpgradeTarget;
    public float ReuseTimeModifier => reuseTimeModifier;
    public float TimeModifier => BuffTimeModifier;
    #endregion

    #endregion


    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(UpgradeAsset))]
    public class UpgradeAssetInspector : Editor
    {
        private UpgradeAsset upgradeAsset;
        private void OnEnable()
        {
            upgradeAsset = (UpgradeAsset)target;
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.itemName)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.itemDescription)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.itemImage)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.cost)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.upgradeTarget)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.upgradeLevel)));
            if (upgradeAsset.upgradeTarget == UpgradeType.Other)
            {
            }
            if (upgradeAsset.upgradeTarget == UpgradeType.Tower)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.towerUpgradeTarget)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.glodCost)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.damageModifier)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.fireRateModifier)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.fireRadiusModifier)));
            }
            if (upgradeAsset.upgradeTarget == UpgradeType.Player)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.playerUpgradeTarget)));
                if (upgradeAsset.PlayerUpgradeTaget == PlayerUpgrade.Live)
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.liveUpgrade)));
                if (upgradeAsset.PlayerUpgradeTaget == PlayerUpgrade.Gold)
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.goldUpgrade)));
            }
            if (upgradeAsset.upgradeTarget == UpgradeType.Ability)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.abilityUpgradeTarget)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.reuseTimeModifier)));
                if (upgradeAsset.AbilityUpgradeTarget == AbilityType.Other)
                {
                }
                if (upgradeAsset.AbilityUpgradeTarget == AbilityType.Attack)
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.damageModifier)));
                if (upgradeAsset.AbilityUpgradeTarget == AbilityType.Buff)
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.BuffTimeModifier)));
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
    #endregion
}
