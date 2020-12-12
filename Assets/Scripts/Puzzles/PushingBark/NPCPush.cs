using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPush : NPC
{

    public void Update()
    {
        isSpeaking = FindObjectOfType<DialogueManagerPush>().getIsActive();
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
                FindObjectOfType<DialogueManagerPush>().DisplayNextSentence();
        }
    }

    public override void TriggerDialogue()
    {
        isBusy = true;
        FindObjectOfType<DialogueManagerPush>().StartDialogue(dialogue);
    }
}
