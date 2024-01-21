using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildSite : MonoBehaviour
{
    public enum BuildType
    {
        None,
        Normal,
        Large
    }
    public static event Action<BuildSite> OnClickAction;
    [SerializeField] private BuildType buildSiteType;
    public BuildType BuildSiteType => buildSiteType;
    public static void HideControls()
    {
        OnClickAction(null);
    }
    public void OnClick()
    {
        OnClickAction(this);
    }
}
