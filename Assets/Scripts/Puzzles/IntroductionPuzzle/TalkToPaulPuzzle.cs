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
    }

    override public void OnStartPuzzle()
    {
        managerRef.SetCurrentObjective("Talk to Paul. Use <E> to interact with characters");
    }
}
