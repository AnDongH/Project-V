using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TalkUI : PopUpUI
{
    enum Objects {
        TestTalkUI
    }

    protected override void Init() {
        base.Init();
    }

    private void Start() {

        Bind<GameObject>(typeof(Objects));

        GetButton((int)Objects.TestTalkUI).gameObject.BindEvent(OnPanelClicked);
    }

    private void OnEnable() {
        Init();
    }

    private void OnPanelClicked(PointerEventData data) {
        Access.Talk.NextTalk();
    }
}
