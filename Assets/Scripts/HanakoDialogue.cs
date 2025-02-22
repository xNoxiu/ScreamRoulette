using Mono.Cecil.Cil;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.SceneManagement;

public class HanakoDialogue : MonoBehaviour
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
                "Oh? A visitor… how rare. You’ve come so far, haven’t you? And now you stand before me, trembling, desperate.",
                "What is it you seek?",
                " Player: 'I want to leave this place.' ",
                " Hanako: 'To leave? From here? From the place between breath and stillness… between heartbeat and silence?' ",
                "Escape is no simple thing, foolish traveler.",
                "This is not a door you may simply walk through. No path leads away without a price.",
                " Player: 'Name your price. Whatever it is, I will pay it.' ",
                " Hanako: ''Such bravery... or is it ignorance? No matter. The deal is struck.' ",
                " [Darkness swells. The world twists. A sharp, searing pain—brief, then nothing. Silence. When you come to, the world is black. Lips move, but no sound escapes.] ",
                " Mysterious Voice: 'Eyes tainted by the sight of this place may never leave it. A tongue that would tell its tale must never speak again.' ",
                " Hanako: 'Ah… such a high price. But you did say… you were willing to pay.' "

            };
            List<string> option1sentences = new List<string>()
            {
                " "
            };
            List<string> option2sentences = new List<string>()
            {
                " "
            };
            dialogueManager.StartDialogue(sentences,
            () => dialogueManager.StartFollowUpDialogue(option1sentences, () =>
                {
                    Debug.Log("Próba za³adowania sceny 7...");
                    SceneManager.LoadScene(7);
                }),
                () => dialogueManager.StartFollowUpDialogue(option2sentences, () =>
                {
                    Debug.Log("Próba za³adowania sceny GameOver...");
                    SceneManager.LoadScene(7);
                })
            );
        }
    }
}