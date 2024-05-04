using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : NormalSingleton<UIManager> {

    // Canvas sorting order (only popup ui)
    private int sortingOrder = 10;

    // stack of popup ui
    private LinkedList<PopUpUI> popupStack = new LinkedList<PopUpUI>();

    // current UI objects at cur Scene
    private Dictionary<string, GameObject> curUIObjs = new Dictionary<string, GameObject>();

    // current UI objects initalize
    [Header("UI objs at cur Scene")]
    [SerializeField] private GameObject[] uiObjs;

    protected override void Awake() {
        base.Awake();
        foreach (var obj in uiObjs) {
            curUIObjs.Add(obj.name, obj.gameObject);
        }
    }

    /// <summary>
    /// Creation and initialization of a Canvas (a single UI functional unit).
    /// </summary>
    /// <param name="go"></param>
    /// <param name="sort"></param>
    public void SetCanvas(GameObject go, bool sort = true) {

        Canvas canvas = Utils.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.overrideSorting = true;

        if (sort) {
            canvas.sortingOrder = sortingOrder;
            sortingOrder++;
        }
        else {
            canvas.sortingOrder = 0;
        }
    }

    /// <summary>
    /// Active True Scene UI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T ShowSceneUI<T>(string name = null) where T : SceneUI {
        if (string.IsNullOrEmpty(name)) name = typeof(T).Name;

        GameObject go = UI_Instantiate(name);
        T SceneUI = Utils.GetOrAddComponent<T>(go);

        return SceneUI;
    }

    /// <summary>
    /// Active True PopUp UI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T ShowPopupUI<T>(string name = null) where T : PopUpUI {
        if (string.IsNullOrEmpty(name)) name = typeof(T).Name;

        GameObject go = UI_Instantiate(name);
        T popup = Utils.GetOrAddComponent<T>(go);

        if (!popupStack.Contains(popup)) popupStack.AddLast(popup);

        return popup;
    }

    /// <summary>
    /// Close the popup UI provided as an argument.
    /// </summary>
    /// <param name="popup"></param>
    public void ClosePopupUI(PopUpUI popup) {
        if (popupStack.Count == 0) {
            Define.LogError("not exist popup UIM");
            return;
        }

        popup.gameObject.SetActive(false);
        popupStack.Remove(popup);

        // re sort canvas ordering
        sortingOrder = 10;
        foreach (var p in popupStack) {
            Utils.GetOrAddComponent<Canvas>(p.gameObject).sortingOrder = sortingOrder;
            sortingOrder++;
        }
    }

    /// <summary>
    /// Close the most recently opened popup UI.
    /// </summary>
    public void ClosePopupUI() {
        if (popupStack.Count == 0) {
            Define.LogError("not exist popup UIM");
            return;
        }

        PopUpUI popup = popupStack.Last.Value;
        popup.gameObject.SetActive(false);
        popupStack.RemoveLast();
        sortingOrder--;
    }

    /// <summary>
    /// Close all popup UI.
    /// </summary>
    public void CloseAllPopupUI() {
        while (popupStack.Count > 0)
            ClosePopupUI();
    }

    /// <summary>
    /// UI On
    /// </summary>
    /// <param name="name"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    private GameObject UI_Instantiate(string name) {
        curUIObjs[name].SetActive(true);
        return curUIObjs[name];
    }
}