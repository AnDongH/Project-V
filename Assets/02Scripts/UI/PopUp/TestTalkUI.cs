using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestTalkUI : UI_PopUp
{
    enum Objects {
        TestTalkUI
    }

    protected override void Init() {
        base.Init();
    }

    private void Start() {

        Bind<GameObject>(typeof(Objects));

        GetButton((int)Objects.TestTalkUI).gameObject.BindEvent(OnExitBtnClicked);
    }

    private void OnEnable() {
        Init();
    }


    private void OnExitBtnClicked(PointerEventData data) {
        Access.Talk.NextTalk();
    }
}
