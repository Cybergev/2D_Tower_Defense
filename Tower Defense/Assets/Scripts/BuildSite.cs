using System;
using UnityEngine;

public class BuildSite : MonoBehaviour
{
    public enum BuildType
    {
        None,
        Normal,
        Large
    }
    public static event Action<BuildSite> OnClickAction;
    public static bool IsClickable { get; private set; } = true;

    [SerializeField] private BuildType buildSiteType;
    public BuildType BuildSiteType => buildSiteType;
    public static void ChangeClickStatus(bool clickStatus)
    {
        IsClickable = clickStatus;
    }
    public static void HideControls()
    {
        OnClickAction(null);
    }
    public void OnClick()
    {
        if (!IsClickable)
            return;
        OnClickAction(this);
    }
}
