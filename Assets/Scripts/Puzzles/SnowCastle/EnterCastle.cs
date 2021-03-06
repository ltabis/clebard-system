﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCastle : Puzzle
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
        managerRef.SetCurrentObjective("sneaky beaky like", "Find a way inside the castle");
        managerRef.SetBarksActive(false);
        managerRef.SetBarksUIVisible(false);
    }
}
