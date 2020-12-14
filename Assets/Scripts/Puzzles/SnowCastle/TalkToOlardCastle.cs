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
        if (dialog.HasPlayerFinishDialog())
            managerRef.NextPuzzle();
    }

    override public void OnLeavePuzzle() 
    {
        dialog.enabled = false;
        olard.SetActive(false);
        managerRef.SetBarksActive(false);
        managerRef.SetBarksUIVisible(false);
    }

    override public void OnStartPuzzle()
    {
        dialog.enabled = true;
        dialog.TriggerDialogue();
        olard.SetActive(true);
    }
}
