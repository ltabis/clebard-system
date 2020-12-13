using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlateformsPuzzle : Puzzle
{
    [SerializeField]
    private Transporter transporter;
    [SerializeField]
    private List<Plateform> plateforms = new List<Plateform>();

    void Update()
    {
        uint countActivatedPlaterforms = 0;

        foreach (var plateform in plateforms)
            if (plateform.IsActivated)
                ++countActivatedPlaterforms;

        if (countActivatedPlaterforms != plateforms.Count) {
            managerRef.SetCurrentObjective("Switches", "Activate " + (plateforms.Count - countActivatedPlaterforms) + " more Transporter's switches.");
        } else {
            managerRef.RemoveCurrentObjective();
            managerRef.RemoveCurrentTip();
            transporter.Active = true;
            managerRef.NextPuzzle();
        }
    }

    override public void OnLeavePuzzle() 
    {
        managerRef.RemoveCurrentObjective();
    }

    override public void OnStartPuzzle()
    {
        foreach (var plateform in plateforms)
            plateform.EnablePlateform();

        managerRef.SetCurrentObjective("Switches", "Activate " + plateforms.Count +  " more Transporter's switches");
        managerRef.SetCurrentTip("Remember, you can use your Sniffing Mode <A> to reveal important items");
    }
}
