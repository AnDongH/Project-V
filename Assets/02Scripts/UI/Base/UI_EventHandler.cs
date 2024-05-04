using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Register UI Event
/// </summary>
public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnEnterHandler = null;
    public Action<PointerEventData> OnExitHandler = null;
    public bool interactable = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable) return;
        OnClickHandler?.Invoke(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (!interactable) return;
        OnEnterHandler?.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (!interactable) return;
        OnExitHandler?.Invoke(eventData);
    }
}