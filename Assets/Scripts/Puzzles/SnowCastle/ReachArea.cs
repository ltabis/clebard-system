using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachArea : Puzzle
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            managerRef.NextPuzzle();
    }

    override public void OnLeavePuzzle() 
    {
    }

    override public void OnStartPuzzle()
    {
        managerRef.RemoveCurrentObjective();
        managerRef.RemoveCurrentTip();
    }
}
