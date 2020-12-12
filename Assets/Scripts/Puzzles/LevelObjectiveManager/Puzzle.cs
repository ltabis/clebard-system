using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public LevelObjectiveManger managerRef;

    virtual public void OnLeavePuzzle() {}
    virtual public void OnStartPuzzle() {}
}
