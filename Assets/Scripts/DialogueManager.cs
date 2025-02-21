using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public Button option1;
    public Button option2;

    private Queue<string> sentences;
    private string correctAnswer;
    private System.Action onOption1;
    private System.Action onOption2;
    private System.Action onDialogueEnd;

    private bool isTyping = false;

    void Start()
    {
        sentences = new Queue<string>();
        option1.gameObject.SetActive(false);
        option2.gameObject.SetActive(false);
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Q))
        {
            ShowNextSentence();
        }
    }

    public void StartDialogue(List<string> dialogueLines, System.Action option1Action, System.Action option2Action)
    {
        sentences.Clear();
        dialoguePanel.SetActive(true);

        foreach (string line in dialogueLines)
        {
            sentences.Enqueue(line);
        }

        onOption1 = option1Action;
        onOption2 = option2Action;

        ShowNextSentence();
    }

    public void ShowNextSentence()
    {
        if (isTyping) return;

        if (sentences.Count == 0)
        {
            ShowOptions();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        isTyping = false;
    }
    
    void ShowOptions()
    {
        option1.gameObject.SetActive(true);
        option2.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnOption1Selected()
    {
        onOption1?.Invoke();

        option1.gameObject.SetActive(false);
        option2.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
    } 

    public void OnOption2Selected()
    {
        onOption2?.Invoke();

        option1.gameObject.SetActive(false);
        option2.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void StartFollowUpDialogue(List<string> followUpLines, System.Action onEndAction)
    {
        Debug.Log("Rozpoczynam dodatkowy dialog...");

        sentences.Clear();
        foreach (string line in followUpLines)
        {
            sentences.Enqueue(line);
        }
        onDialogueEnd = onEndAction;
        Debug.Log("Liczba zdañ w follow-up dialogu: " + sentences.Count);
        ShowNextFollowUpSentence();
    }

    public void ShowNextFollowUpSentence()
    {
        if (isTyping) return;

        Debug.Log("Liczba zdañ w kolejce: " + sentences.Count);

        if (sentences.Count == 0)
        {
            Debug.Log("Koniec follow-up dialogu, zmieniam scenê...");
            dialoguePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("Wywo³ujê onDialogueEnd...");
            onDialogueEnd?.Invoke();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log("Wyœwietlam follow-up zdanie: " + sentence);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        if (sentences.Count == 0)
        {
            Debug.Log("Koniec follow-up dialogu, zmieniam scenê...");
            StartCoroutine(EndDialogueWithDelay());
        }

    }

    private IEnumerator EndDialogueWithDelay()
    {
        yield return new WaitForSeconds(4f);

        dialoguePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Wywo³ujê onDialogueEnd...");
        onDialogueEnd?.Invoke();
    }

}