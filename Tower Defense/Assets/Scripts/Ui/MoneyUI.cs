using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private Text moneyText;

    private void Start()
    {
        TDPlayer.GoldUpdateSubscribe(OnChangeMoneyAmount);
    }
    private void OnDestroy()
    {
        TDPlayer.GoldUpdateDiscribe(OnChangeMoneyAmount);
    }

    private void OnChangeMoneyAmount(int value)
    {
        moneyText.text = value.ToString();
    }
}