using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraSpaceController : MonoSingleton<CameraSpaceController>, IPointerDownHandler
{
    public event Action<PointerEventData> PointerDown;
    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown?.Invoke(eventData);
    }
}
