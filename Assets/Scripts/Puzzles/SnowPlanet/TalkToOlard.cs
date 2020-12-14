using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToOlard : Puzzle
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
        managerRef.RemoveCurrentTip();
    }

    override public void OnStartPuzzle()
    {
        dialog.enabled = true;
        managerRef.SetBarksActive(false);
        managerRef.SetCurrentObjective("Freezy", "Look around for inhabitants");
    }
}
