using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler {
    public Action<PointerEventData> OnClickHandler = null;

    public bool interactable = true;

    public void OnPointerClick(PointerEventData eventData) // Ŭ�� �̺�Ʈ �������̵�
    {
        if (!interactable) return;

        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData); // Ŭ���� ���õ� �׼� ����
    }


}