using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;
    private GameObject player;
    public float distance = 1;

    public GameObject quest;
    public bool gotQuest = false;
    public float questY;

    private bool isSpeaking = false;
    private bool isBusy = false;

    public void Start()
    {
        Vector3 questPos;

        if (gotQuest)
        {
            questPos.x = transform.position.x;
            questPos.y = transform.position.y + questY;
            questPos.z = transform.position.z;
            quest.transform.position = questPos;
        }
        player = GameObject.Find("Player");
    }

    public void Update()
    {
        isSpeaking = FindObjectOfType<DialogueManager>().getIsActive();
        if (!isSpeaking)
        {
            isBusy = false;
            if (Input.GetKeyDown(KeyCode.R))
                Interact();
        }
        else if (isBusy)
        {
            if (Input.GetKeyDown(KeyCode.R))
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }

    public void Interact()
    {
        Vector3 dir = player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, dir);
        
        if (Vector3.Distance(transform.position, player.transform.position) < distance && Mathf.Abs(angle) < 90)
            TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        isBusy = true;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}