using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MarketController : MonoSingleton<MarketController>
{
    [SerializeField] private MarketItem[] marketItems;
    [SerializeField] private UnityEvent<int> moneyUpdate;
    [HideInInspector] public UnityEvent<int> MoneyUpdate => moneyUpdate;
    private List<ItemAsset> soldItems = new List<ItemAsset>();
    public List<ItemAsset> SoldItems => soldItems;
    private int money;
    public int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            moneyUpdate.Invoke(Money);
        }
    }
    private void OnEnable()
    {
        marketIni();
        if (marketItems.Length != 0)
            foreach (var item in marketItems)
            {
                item.MarketitemIni();
                item.gameObject.SetActive(true);
            }
    }
    private void OnDisable()
    {
        marketIni();
        if (marketItems.Length != 0)
            foreach (var item in marketItems)
                item.gameObject.SetActive(false);
    }
    private void Start()
    {
        marketIni();
        gameObject.SetActive(false);
    }
    private void marketIni()
    {
        if (marketItems.Length == 0)
        {
            var items = GetComponentsInChildren<MarketItem>();
            if (items != null)
            {
                marketItems = new MarketItem[items.Length];
                marketItems = items;
            }
        }
        Money = 0;
        soldItems = new List<ItemAsset>();
        foreach (var result in LevelResultController.Instance.ArrayLevelResults)
            Money += result.LevelReward > 0 ? result.LevelReward : 0;
        foreach (var item in ItemController.Instance.Items)
        {
            Money -= item.Cost;
            soldItems.Add(item);
        }
    }
    public void BuyItem(ItemAsset item)
    {
        if (Money < item.Cost)
            return;
        Money -= item.Cost;
        soldItems.Add(item);
        foreach (var itemAsset in ItemController.Instance.AllItemAssets)
        {
            if (itemAsset.ItemName == item.ItemName)
                ItemController.Instance.AddItem(itemAsset.ItemName);
        }
    }
}
