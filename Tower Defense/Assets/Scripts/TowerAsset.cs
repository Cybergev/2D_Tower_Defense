using UnityEngine;

[CreateAssetMenu]
public class TowerAsset : ScriptableObject
{
    public enum TowerType
    {
        Archer,
        PiercingArcher,
        Mage
    }
    [SerializeField] private TowerType type;
    [SerializeField] private BuildSite.BuildType buildType;
    [SerializeField] private Tower towerPref;
    [SerializeField] private float towerRadius;
    [SerializeField] private TuerretProperties tuerretProperties;

    [SerializeField] private Sprite towerSprite;
    [SerializeField] private Sprite GuiSprite;
    [SerializeField] private int glodCost = 15;

    public TowerType Type => type;
    public BuildSite.BuildType BuildType => buildType;
    public Tower TowerPref => towerPref;
    public float TowerRadius => towerRadius;
    public TuerretProperties TuerretProperties => tuerretProperties;

    public Sprite TowerSprite => towerSprite;
    public Sprite GUISprite => GuiSprite;
    public int GlodCost => glodCost;
}