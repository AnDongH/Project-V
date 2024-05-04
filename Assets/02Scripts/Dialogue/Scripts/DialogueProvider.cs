using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;

public class DialogueProvider : MonoBehaviour
{

    [SerializeField] private DialogueGraph[] dialogues;

    private List<DialogueGraph> GetAvailable() {

        List<DialogueGraph> results = new List<DialogueGraph>();

        foreach (DialogueGraph dialogue in dialogues) {
            if (dialogue.IsAcceptable) results.Add(dialogue);
        }

        return results;
    }

    public void Provide() {

        List<DialogueGraph> results = GetAvailable();

        if (results.Count == 0) return;

        if (results.Count == 1) Access.DIalogueM.RegisterDialogue(results[0]);
        else Access.DIalogueM.RegisterDialogue(results);
    }
}
