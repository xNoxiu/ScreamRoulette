using System;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NekomataDialogue : MonoBehaviour
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
                "Ah… what do we have here? A lost little soul, wandering so carelessly into my domain? Foolish… so very foolish.",
                "You wish to pass? Oh, how amusing.",
                "Did you think I would grant you such a mercy without a price? No, no, no… The world is cruel, little one, and nothing is given freely.",
                " (The Nekomata licks its fangs, eyes gleaming in the dim light.) ",
                "I am… hungry… and you reek of life, warm and pulsing.",
                "I could tear you apart here and now, feast upon your trembling flesh, and none would know you ever existed.",
                " (It tilts its head, a wicked grin stretching across its face.) ",
                "But I am generous… in my own way. A trade, yes? A morsel of yourself—just a taste—offered willingly.",
                "Flesh, bone, a delicate digit perhaps? It matters not… only that you surrender it to me.",
                "Deny me, and the choice shall no longer be yours. So… what will it be, traveler? A gift of flesh… or the cold embrace of oblivion?"
            };

            List<string> option1sentences = new List<string>()
            {
                "Mmm… a fine offering. A taste of your suffering, a piece of your very being.",
                "Yes… you will carry this wound forever, a reminder of our little bargain.",
                "Go on, then. But know this—what is taken is never truly lost.",
                "I will remember the scent of your sacrifice… and should we meet again, I will hunger once more."
            };

            List<string> option2sentences = new List<string>()
            {
                "Ah… I see. So you wish to keep yourself whole? How… selfish.",
                "Then I shall take all of you instead."
            };

            dialogueManager.StartDialogue(sentences,
                () => dialogueManager.StartFollowUpDialogue(option1sentences, () =>
                {
                    Debug.Log("Próba za³adowania sceny 4...");
                    SceneManager.LoadScene(4);
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