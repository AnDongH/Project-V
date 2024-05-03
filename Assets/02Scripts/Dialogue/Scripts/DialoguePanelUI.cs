using Dialogue;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialoguePanelUI : PopUpUI
{
    enum TMPs {
        TalkText,
        TalkerText,
    }

    [SerializeField] private SelectBtn btn;
    [SerializeField] private Transform p;

    private List<SelectBtn> btns = new List<SelectBtn>();

    protected override void Init() {
        base.Init();
    }

    private void Start() {

        Bind<TextMeshProUGUI>(typeof(TMPs));
        gameObject.BindEvent(NextDialogue);

        Access.DIalogueM.OnDialogueRegistered += OnRegistered;
        Access.DIalogueM.OnDialogueListRegistered += OnRegistered;
        Access.DIalogueM.OnDialogueNexted += OnNexted;
        Access.DIalogueM.OnDialogueCompleted += OnCompleted;

        gameObject.SetActive(false);
    }

    private void OnEnable() {
        Init();
    }

    private void OnDestroy() {
        Access.DIalogueM.OnDialogueRegistered -= OnRegistered;
        Access.DIalogueM.OnDialogueListRegistered -= OnRegistered;
        Access.DIalogueM.OnDialogueNexted -= OnNexted;
        Access.DIalogueM.OnDialogueCompleted -= OnCompleted;
    }

    private void NextDialogue(PointerEventData data) {
        DialogueManager.Instance.NextDialogue();
    }

    private void OnRegistered(DialogueGraph dialogue) {

        foreach (SelectBtn btn in btns) {
            Destroy(btn.gameObject);
        }
        btns.Clear();

        Access.UIM.ShowPopupUI<PopUpUI>(name);
        GetTMP((int)TMPs.TalkText).text = dialogue.currentNode.text;
        GetTMP((int)TMPs.TalkerText).text = dialogue.currentNode.character.TalkerName;
        GetTMP((int)TMPs.TalkText).color = dialogue.currentNode.character.TextColor;

        if (dialogue.currentNode.answers.Count != 0) {
            for (int i = 0; i < dialogue.currentNode.answers.Count; i++) {
                btns.Add(Instantiate(btn, p));
                btns[i].Init(i, dialogue.currentNode.answers[i].text, false);
            }
            gameObject.SetInteractable(false);
        }
        else
            gameObject.SetInteractable(true);
    }

    private void OnRegistered(List<DialogueGraph> dialogues) {

        foreach (SelectBtn btn in btns) {
            Destroy(btn.gameObject);
        }
        btns.Clear();

        Access.UIM.ShowPopupUI<PopUpUI>(name);
        GetTMP((int)TMPs.TalkText).text = "무엇을 물어볼까?";
        GetTMP((int)TMPs.TalkerText).text = "나";
        GetTMP((int)TMPs.TalkText).color = Color.white;

        if (dialogues.Count != 0) {
            for (int i = 0; i < dialogues.Count; i++) {
                btns.Add(Instantiate(btn, p));
                btns[i].Init(i, dialogues[i].dialogueName, true, dialogues[i]);
            }
            gameObject.SetInteractable(false);
        }
        else
            gameObject.SetInteractable(true);
    }

    private void OnNexted(DialogueGraph dialogue) {

        foreach (SelectBtn btn in btns) {
            Destroy(btn.gameObject);
        }
        btns.Clear();

        GetTMP((int)TMPs.TalkText).text = dialogue.currentNode.text;
        GetTMP((int)TMPs.TalkerText).text = dialogue.currentNode.character.TalkerName;
        GetTMP((int)TMPs.TalkText).color = dialogue.currentNode.character.TextColor;

        if (dialogue.currentNode.answers.Count != 0) {
            for (int i = 0; i < dialogue.currentNode.answers.Count; i++) {
                btns.Add(Instantiate(btn, p));
                btns[i].Init(i, dialogue.currentNode.answers[i].text, false);
            }
            gameObject.SetInteractable(false);
        }
        else
            gameObject.SetInteractable(true);
    }

    private void OnCompleted(DialogueGraph dialogue) {
        foreach (SelectBtn btn in btns) {
            Destroy(btn.gameObject);
        }
        btns.Clear();
        ClosePopUpUI();
    }

    
}
