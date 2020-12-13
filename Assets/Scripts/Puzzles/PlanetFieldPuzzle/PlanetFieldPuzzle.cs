using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFieldPuzzle : Puzzle
{
    [SerializeField]
    private List<TriggerArea> deathAreas = new List<TriggerArea>();
    [SerializeField]
    private Vector3 initialArea;

    void Update()
    {
        foreach (var deathArea in deathAreas)
            if (deathArea.HasBeenEntered)
                Respawn();
    }

    void Respawn()
    {
        managerRef.SetPlayerPosition(initialArea);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            managerRef.NextPuzzle();
    }

    override public void OnLeavePuzzle() 
    {
        managerRef.RemoveCurrentObjective();
        managerRef.RemoveCurrentTip();
    }

    override public void OnStartPuzzle()
    {
        managerRef.SetCurrentObjective("Flying dog", "Make your way across the planet field");
        managerRef.SetCurrentTip("Gather momentum by running. Use it to jump higher");
    }
}
