using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;
    private GameObject player;
    public float distance = 1;

    private bool isSpeaking = false;
    private bool isBusy = false;
    public KeyCode tempKey = KeyCode.R;

    public void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Update()
    {
        isSpeaking = FindObjectOfType<DialogueManager>().getIsActive();
        if (!isSpeaking)
        {
            Vector3 dir = player.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, dir);
  
            isBusy = false;
            if (Input.GetKeyDown(tempKey))
                Interact();
        }
        else if (isBusy)
        {
            if (Input.GetKeyDown(tempKey))
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