using UnityEngine;
using UnityEngine.UI;

public class TowerBuyController : MonoBehaviour
{
    [SerializeField] private Tower m_towerPrefab;
    [SerializeField] private TowerAsset m_towerAsset;
    [SerializeField] private Button m_button;
    [SerializeField] private Text m_text;
    private Transform m_buildSiteTransfom;

    public void SetBuildSite(Transform value)
    {
        m_buildSiteTransfom = value;
    }

    private void Start()
    {
        Player.Instance.ChangeGoldAmount.AddListener(GoldStatusCheck);
        GoldStatusCheck(Player.Instance.NumGold);
        m_text.text = m_towerAsset.GlodCost.ToString();
        m_button.GetComponent<Image>().sprite = m_towerAsset.GUISprite;
    }
    private void OnDestroy()
    {
        Player.Instance.ChangeGoldAmount.RemoveListener(GoldStatusCheck);
    }
    private void GoldStatusCheck(int value)
    {
        m_button.interactable = value >= m_towerAsset.GlodCost;
        m_text.color = m_button.interactable ? Color.white : Color.red;
    }
    public void TryBuild()
    {
        Player.Instance.ChangeGold(-m_towerAsset.GlodCost);
        var towerObject = Instantiate(m_towerPrefab, m_buildSiteTransfom.position, Quaternion.identity);
        towerObject.AssignTowerAsset(m_towerAsset);
        towerObject.GetComponentInChildren<SpriteRenderer>().sprite = m_towerAsset.TowerSprite;

        Destroy(m_buildSiteTransfom.gameObject);
        BuildSite.HideControls();
    }
}
