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
        Init();
        Bind<GameObject>(typeof(Objects));
        GetButton((int)Objects.TestTalkUI).gameObject.BindEvent(OnPanelClicked);
    }

    private void OnPanelClicked(PointerEventData data) {
        
    }
}
