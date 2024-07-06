using System.Collections.Generic;
using UnityEngine;

public class BuyController : MonoSingleton<BuyController>
{
    [SerializeField] private RectTransform m_rectTransform;
    [SerializeField] private BuildController m_buildControllerPef;
    private List<BuildController> m_buildControllers = new List<BuildController>();
    private List<UpgradeAsset> m_upgradeAssets = new List<UpgradeAsset>();
    private List<UnlockerAsset> m_unlockAssets = new List<UnlockerAsset>();
    private void Start()
    {
        if (!m_rectTransform)
            m_rectTransform = GetComponent<RectTransform>();
        foreach (var item in ItemController.Instance.Items)
        {
            if (item is UnlockerAsset)
                if ((item as UnlockerAsset).UnlockTarget == UnlockerAsset.UnlockerType.Tower)
                    m_unlockAssets.Add(item as UnlockerAsset);
            if (item is UpgradeAsset)
                if ((item as UpgradeAsset).UpgradeTarget == UpgradeAsset.UpgradeType.Tower)
                    m_upgradeAssets.Add(item as UpgradeAsset);
        }
        BuildSite.OnClickAction += MoveToTransform;
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        BuildSite.OnClickAction -= MoveToTransform;
    }
    private void MoveToTransform(BuildSite target)
    {
        if (m_buildControllers.Count > 0)
        {
            foreach (var controller in m_buildControllers)
                Destroy(controller.gameObject);
            m_buildControllers.Clear();
        }
        if (target)
        {
            var position = Camera.main.WorldToScreenPoint(target.transform.position);
            m_rectTransform.anchoredPosition = position;
            Tower tower = target as Tower;
            if (tower)
            {
                List<UpgradeAsset> upgrades = new List<UpgradeAsset>();
                foreach (UpgradeAsset upgradeAsset in m_upgradeAssets)
                    if (upgradeAsset.TowerUpgradeTarget == tower.CurrentTowerAsset.Type)
                    {
                        if (tower.CurrentUpgradeAsset)
                        {
                            if (upgradeAsset.UpgradeLevel >= tower.CurrentUpgradeAsset.UpgradeLevel)
                            {
                                if (upgrades.Count > 0)
                                {
                                    foreach (var upgrade in upgrades)
                                        if (upgrade.UpgradeLevel <= upgradeAsset.UpgradeLevel)
                                            upgrades.Add(upgradeAsset);
                                }
                                else
                                    upgrades.Add(upgradeAsset);
                            }
                        }
                        else
                            upgrades.Add(upgradeAsset);
                    }
                var destruckter = Instantiate(m_buildControllerPef, transform);
                destruckter.SetAsDestruckter();
                m_buildControllers.Add(destruckter);
                for (int i = 0; i < upgrades.Count; i++)
                {
                    var controller = Instantiate(m_buildControllerPef, transform);
                    controller.AssignUpgradeAsset(upgrades[i]);
                    m_buildControllers.Add(controller);
                }
            }
            else
            {
                for (int i = 0; i < m_unlockAssets.Count; i++)
                {
                    var asset = m_unlockAssets[i];
                    var controller = Instantiate(m_buildControllerPef, transform);
                    controller.AssignTowerAsset(asset.UnlockTowerAsset);
                    m_buildControllers.Add(controller);
                }
            }
            if (m_buildControllers.Count > 0)
            {
                float angle = 360 / m_buildControllers.Count;
                for (int i = 0; i < m_buildControllers.Count; i++)
                {
                    if (m_buildControllers[i].IsDestruckter)
                        continue;
                    var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.left * 80);
                    m_buildControllers[i].transform.position += offset;
                }
            }
            gameObject.SetActive(true);
            foreach (var tbc in GetComponentsInChildren<BuildController>())
                tbc.SetBuildSite(target);
        }
        else
        {
            gameObject.SetActive(false);
            foreach (var controller in m_buildControllers)
                Destroy(controller.gameObject);
            m_buildControllers.Clear();
        }
    }
    public void RemoveListeningActions()
    {
        BuildSite.OnClickAction -= MoveToTransform;
    }
    public void AddListeningActions()
    {
        BuildSite.OnClickAction += MoveToTransform;
    }
}
