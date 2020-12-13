using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCroquettesPuzzle : Puzzle
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

        managerRef.SetCurrentObjective("VIP", "Get the PREMIUM GOLD CROQUETTES™ for Paul");
        managerRef.SetCurrentTip("Don't trust dogs");
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
