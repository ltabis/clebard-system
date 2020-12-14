using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachArea : Puzzle
{
    [SerializeField]
    List<GameObject> lights = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            foreach (var light in lights)
                light.SetActive(true);
            managerRef.NextPuzzle();
        }
    }

    override public void OnLeavePuzzle() 
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    override public void OnStartPuzzle()
    {
        managerRef.RemoveCurrentObjective();
        managerRef.RemoveCurrentTip();
    }
}
