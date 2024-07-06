using UnityEngine;
using UnityEngine.UI;

public class BuildController : MonoBehaviour
{
    [SerializeField] private Button m_button;
    [SerializeField] private Text m_text;
    [SerializeField] private BuildSite m_buildSitePref;
    [SerializeField] private Sprite m_destruckterGUI;
    public bool IsDestruckter { private set; get; }
    private TowerAsset m_towerAsset;
    private UpgradeAsset m_upgradeAsset;

    private BuildSite m_buildSite;
    private Tower m_tower;


    private void OnDestroy()
    {
        Player.Instance.ChangeGoldAmount.RemoveListener(GoldStatusCheck);
    }
    private void GoldStatusCheck(int value)
    {
        if(m_towerAsset)
            m_button.interactable = value >= m_towerAsset.GlodCost;
        if (m_upgradeAsset)
            m_button.interactable = value >= m_upgradeAsset.GlodCost;
        m_text.color = m_button.interactable ? Color.white : Color.red;
    }
    public void TryBuild()
    {
        if (m_tower && m_upgradeAsset)
        {
            Player.Instance.ChangeGold(-m_upgradeAsset.Cost);
            m_tower.AssignUpgradeAsset(m_upgradeAsset);
        }
        if (m_buildSite && m_towerAsset)
        {
            Player.Instance.ChangeGold(-m_towerAsset.GlodCost);
            var towerObject = Instantiate(m_towerAsset.TowerPref, m_buildSite.transform.position, Quaternion.identity, m_buildSite.transform.root);
            towerObject.AssignTowerAsset(m_towerAsset);
            towerObject.AssignUpgradeAsset(m_upgradeAsset);
            Destroy(m_buildSite.transform.gameObject);
        }
        if (IsDestruckter)
            TryDestroy();
        BuildSite.HideControls();
    }
    public void TryDestroy()
    {
        Instantiate(m_buildSitePref, m_buildSite.transform.position, Quaternion.identity, m_buildSite.transform.root);
        Destroy(m_tower.transform.gameObject);
    }
    public void AssignTowerAsset(TowerAsset towerAsset)
    {
        if (!towerAsset)
            return;
        m_towerAsset = towerAsset;
        m_upgradeAsset = null;
        m_text.text = towerAsset.GlodCost.ToString();
        m_button.GetComponent<Image>().sprite = towerAsset.GUISprite;
        Player.Instance.ChangeGoldAmount.AddListener(GoldStatusCheck);
        GoldStatusCheck(Player.Instance.NumGold);
    }
    public void AssignUpgradeAsset(UpgradeAsset upgradeAsset)
    {
        if (!upgradeAsset)
            return;
        m_towerAsset = null;
        m_upgradeAsset = upgradeAsset;
        m_text.text = upgradeAsset.GlodCost.ToString();
        m_button.GetComponent<Image>().sprite = upgradeAsset.ItemImage;
        Player.Instance.ChangeGoldAmount.AddListener(GoldStatusCheck);
        GoldStatusCheck(Player.Instance.NumGold);
    }
    public void SetAsDestruckter()
    {
        IsDestruckter = true;
        m_towerAsset = null;
        m_upgradeAsset = null;
        Destroy(m_text.transform.parent.gameObject);
        m_button.GetComponent<Image>().sprite = m_destruckterGUI;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
    public void SetBuildSite(BuildSite value)
    {
        if (!value)
            return;
        m_buildSite = value;
        m_tower = value as Tower;
    }

}
