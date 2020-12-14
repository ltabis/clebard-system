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
        managerRef.SetBarksUIVisible(true);
        managerRef.SetBarksActive(true);
    }
}
