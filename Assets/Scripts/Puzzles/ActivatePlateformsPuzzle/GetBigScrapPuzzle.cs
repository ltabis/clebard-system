using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBigScrapPuzzle : Puzzle
{
    [SerializeField]
    private Scrap scrap;

    override public void OnLeavePuzzle()
    {
        managerRef.RemoveCurrentObjective();
        managerRef.RemoveCurrentTip();
    }

    override public void OnStartPuzzle()
    {
        scrap.gameObject.SetActive(true);

        managerRef.SetCurrentObjective("Space dump", "Go on SpaceDump and go get some scraps");
        managerRef.SetCurrentTip("You can use 'Transporters' to easly travel between planets");
    }

   void Update()
   {
       checkFoundScraps();
   }

   void checkFoundScraps()
   {
        if (scrap.HasBeenFound()) {
            managerRef.RemoveCurrentObjective();
            managerRef.NextPuzzle();
        }
   }
}
