using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class NPCDialogue : MonoBehaviour, IInteractable
{
    public DialogueRunner dialogueRunner;
    public string test;
    public GameObject textComponent;
    public bool isActive = false;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            isActive = !isActive;
            textComponent.SetActive(isActive);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isActive = !isActive;
            textComponent.SetActive(isActive);
        }
    }

    public void Interact()
    {
        dialogueRunner.StartDialogue(test);
    }
}
