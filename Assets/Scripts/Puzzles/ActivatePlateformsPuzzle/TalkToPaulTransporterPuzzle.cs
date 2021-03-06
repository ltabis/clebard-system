﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToPaulTransporterPuzzle : Puzzle
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
    }

    override public void OnStartPuzzle()
    {
        dialog.enabled = true;
        managerRef.SetPlayerMovements(false);
        dialog.TriggerDialogue();
        managerRef.RemoveCurrentTip();
        managerRef.RemoveCurrentObjective();
    }
}
