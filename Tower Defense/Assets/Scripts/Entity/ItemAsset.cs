using UnityEngine;
[CreateAssetMenu]
public class ItemAsset : ScriptableObject
{
    [SerializeField] protected string itemName;
    [SerializeField] protected string itemDescription;
    [SerializeField] protected Sprite itemImage;
    [SerializeField] protected int cost;

    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    public Sprite ItemImage => itemImage;
    public int Cost => cost;
}