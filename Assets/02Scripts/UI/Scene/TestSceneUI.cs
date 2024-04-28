using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestSceneUI : SceneUI
{
    enum Buttons {
        TextButton
    }

    enum InputFields {

    }

    enum Texts {
        Header_Text
    }

    enum Objects {

    }

    int i = 0;

    protected override void Init() {
        base.Init();
    }

    private void Start() {

        Bind<Button>(typeof(Buttons));
        Bind<InputField>(typeof(InputFields));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(Objects));

        GetButton((int)Buttons.TextButton).gameObject.BindEvent(Test);
    }

    private void OnEnable() {
        Init();
    }


    private void Test(PointerEventData data) {
        i++;
        GetText((int)Texts.Header_Text).text = i.ToString();
        Access.UI.ShowPopupUI<PopUpUI>("TestPopUpUI1");
    }

}
