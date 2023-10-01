using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuyControl : MonoBehaviour
{
    [SerializeField] private TowerAsset m_towerAsset;
    [SerializeField] private Button m_button;
    [SerializeField] private Text m_text;
    [SerializeField] private Transform m_buildSiteTransfom;
    public void SetBuildSite(Transform value)
    {
        m_buildSiteTransfom = value;
    }

    private void Awake()
    {
    }
    private void Start()
    {
        TDPlayer.GoldUpdateSubscribe(GoldStatusCheck);
        m_text.text = m_towerAsset.glodCost.ToString();
        m_button.GetComponent<Image>().sprite = m_towerAsset.GUISprite;
    }
    private void GoldStatusCheck(int value)
    {
        if(value > m_towerAsset.glodCost != m_button.interactable)
        {
            m_button.interactable = !m_button.interactable;
            m_text.color = m_button.interactable ? Color.white : Color.red;
        }
    }
    public void Buy()
    {
        TDPlayer.Instance.TryBuild(m_towerAsset, m_buildSiteTransfom);
        BuildSite.HideControls();
    }
}
