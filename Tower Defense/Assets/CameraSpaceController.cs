using UnityEngine;
using UnityEngine.EventSystems;

public class CameraSpaceController : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        BuildSite.HideControls();
    }
}
