using UnityEngine;
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
        Player
    }
    [SerializeField] private UpgradeType type;
    [SerializeField] private int upgradeLevel;
    #region Public
    public UpgradeType Type => type;
    public int UpgradeLevel => upgradeLevel;
    #endregion

    #endregion

    #region Tower
    [SerializeField] private int glodCost;
    [SerializeField] private TowerAsset.TowerType towerUpgradeTarget;
    [SerializeField, Range(1.0f, 2.0f)] private float damageModifier = 1;
    [SerializeField, Range(1.0f, 0.0f)] private float fireRateModifier = 1;
    [SerializeField, Range(1.0f, 2.0f)] private float fireRadiusModifier = 1;
    #region Public
    public int GlodCost => glodCost;
    public TowerAsset.TowerType TowerUpgradeTarget => towerUpgradeTarget;
    public float DamageModifier => damageModifier;
    public float FireRateModifier => fireRateModifier;
    public float FireRadiusModifier => fireRadiusModifier;
    #endregion

    #endregion

    #region Player
    public enum PlayerUpgrade
    {
        Live,
        Gold
    }
    [SerializeField] private PlayerUpgrade playerUpgradeTarget;
    [SerializeField, Range(10, 40)] private int liveUpgrade;
    [SerializeField, Range(10, 90)] private int goldUpgrade;
    #region Public
    public PlayerUpgrade PlayerUpgradeTaget => playerUpgradeTarget;
    public int LiveUpgrade => liveUpgrade;
    public int GoldUpgrade => goldUpgrade;
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
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.type)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.upgradeLevel)));
            if (upgradeAsset.type == UpgradeType.Other)
            {
            }
            if (upgradeAsset.type == UpgradeType.Tower)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.glodCost)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.towerUpgradeTarget)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.damageModifier)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.fireRateModifier)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.fireRadiusModifier)));
            }
            if (upgradeAsset.type == UpgradeType.Player)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.playerUpgradeTarget)));
                if (upgradeAsset.PlayerUpgradeTaget == PlayerUpgrade.Live)
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.liveUpgrade)));
                if (upgradeAsset.PlayerUpgradeTaget == PlayerUpgrade.Gold)
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(upgradeAsset.goldUpgrade)));
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
    #endregion
}
