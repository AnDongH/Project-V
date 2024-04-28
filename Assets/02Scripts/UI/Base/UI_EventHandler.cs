using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 
/// </summary>
public class UI_EventHandler : MonoBehaviour, IPointerClickHandler {

    public Action<PointerEventData> OnClickHandler = null;
    public bool interactable = true;

    public void OnPointerClick(PointerEventData eventData) // Ŭ�� �̺�Ʈ �������̵�
    {
        if (!interactable) return;
        OnClickHandler?.Invoke(eventData); // Ŭ���� ���õ� �׼� ���� // Ŭ���� ���õ� �׼� ����
    }

}