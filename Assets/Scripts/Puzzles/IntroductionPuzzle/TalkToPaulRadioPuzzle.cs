using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToPaulRadioPuzzle : Puzzle
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
        dialog.enabled = true;
        managerRef.SetCurrentObjective("Get back to Paul. Remember, use <E> to interact with characters");
    }
}
