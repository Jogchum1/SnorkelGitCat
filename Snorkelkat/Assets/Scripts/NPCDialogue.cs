using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class NPCDialogue : MonoBehaviour, IInteractable
{
    public DialogueRunner dialogueRunner;
    public string test;
    public void Interact()
    {
        dialogueRunner.StartDialogue(test);
    }
}
