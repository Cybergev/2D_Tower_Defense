using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TDPlayer : Player
{
    public static new TDPlayer Instance { get => Player.Instance as TDPlayer; }

    [SerializeField] private UnityEvent<int> changeGoldAmount;
    [HideInInspector] public UnityEvent<int> ChangeGoldAmount => changeGoldAmount;

    [SerializeField] private int m_NumGold;
    public int NumGold => m_NumGold;
    public int PastNumGold { get; private set; }

    protected override void Update()
    {
        base.Update();
        if (PastNumGold != NumGold)
        {
            PastNumGold = NumGold;
            changeGoldAmount.Invoke(NumGold);
        }
    }

    public void ChangeGold(int value)
    {
        m_NumGold += value;
    }
}
