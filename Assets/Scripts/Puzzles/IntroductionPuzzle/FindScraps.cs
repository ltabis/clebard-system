using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindScraps : MonoBehaviour
{
    [SerializeField]
    private List<Scrap> scraps = new List<Scrap>();
    [SerializeField]
    private UIManager ui;

    void Start()
    {
        ui.SetObjective("Find scraps. You can use your sniffing mode <A> to reveal important items.");
    }

   void Update()
   {
       checkFoundScraps();
   }

   void checkFoundScraps()
   {
        foreach (var scrap in scraps) {
            if (!scrap.HasBeenFound())
            return;
        }

        Debug.Log("All scraps found !");
   }
}
