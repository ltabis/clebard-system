using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToPaulBarks : Puzzle
{
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
        managerRef.SetPlayerMovements(true);
        managerRef.SetCurrentTip("Use <R> to bark");
    }

    override public void OnStartPuzzle()
    {
        dialog.enabled = true;
        dialog.TriggerDialogue();
        managerRef.SetPlayerMovements(false);
        managerRef.SetBarksActive(false);
        managerRef.SetBarksUIVisible(false);
    }
}
