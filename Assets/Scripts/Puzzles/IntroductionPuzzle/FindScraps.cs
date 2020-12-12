using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindScraps : MonoBehaviour
{
    [SerializeField]
    private List<Scrap> scraps = new List<Scrap>();
    [SerializeField]
    private UIManager ui;

    private uint scrapsFound = 0;

    void Start()
    {
        ui.SetObjective("Find " + scraps.Count + " more scraps. You can use your sniffing mode <A> to reveal important items.");
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
            ui.SetObjective("Find " + (scraps.Count - scrapsFound) + " more scraps. You can use your sniffing mode <A> to reveal important items.");
        }

        if (countFoundScraps == scraps.Count) {
            Debug.Log("All scraps found !");
            ui.RemoveObjective();
        }
   }
}
