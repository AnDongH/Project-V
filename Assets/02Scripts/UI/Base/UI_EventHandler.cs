using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler {
    public Action<PointerEventData> OnClickHandler = null;

    public bool interactable = true;

    public void OnPointerClick(PointerEventData eventData) // 클릭 이벤트 오버라이딩
    {
        if (!interactable) return;

        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData); // 클릭와 관련된 액션 실행
    }


}