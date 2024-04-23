using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestPopUp_UI : UI_PopUp
{
    enum Buttons {
        TextButton
    }

    protected override void Init() {
        base.Init();
    }

    private void Start() {

        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.TextButton).gameObject.BindEvent(OnExitBtnClicked);
    }

    private void OnEnable() {
        Init();
    }


    private void OnExitBtnClicked(PointerEventData data) {
        ClosePopUpUI();
    }
}
