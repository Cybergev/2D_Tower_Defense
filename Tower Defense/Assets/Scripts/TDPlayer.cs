using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TDPlayer : Player
{
    public static new TDPlayer Instance { get => Player.Instance as TDPlayer; }

    private static event Action<int> ChangeGoldAmount;

    [SerializeField] private Tower m_towerPrefab;

    [SerializeField] private int m_NumGold;
    public int NumGold => m_NumGold;
    public int PastNumGold { get; private set; }
    public static void GoldUpdateSubscribe(Action<int> act)
    {
        ChangeGoldAmount += act;
        act(Instance.m_NumGold);
    }
    public static void GoldUpdateDiscribe(Action<int> act)
    {
        ChangeGoldAmount -= act;
        act(Instance.m_NumGold);
    }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        ChangeGoldAmount(m_NumGold);
    }

    protected override void Update()
    {
        base.Update();
        if (PastNumGold != NumGold)
        {
            PastNumGold = NumGold;
            ChangeGoldAmount(NumGold);
        }
    }

    public void ChangeGold(int value)
    {
        m_NumGold += value;
    }
    public void TryBuild(TowerAsset m_towerAsset, Transform buildSite)
    {
        ChangeGold(-m_towerAsset.glodCost);
        var towerObject = Instantiate(m_towerPrefab, buildSite.position, Quaternion.identity);
        var towerTurret = towerObject.GetComponentInChildren<Turret>();
        var tower = towerObject.GetComponentInChildren<Tower>();

        towerObject.GetComponentInChildren<SpriteRenderer>().sprite = m_towerAsset.towerSprite;
        towerTurret.AssignLoadut(m_towerAsset.tuerretProperties);
        tower.SetRadius(m_towerAsset.towerRadius);

        Destroy(buildSite.gameObject);
    }
}
