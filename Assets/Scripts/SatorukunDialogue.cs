using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            List<string> correctAnswerSentences = new List<string>()
            {
                "Hm... Correct. I guess I need to let you go now..."
            };

            List<string> wrongAnswerSentences = new List<string>()
            {
                "Wrong! You die!"
            };

            string correctAnswer = "kitsune";

            dialogueManager.StartDialogue(sentences, correctAnswer,
                () => dialogueManager.StartFollowUpDialogue(correctAnswerSentences, () => 
                {
                    Debug.Log("Próba za³adowania sceny 3...");
                    SceneManager.LoadScene(3);
                }),
                () => dialogueManager.StartFollowUpDialogue(wrongAnswerSentences, () =>
                {
                    Debug.Log("Próba za³adowania sceny GameOver...");
                    SceneManager.LoadScene("GameOver");
                })
            );
        }
    }
}