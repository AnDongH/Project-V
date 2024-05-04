using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BaseUI : MonoBehaviour {
    
    protected Dictionary<Type, UnityEngine.Object[]> objects = new();

    /// <summary>
    /// Bind the UI and store it in a Dictionary with key-value pairs. (ui type, object)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    protected void Bind<T>(Type type) where T : UnityEngine.Object {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        this.objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++) {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Utils.FindChild(gameObject, names[i], true);
            else
                objects[i] = Utils.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Define.Log($"Failed to bind({names[i]})");
        }
    }

    /// <summary>
    ///  Retrieve the UI component corresponding to<T>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="idx"></param>
    /// <returns></returns>
    protected T Get<T>(int idx) where T : UnityEngine.Object {
        UnityEngine.Object[] objects = null;
        return this.objects.TryGetValue(typeof(T), out objects) == false ? null : objects[idx] as T;
    }

    /// <summary>
    /// Registers the specified action on the designated gameObject according to the given eventType.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="action"></param>
    /// <param name="eventType"></param>
    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent eventType = Define.UIEvent.Click) {
        UI_EventHandler evt = Utils.GetOrAddComponent<UI_EventHandler>(go);

        /*
            If you want to register a new event, follow these steps:

            1. Add a new enum value to UIEvent in the Define.
            2. Add a new IEventSystemHandler interface to UI_EventHandler.
            3. Create the corresponding event callback in UI_EventHandler.
            4. Register the event in BindEvent.
            5. Use the extension method to bind the event to the UI with the newly created event.

            - if you don't know well, please tell adh - 
        */
        switch (eventType) {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Enter:
                evt.OnEnterHandler -= action;
                evt.OnEnterHandler += action;
                break;
            case Define.UIEvent.Exit:
                evt.OnExitHandler -= action;
                evt.OnExitHandler += action;
                break;
        }
    }

    public static void SetInteractable(GameObject go, bool flag) {
        UI_EventHandler evt = Utils.GetOrAddComponent<UI_EventHandler>(go);
        evt.interactable = flag;
    }

    // Get UI component by Get method
    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected InputField GetInputField(int idx) { return Get<InputField>(idx); }
    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected TextMeshProUGUI GetTMP(int idx) { return Get<TextMeshProUGUI>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }
    protected Dropdown GetDropdown(int idx) { return Get<Dropdown>(idx); }
    protected Toggle GetToggle(int idx) { return Get<Toggle>(idx); }
    protected RectTransform GetRect(int idx) { return Get<RectTransform>(idx); }
}