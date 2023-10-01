using UnityEngine;
using UnityEngine.UI;

public class LivesAmountUI : MonoBehaviour
{
    [SerializeField] private Text livesText;

    private void Start()
    {
        TDPlayer.LifeUpdateSubscribe(OnChangeLivesAmount);
    }
    private void OnDestroy()
    {
        TDPlayer.LifeUpdateDisscribe(OnChangeLivesAmount);
    }

    private void OnChangeLivesAmount(int value)
    {
        livesText.text = value.ToString();
    }
}