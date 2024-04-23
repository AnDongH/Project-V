using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Manager : NormalSingleton<UI_Manager> {

    private int _order = 10;

    private LinkedList<UI_PopUp> _popupStack = new LinkedList<UI_PopUp>();
    
    private UI_Scene _sceneUI = null;

    private Dictionary<string, GameObject> curUIObjs = new Dictionary<string, GameObject>();

    [SerializeField] private GameObject[] uiObjs;

    private void Start() {
        foreach (var obj in uiObjs) {
            curUIObjs.Add(obj.name, obj.gameObject);
        }
    }

#if UNITY_EDITOR
    private void Update() {


        if (Input.GetKeyDown(KeyCode.Alpha1))
            ShowPopupUI<UI_PopUp>("TestPopUpUI1");
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ShowPopupUI<UI_PopUp>("TestPopUpUI2");
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ShowPopupUI<UI_PopUp>("TestPopUpUI3");
        if (Input.GetKeyDown(KeyCode.Alpha4))
            ShowPopupUI<UI_PopUp>("TestPopUpUI4");
        if (Input.GetKeyDown(KeyCode.Alpha5))
            ShowPopupUI<UI_PopUp>("TestPopUpUI5");

        if (Input.GetKeyDown(KeyCode.Escape))
            ClosePopupUI();
        if (Input.GetKeyDown(KeyCode.A))
            CloseAllPopupUI();

    }
#endif


    /// <summary>
    /// 캔버스(하나의 UI 묶음 단위) 생성, 초기화
    /// </summary>
    /// <param name="go"></param>
    /// <param name="sort"></param>
    public void SetCanvas(GameObject go, bool sort = true) {
        Canvas canvas = Utils.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.overrideSorting = true;
        if (sort) {
            canvas.sortingOrder = _order;
            _order++;
        }
        else {
            canvas.sortingOrder = 0;
        }
    }

    /// <summary>
    /// 고정 UI 생성
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go;

        go = UI_Instantiate(name);

        T SceneUI = Utils.GetOrAddComponent<T>(go);
        _sceneUI = SceneUI;

        return SceneUI;
    }

    /// <summary>
    /// 팝업 UI 생성
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T ShowPopupUI<T>(string name = null) where T : UI_PopUp {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go;

        go = UI_Instantiate(name);

        T popup = Utils.GetOrAddComponent<T>(go);
        if (!_popupStack.Contains(popup)) _popupStack.AddLast(popup);
        else ClosePopupUI(popup);

        return popup;
    }

    /// <summary>
    /// 팝업 UI 닫기 -> 닫으려는 것이 가장 마지막에 연건지 확인
    /// </summary>
    /// <param name="popup"></param>
    public void ClosePopupUI(UI_PopUp popup) {
        if (_popupStack.Count == 0)
            return;

        _popupStack.Remove(popup);
        popup.gameObject.SetActive(false);

        _order = 10;

        foreach (var p in _popupStack) {
            Utils.GetOrAddComponent<Canvas>(p.gameObject).sortingOrder = _order;
            _order++;
        }
    }

    /// <summary>
    /// 팝업 UI 닫기 -> 가장 마지막에 연 UI 닫기
    /// </summary>
    public void ClosePopupUI() {
        if (_popupStack.Count == 0)
            return;

        UI_PopUp popup = _popupStack.Last.Value;
        popup.gameObject.SetActive(false);
        _popupStack.RemoveLast();
        Debug.Log(1);
        _order--;
    }

    /// <summary>
    /// 모든 팝업 UI 닫기
    /// </summary>
    public void CloseAllPopupUI() {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    /// <summary>
    /// UI On
    /// </summary>
    /// <param name="name"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    private GameObject UI_Instantiate(string name, Transform parent = null) {
        curUIObjs[name].SetActive(true);
        return curUIObjs[name];
    }
}