using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public TMP_InputField answerInput;
    public Button submitButton;

    private Queue<string> sentences;
    private string correctAnswer;
    private System.Action onCorrectAnswer;
    private System.Action onWrongAnswer;

    private bool isTyping = false;

    void Start()
    {
        sentences = new Queue<string>();
        submitButton.onClick.AddListener(CheckAnswer);
        answerInput.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Q))
        {
            ShowNextSentence();
        }
    }

    public void StartDialogue(List<string> dialogueLines, string expectedAnswer, System.Action correctAction, System.Action wrongAction)
    {
        sentences.Clear();
        dialoguePanel.SetActive(true);

        foreach (string line in dialogueLines)
        {
            sentences.Enqueue(line);
        }

        correctAnswer = expectedAnswer.ToLower();
        onCorrectAnswer = correctAction;
        onWrongAnswer = wrongAction;

        ShowNextSentence();
    }

    public void ShowNextSentence()
    {
        if (isTyping) return;

        if (sentences.Count == 0)
        {
            ShowAnswerInput();
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

    void ShowAnswerInput()
    {
        answerInput.gameObject.SetActive(true);
        submitButton.gameObject.SetActive(true);
        answerInput.ActivateInputField();
        Cursor.lockState = CursorLockMode.None;
    }

    public void CheckAnswer()
    {
        string playerAnswer = answerInput.text.ToLower().Trim();

        if (playerAnswer == correctAnswer)
        {
            onCorrectAnswer?.Invoke();
        }
        else
        {
            onWrongAnswer?.Invoke();
        }

        answerInput.text = "";
        answerInput.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
        dialoguePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}