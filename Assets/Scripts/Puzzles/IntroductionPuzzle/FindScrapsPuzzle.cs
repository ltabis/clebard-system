using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindScrapsPuzzle : Puzzle
{
    [SerializeField]
    private List<Scrap> scraps = new List<Scrap>();
    private uint scrapsFound = 0;

    override public void OnLeavePuzzle()
    {
        managerRef.RemoveCurrentObjective();
        managerRef.RemoveCurrentTip();
    }

    override public void OnStartPuzzle()
    {
        foreach (var scrap in scraps)
            scrap.gameObject.SetActive(true);

        managerRef.SetCurrentObjective("Fetch scrap", "Find " + scraps.Count + " more scraps for Paul.");
        managerRef.SetCurrentTip("You can use your Sniffing Mode <A> to reveal important items");
    }

   void Update()
   {
       checkFoundScraps();
   }

   void checkFoundScraps()
   {
        uint countFoundScraps = 0;

        foreach (var scrap in scraps)
            if (scrap.HasBeenFound())
                ++countFoundScraps;

        if (countFoundScraps != scrapsFound) {
            scrapsFound = countFoundScraps;
            managerRef.SetCurrentObjective("Fetch scrap", "Find " + (scraps.Count - scrapsFound) + " more scraps for Paul.");
        }

        if (countFoundScraps == scraps.Count) {
            managerRef.RemoveCurrentObjective();
            managerRef.NextPuzzle();
        }
   }
}
