using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class MarketItem : MonoBehaviour
{
    [SerializeField] private List<ItemAsset> items;
    [SerializeField] private Image UIImage;
    [SerializeField] private Text UIItemDescText;
    [SerializeField] private Button UIBuyButton;
    [SerializeField] private Text UIButtonText;
    [SerializeField] private Sprite UINullImage;
    private List<ItemAsset> currentItems;
    private void SetMarketItem(ItemAsset item)
    {
        switch (item)
        {
            case null:
                UIImage.overrideSprite = UINullImage;
                UIItemDescText.text = "Empty";
                UIBuyButton.interactable = false;
                UIButtonText.text = "Empty";
                break;
            default:
                bool isSold = false;
                for (int i = 0; i < MarketController.Instance.SoldItems.Count && !isSold; i++)
                {
                    var soldI = MarketController.Instance.SoldItems[i];
                    if (soldI != null && soldI.ItemName == item.ItemName)
                    {
                        isSold = true;
                        currentItems.Remove(item);
                    }
                }
                if (isSold)
                {
                    goto case null;
                }
                UIImage.overrideSprite = item.ItemImage;
                UIItemDescText.text = item.ItemDescription;
                UIBuyButton.interactable = MarketController.Instance.Money >= item.Cost;
                UIButtonText.text = item.Cost.ToString();
                break;
        }
    }
    public void MarketitemIni()
    {
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
        SetMarketItem(currentItems.Count > 0 && currentItems[0] != null ? currentItems[0] : null);
    }
    public void OnBuyButton()
    {
        MarketController.Instance.BuyItem(currentItems[0]);
        currentItems.RemoveAt(0);
        SetMarketItem(currentItems.Count > 0 && currentItems[0] != null ? currentItems[0] : null);
        if (UIItemDescText.text == "Empty" && currentItems.Count > 0 && currentItems[0] != null)
            SetMarketItem(currentItems[0]);
    }
}
