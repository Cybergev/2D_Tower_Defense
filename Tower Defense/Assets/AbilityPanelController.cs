using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityPanelController : MonoBehaviour
{
    [SerializeField] private Ability abilityPrefab;
    private List<UnlockerAsset> unlockAssets = new List<UnlockerAsset>();
    private List<UpgradeAsset> upgradeAssets = new List<UpgradeAsset>();
    private List<Ability> abilities = new List<Ability>();
    private void Start()
    {
        foreach (var item in ItemController.Instance.Items)
        {
            if (item is UnlockerAsset)
                if ((item as UnlockerAsset).UnlockTarget == UnlockerAsset.UnlockerType.Ability)
                    unlockAssets.Add(item as UnlockerAsset);
            if (item is UpgradeAsset)
                if ((item as UpgradeAsset).UpgradeTarget == UpgradeAsset.UpgradeType.Ability)
                    upgradeAssets.Add(item as UpgradeAsset);
        }
        if (unlockAssets.Count > 0)
        {
            foreach (var unlocker in unlockAssets)
            {
                var ability = Instantiate(abilityPrefab, transform);
                ability.AssignAbilityAsset(unlocker.UnlockAbilityAsset);
                foreach (var upgrade in upgradeAssets)
                    if (upgrade.AbilityUpgradeTarget == unlocker.UnlockAbilityAsset.Type)
                        ability.AssignUpgradeAsset(upgrade);
                abilities.Add(ability);
            }
            if (abilities.Count > 0)
            {
                for (int i = 0; i < abilities.Count; i++)
                {
                    var offset = abilities[i].GetComponent<RectTransform>().rect.width * i;
                    abilities[i].transform.position += new Vector3(offset, 0, 0);
                }
            }
        }
    }
}
