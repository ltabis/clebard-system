using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private DialogueManager dialogueManager;
    public Dialogue dialogue;
    public GameObject player;
    [SerializeField]
    private NPlayerController playerController;
    public float distance = 1;

    protected bool isSpeaking = false;
    protected bool isBusy = false;
    public KeyCode tempKey = KeyCode.R;
    private bool doneReading = false;

    public void Update()
    {
        isSpeaking = dialogueManager.getIsActive();
        if (!isSpeaking) {
            Vector3 dir = player.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, dir);

            if (isBusy) {
                playerController.EnableControlls(true);
                doneReading = true;
            }

            isBusy = false;
            if (Input.GetKeyDown(tempKey))
                Interact();
        } else if (isBusy)
            if (Input.GetKeyDown(tempKey))
                dialogueManager.DisplayNextSentence();
    }

    public void Interact()
    {
        Vector3 dir = player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, dir);
        
        if (Vector3.Distance(transform.position, player.transform.position) < distance && Mathf.Abs(angle) < 90)
            TriggerDialogue();
    }

    public virtual void TriggerDialogue()
    {
        isBusy = true;
        playerController.EnableControlls(false);
        dialogueManager.StartDialogue(dialogue);
    }

    public bool HasPlayerFinishDialog()
    {
        return doneReading;
    }
}