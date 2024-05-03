using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using TMPro;
using UnityEngine.EventSystems;

public class SelectBtn : PopUpUI
{
    enum TMPs {
        BtnText
    }

    [SerializeField] float vlaue;

    private int idx;

    public void Init(int idx, string text, bool mul, DialogueGraph dialogue =null) {

        Bind<TextMeshProUGUI>(typeof(TMPs));
        if (mul) gameObject.BindEvent((data) => OnBtnPressedMulti(data, dialogue));
        else gameObject.BindEvent(OnBtnPressedSingle);
        gameObject.BindEvent(OnBtnEntered, Define.UIEvent.Enter);
        gameObject.BindEvent(OnBtnExited, Define.UIEvent.Exit);

        this.idx = idx;
        GetTMP((int)TMPs.BtnText).text = text;
    }

    public void OnBtnEntered(PointerEventData data) {
        transform.localScale = Vector3.one * vlaue;
    }

    public void OnBtnExited(PointerEventData data) {
        transform.localScale = Vector3.one;
    }

    private void OnBtnPressedSingle(PointerEventData data) {
       DialogueManager.Instance.AnswerDialogue(idx);
    }

    private void OnBtnPressedMulti(PointerEventData data, DialogueGraph dialogue) {
        DialogueManager.Instance.RegisterDialogue(dialogue);
    }
}
