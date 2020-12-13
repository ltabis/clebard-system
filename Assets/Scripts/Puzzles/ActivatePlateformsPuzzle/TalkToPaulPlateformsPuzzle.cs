using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToPaulPlateformsPuzzle : Puzzle
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
        managerRef.RemoveCurrentObjective();
    }

    override public void OnStartPuzzle()
    {
        dialog.enabled = true;
        dialog.TriggerDialogue();
        managerRef.SetBarksActive(false);
        managerRef.SetCurrentObjective("Instructions", "Listen to Paul's advices");
        managerRef.RemoveCurrentTip();
    }
}
