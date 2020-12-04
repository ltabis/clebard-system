using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //public string[] sentences;
    public Text nameText;
    public Text dialogueText;
    private GameObject dialogueBox;
    private bool isActive = false;

    public Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = GameObject.Find("DialogueBox");
        sentences = new Queue<string>();
        dialogueBox.SetActive(false);
    }

    public void StartDialogue (Dialogue dialogue)
    {
        dialogueBox.SetActive(true);
        nameText.text = dialogue.name;
        isActive = true;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        nameText.text = "";
        dialogueText.text = "";
        isActive = false;
    }

    public bool getIsActive()
    {
        return isActive;
    }
}