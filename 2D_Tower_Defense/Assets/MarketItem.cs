using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketItem : MonoBehaviour
{
    [SerializeField] private List<ItemAsset> items;
    [SerializeField] private Button UIBuyButton;
    [SerializeField] private Image UIImage;
    [SerializeField] private Text UIItemDescText;
    [SerializeField] private Text UICostText;
    [SerializeField] private Sprite UINullImage;
    [SerializeField] private string UICostTextPef = "Cost";

    private ItemAsset currentItem;
    private List<ItemAsset> currentItems;
    private void SetMarketItem(ItemAsset item)
    {
        switch (item)
        {
            case null:
                UIImage.overrideSprite = UINullImage;
                UIItemDescText.text = "Empty";
                UIBuyButton.interactable = false;
                UICostText.text = UICostTextPef + "Empty";
                currentItem = null;
                break;
            default:
                bool isSold = false;
                for (int i = 0; i < MarketController.Instance.SoldItems.Count && !isSold; i++)
                {
                    var soldI = MarketController.Instance.SoldItems[i];
                    if (soldI != null && soldI.ItemName == item.ItemName)
                    {
                        isSold = true;
                        currentItem = null;
                    }
                }
                if (isSold)
                {
                    goto case null;
                }
                UIImage.overrideSprite = item.ItemImage;
                UIItemDescText.text = item.ItemDescription;
                UIBuyButton.interactable = MarketController.Instance.Money >= item.Cost;
                UICostText.text = UICostTextPef + item.Cost.ToString();
                currentItem = item;
                break;
        }
    }
    private void UpdateItem()
    {
        for (int i = 0; i < currentItems.Count && currentItem == null; i++)
            SetMarketItem(currentItems[i]);
    }
    public void MoneyStatusChekc()
    {
        if (!currentItem)
            UIBuyButton.interactable = false;
        else
            UIBuyButton.interactable = MarketController.Instance.Money >= currentItem.Cost;
    }
    public void MoneyStatusChekc(int money)
    {
        if (!currentItem)
            UIBuyButton.interactable = false;
        else
            UIBuyButton.interactable = money >= currentItem.Cost;
    }
    public void MarketitemIni()
    {
        MarketController.Instance.MoneyUpdate.AddListener(MoneyStatusChekc);
        currentItems = new List<ItemAsset>();
        for (int i = 0; i < MarketController.Instance.SoldItems.Count; i++)
        {
            var soldItm = MarketController.Instance.SoldItems[i];
            for (int j = 0; j < items.Count; j++)
            {
                if (items[j].ItemName != soldItm.ItemName)
                    currentItems.Add(items[j]);
            }
        }
        UpdateItem();
    }
    public void OnBuyButton()
    {
        MarketController.Instance.BuyItem(currentItem);
        currentItem = null;
        UpdateItem();
    }
}
