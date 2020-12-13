using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToPaulPuzzle : Puzzle
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
        managerRef.SetBarksActive(false);
        managerRef.SetCurrentObjective("Your ship is damaged. Find help. Use <E> to interact with characters");
    }
}
