using System.Collections.Generic;
using UnityEngine;

public class SatorukunDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;
    private bool hasTalked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTalked)
        {
            hasTalked = true;

            List<string> sentences = new List<string>()
            {
                "... Hello? ... ",
                "You shouldn't be here...",
                "I will let you go, only if you'll answer my question correctly...",
                "How do you call a japanese fox?"
            };

            string correctAnswer = "kitsune";

            dialogueManager.StartDialogue(sentences, correctAnswer,
                () => { Debug.Log("Hm... Correct. I guess I need to let you go now..."); },
                () => { Debug.Log("Wrong! You die!"); }
            );
        }
    }
}