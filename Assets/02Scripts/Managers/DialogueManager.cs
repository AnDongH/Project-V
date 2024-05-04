using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;

public class DialogueManager : DontDestroySingleton<DialogueManager>
{
    #region Events
    public delegate void DialogueRegisteredHandler(DialogueGraph newDialogue);
    public delegate void DialogueListRegisteredHandler(List<DialogueGraph> newDialogues);
    public delegate void DialogueCompletedHandler(DialogueGraph dialogue);
    public delegate void DialogueCanceledHandler(DialogueGraph dialogue);
    public delegate void DialogueSkipedHandler(DialogueGraph dialogue);
    public delegate void DialogueNextedHandler(DialogueGraph dialogue);
    #endregion

    private DialogueGraph curDialogue;

    public DialogueGraph CurDialogue => curDialogue;

    public event DialogueRegisteredHandler OnDialogueRegistered;
    public event DialogueListRegisteredHandler OnDialogueListRegistered;
    public event DialogueCompletedHandler OnDialogueCompleted;
    public event DialogueCanceledHandler OnDialogueCanceled;
    public event DialogueSkipedHandler OnDialogueSkiped;
    public event DialogueNextedHandler OnDialogueNexted;

    public DialogueGraph RegisterDialogue(DialogueGraph dialogueGraph) {
        curDialogue = dialogueGraph;
        curDialogue.FindFirstNode();
        OnDialogueRegistered?.Invoke(curDialogue);
        return curDialogue;
    }

    public void RegisterDialogue(List<DialogueGraph> dialogueGraphs) {
        OnDialogueListRegistered?.Invoke(dialogueGraphs);
    }

    public void NextDialogue() {
        bool flag = curDialogue.NextNode();

        if (flag) OnDialogueNexted?.Invoke(curDialogue);
        else CompleteDialogue();
    }

    public void AnswerDialogue(int i) {
        bool flag = curDialogue.AnswerNode(i);

        if (flag) OnDialogueNexted?.Invoke(curDialogue);
        else CompleteDialogue();
    }

    public void CompleteDialogue() {
        OnDialogueCompleted?.Invoke(curDialogue);
        curDialogue = null;
    }

    public void CancelDialogue() {
        OnDialogueCanceled?.Invoke(curDialogue);
        CompleteDialogue();
    }

    public void SkipDialogue() {
        OnDialogueSkiped?.Invoke(curDialogue);
        CompleteDialogue();
    }

}
