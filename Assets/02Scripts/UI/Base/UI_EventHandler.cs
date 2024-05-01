using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Register UI Event
/// </summary>
public class UI_EventHandler : MonoBehaviour, IPointerClickHandler {

    public Action<PointerEventData> OnClickHandler = null;
    public bool interactable = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable) return;
        OnClickHandler?.Invoke(eventData);
    }
}