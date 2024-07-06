using UnityEngine;
[CreateAssetMenu]
public abstract class AbilityAsset : ScriptableObject
{
    public enum AbilityType
    {
        Other,
        Attack,
        Buff
    }
    [SerializeField] private AbilityType type;
    [SerializeField] private Sprite guiSprite;
    [SerializeField] private int mageCost;
    [SerializeField] private float reuseTime;
    public AbilityType Type => type;
    public Sprite GUISprite => guiSprite;
    public int MageCost => mageCost;
    public float ReuseTime => reuseTime;
}
