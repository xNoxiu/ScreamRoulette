using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
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
                "Ah… so you answered. Good.",
                "That means you can hear me… and that means you can listen.",
                "Tell me… do you feel it?",
                "That prickling at the back of your neck? The weight in the air?",
                "You are not alone, you know.",
                "There is something behind you… watching. Waiting.",
                "You don’t believe me?",
                "Then look. Turn around. Just a quick glance… It won’t hurt. I promise."
            };

            List<string> option1sentences = new List<string>()
            {
                "Hm... Fine. Don't turn around, I guess..."
            };

            List<string> option2sentences = new List<string>()
            {
                "Oh, I lied."
            };

            dialogueManager.StartDialogue(sentences,
                () => dialogueManager.StartFollowUpDialogue(option1sentences, () => 
                {
                    Debug.Log("Próba za³adowania sceny 3...");
                    SceneManager.LoadScene(3);
                }),
                () => dialogueManager.StartFollowUpDialogue(option2sentences, () =>
                {
                    Debug.Log("Próba za³adowania sceny GameOver...");
                    SceneManager.LoadScene(6);
                })
            );
        }
    }
}