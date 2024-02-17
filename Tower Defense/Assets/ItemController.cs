using SaveSystem;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoSingleton<ItemController>
{
    [SerializeField] private ItemAsset[] baseItemAssets;

    [SerializeField] private ItemAsset[] allItemAssets;
    public ItemAsset[] AllItemAssets => allItemAssets;

    private List<ItemAsset> items;
    public List<ItemAsset> Items => items;

    protected override void Awake()
    {
        base.Awake();
        LoadItems();
    }
    public void AddItem(string itemName)
    {
        for (int i = 0; i < allItemAssets.Length; i++)
        {
            var item = allItemAssets[i];
            if (item.name == itemName)
                items.Add(item);
        }
        SaveItems();
    }
    public void AddItems(string[] itemNames)
    {
        for (int i = 0; i < itemNames.Length; i++)
        {
            var name = itemNames[i];
            for (int j = 0; j < allItemAssets.Length; i++)
            {
                var item = items[i];
                if (item.ItemName == name)
                    items.Add(item);
            }
        }
    }
    private void AddUpgradeItem(UpgradeAsset asset)
    {
        if (asset == null)
            return;
        for(int i = 0; i < items.Count; i++)
        {
            var item = items[i] as UpgradeAsset;
            if (item == null)
                continue;
            if(item.ItemName == asset.ItemName && item.UpgradeLevel <= asset.UpgradeLevel)
                items[i] = asset;
            if (i == items.Count)
                items.Add(item);
        }
    }
    private void SaveItems()
    {
        List<string> itemNames = new List<string>();
        foreach (var item in Items)
            itemNames.Add(item.ItemName);
        SaveController<string[]>.Save(nameof(Items), itemNames.ToArray());
    }
    private void LoadItems()
    {
        items = new List<ItemAsset>();
        string[] itemNames = SaveController<string[]>.TryLoad(nameof(Items));
        if (itemNames != null)
            SaveItems();
        else
            foreach (var name in itemNames)
                foreach (var item in AllItemAssets)
                    if (name == item.ItemName)
                        Items.Add(item);
    }
    public void ClearItems()
    {
        items = new List<ItemAsset>(baseItemAssets);
        SaveItems();
    }
}
