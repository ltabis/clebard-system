using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToStash : Puzzle
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            managerRef.NextPuzzle();
    }

    override public void OnLeavePuzzle() 
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    override public void OnStartPuzzle()
    {
        managerRef.SetCurrentObjective("sneaky beaky like", "Find Olard's stash and steal his scrap");
        managerRef.SetCurrentTip("Use <R> to push objects by barking");
        managerRef.SetBarksUIVisible(true);
        managerRef.SetBarksActive(true);
    }
}
