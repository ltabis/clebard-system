using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToOlardCastle : Puzzle
{
    [SerializeField]
    private GameObject olard;
    [SerializeField]
    private NPC dialog;

    void Update()
    {
        if (dialog.HasPlayerFinishDialog()) {
            managerRef.NextPuzzle();
            olard.SetActive(false);
        }
    }

    override public void OnLeavePuzzle() 
    {
        dialog.enabled = false;
    }

    override public void OnStartPuzzle()
    {
        dialog.enabled = true;
        dialog.TriggerDialogue();
        managerRef.SetBarksActive(false);
        olard.SetActive(true);
    }
}
