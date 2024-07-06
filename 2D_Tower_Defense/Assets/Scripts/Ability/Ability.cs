using System;
using System.Collections;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [SerializeField] private AbilityAsset currentAbilityAsset;
    [SerializeField] private UpgradeAsset currentUpgradeAsset;
    [SerializeField] private Image image;
    [SerializeField] private Button button;
    [SerializeField] private Text costText;
    private bool isUsed;

    private void UseAttack(AbilityAsset ability, UpgradeAsset upgrade)
    {
        if (!(ability as AbilityAttackAsset) || ability.Type != AbilityAsset.AbilityType.Attack)
            return;
        IEnumerator Attacking()
        {
            Player.Instance.ChangeMage(-currentAbilityAsset.MageCost);
            bool call = false;
            Vector3 pos = new Vector3();
            CameraSpaceController.Instance.PointerDown += (PointerEventData eventData) => { call = eventData != null; pos = Camera.main.ScreenToWorldPoint(eventData.position); };
            Time.timeScale = 0.25f;
            BuildSite.ChangeClickStatus(false);
            yield return new WaitUntil(() => call);
            Time.timeScale = 1;
            BuildSite.ChangeClickStatus(true);
            (ability as AbilityAttackAsset).Attack(upgrade, pos).Invoke();
            CameraSpaceController.Instance.PointerDown -= (PointerEventData eventData) => { call = eventData != null; pos = Camera.main.ScreenToWorldPoint(eventData.position); };
        }
        StartCoroutine(Attacking());
    }
    private void UseBuff(AbilityAsset ability, UpgradeAsset upgrade)
    {
        if (!(ability as AbilityBuffAsset) || ability.Type != AbilityAsset.AbilityType.Buff)
            return;
        IEnumerator Buffing()
        {
            Player.Instance.ChangeMage(-currentAbilityAsset.MageCost);
            (ability as AbilityBuffAsset).Buff(upgrade).Invoke();
            yield return new WaitForSeconds((ability as AbilityBuffAsset).BuffTIme * (upgrade && upgrade.UpgradeTarget == UpgradeAsset.UpgradeType.Ability ? upgrade.TimeModifier : 1));
            (ability as AbilityBuffAsset).Unbuff(upgrade).Invoke();
        }
        StartCoroutine(Buffing());
    }
    public void Use()
    {
        if (!currentAbilityAsset)
            return;
        if (currentAbilityAsset.Type == AbilityAsset.AbilityType.Other)
        {
        }
        if (currentAbilityAsset.Type == AbilityAsset.AbilityType.Attack)
        {
            UseAttack(currentAbilityAsset, currentUpgradeAsset);
        }
        if (currentAbilityAsset.Type == AbilityAsset.AbilityType.Buff)
        {
            UseBuff(currentAbilityAsset, currentUpgradeAsset);
        }
        IEnumerator ReuseTimer()
        {
            isUsed = true;
            yield return new WaitForSeconds(currentAbilityAsset.ReuseTime * (currentUpgradeAsset ? currentUpgradeAsset.ReuseTimeModifier : 1));
            isUsed = false;
        }
        StartCoroutine(ReuseTimer());
    }
    private void CheckMage(float mage)
    {
        if (!button)
            return;
        bool cheker = mage >= currentAbilityAsset.MageCost;
        button.interactable = cheker && !isUsed;
        costText.color = cheker ? Color.white : Color.red;
    }
    public void AssignAbilityAsset(AbilityAsset asset)
    {
        if (asset == null)
            return;
        currentAbilityAsset = asset;
        image.overrideSprite = currentAbilityAsset.GUISprite;
        button.interactable = currentAbilityAsset.MageCost >= Player.Instance.NumGold;
        costText.text = currentAbilityAsset.MageCost.ToString();
        CheckMage(Player.Instance.NumMage);
        Player.Instance.ChangeMageAmount.RemoveListener(CheckMage);
        Player.Instance.ChangeMageAmount.AddListener(CheckMage);
    }
    public void AssignUpgradeAsset(UpgradeAsset asset)
    {
        if (asset == null || asset.UpgradeTarget != UpgradeAsset.UpgradeType.Ability || asset.AbilityUpgradeTarget != currentAbilityAsset.Type)
            return;
        currentUpgradeAsset = asset;
    }
    public void AssignAssets(AbilityAsset abilityAsset, UpgradeAsset upgradeAsset)
    {
        AssignAbilityAsset(abilityAsset);
        AssignUpgradeAsset(upgradeAsset);
    }
}
