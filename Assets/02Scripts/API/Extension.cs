using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension {

    public static T GetOrAddComponent<T>(this GameObject go) where T : Component {
        return Utils.GetOrAddComponent<T>(go);
    }

    public static void BindEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent eventType = Define.UIEvent.Click) {
        BaseUI.BindEvent(go, action, eventType);
    }

    public static void SetInteractable(this GameObject go, bool flag) {
        BaseUI.SetInteractable(go, flag);
    }
}