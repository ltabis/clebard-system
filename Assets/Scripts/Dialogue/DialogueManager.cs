using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public GameObject dialogueBox;
    [SerializeField]
    private UIManager playerUI;
    protected bool isActive = false;

    public Queue<string> sentences;

    void Start()
    {
        //dialogueBox = GameObject.Find("DialogueBox");
        sentences = new Queue<string>();
        dialogueBox.SetActive(false);
    }

    public void StartDialogue (Dialogue dialogue)
    {
        playerUI.SetUIVisible(false);
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

    public virtual void EndDialogue()
    {
        dialogueBox.SetActive(false);
        nameText.text = "";
        dialogueText.text = "";
        isActive = false;
        playerUI.SetUIVisible(true);
    }

    public bool getIsActive()
    {
        return isActive;
    }
}