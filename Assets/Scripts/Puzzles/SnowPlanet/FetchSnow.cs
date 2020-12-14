using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchSnow : Puzzle
{
    [SerializeField]
    private List<Snow> snowClouds = new List<Snow>();

    void Update()
    {
        foreach (var snowCloud in snowClouds)
            if (snowCloud.HasBeenFound()) {
                snowCloud.Activated = false;
                snowClouds.Remove(snowCloud);
                return;
            }

        if (snowClouds.Count == 0)
            managerRef.NextPuzzle();
    }

    override public void OnLeavePuzzle() 
    {
        managerRef.RemoveCurrentObjective();
    }

    override public void OnStartPuzzle()
    {
        foreach (var snowCloud in snowClouds)
            snowCloud.Activated = true;

        managerRef.SetCurrentObjective("Global barking", "Find some snow for Olard");
    }
}
