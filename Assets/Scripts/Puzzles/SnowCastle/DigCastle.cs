using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DigCastle : Puzzle
{
    [SerializeField]
    private List<NPC> dialogs = new List<NPC>();

    [SerializeField]
    private List<GameObject> holes = new List<GameObject>();
    [SerializeField]
    private GameObject fade;
    private int currentAction = 0;
    private bool lastDialogue = false;

    void Update()
    {
        if (lastDialogue) {
            if (dialogs[currentAction].HasPlayerFinishDialog())
                managerRef.NextPuzzle();
            return;
        }

        if (holes[currentAction].GetComponent<DigHole>().HasBeenDug) {
            ++currentAction;
            if (currentAction >= holes.Count) {
                fade.SetActive(true);
                dialogs[currentAction].enabled = true;
                dialogs[currentAction].TriggerDialogue();
                lastDialogue = true;
                return;
            }
            dialogs[currentAction].enabled = true;
            dialogs[currentAction].TriggerDialogue();
            holes[currentAction].SetActive(true);
        }
    }

    override public void OnLeavePuzzle() 
    {
        managerRef.RemoveCurrentObjective();
        managerRef.RemoveCurrentTip();
    }

    override public void OnStartPuzzle()
    {
        for (int i = 1; i < holes.Count; ++i)
            holes[i].SetActive(false);

        managerRef.SetCurrentObjective("King Slayer", "Find where Olard whent");
        managerRef.SetCurrentTip("Find burrows and use <T> to dig");
    }
}
