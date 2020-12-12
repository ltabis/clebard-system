using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectiveManger : MonoBehaviour
{
    [SerializeField]
    protected List<Puzzle> puzzles = new List<Puzzle>();
    protected int currentPuzzle = 0;

    [SerializeField]
    protected UIManager ui;

    void Awake()
    {
        // initiating all puzzles.
        foreach (var puzzle in puzzles) {
            puzzle.managerRef = this;
            puzzle.enabled = false;
        }

        puzzles[currentPuzzle].enabled = true;
        puzzles[currentPuzzle].OnStartPuzzle();
    }

    public void SetCurrentObjective(string objective)
    {
        ui.SetObjective(objective);
    }

    public void RemoveCurrentObjective()
    {
        ui.RemoveObjective();
    }

    public void NextPuzzle()
    {
        // disabling current puzzle.
        puzzles[currentPuzzle].OnLeavePuzzle();
        puzzles[currentPuzzle++].enabled = false;

        if (currentPuzzle >= puzzles.Count) {
            Debug.Log("End of level");
            return;
        }

        // starting new puzzle.
        puzzles[currentPuzzle].enabled = true;
        puzzles[currentPuzzle].OnStartPuzzle();
    }

    public void SetPuzzle(int index)
    {
        currentPuzzle = index;
    }
}
