using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManagerPush : DialogueManager
{
    public override void EndDialogue()
    {
        dialogueBox.SetActive(false);
        nameText.text = "";
        dialogueText.text = "";
        isActive = false;

        FindObjectOfType<Spawner>().launch = true;
        Destroy(FindObjectOfType<NPCPush>());
        Destroy(this);
    }
}
