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
                "Wrong! You die!",
                "Your journey ends here..."
            };

            string correctAnswer = "kitsune";

            dialogueManager.StartDialogue(sentences, correctAnswer,
                () => dialogueManager.StartFollowUpDialogue(correctAnswerSentences, () => SceneManager.LoadSceneAsync(3)),
                () => dialogueManager.StartFollowUpDialogue(wrongAnswerSentences, () => SceneManager.LoadScene("GameOver"))
            );
        }
    }
}