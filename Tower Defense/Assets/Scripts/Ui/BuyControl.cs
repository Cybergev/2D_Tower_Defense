using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyControl : MonoBehaviour
{
    [SerializeField] private RectTransform m_rectTransform;
    private void Start()
    {
        if (!m_rectTransform)
        {
            m_rectTransform = GetComponent<RectTransform>();
        }
        BuildSite.OnClickEvent += MoveToTransform;
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        BuildSite.OnClickEvent -= MoveToTransform;
    }
    private void MoveToTransform(Transform target)
    {
        if (target)
        {
            var position = Camera.main.WorldToScreenPoint(target.position);
            m_rectTransform.anchoredPosition = position;
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
        {
            tbc.SetBuildSite(target);
        }
    }
}
