using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelObjectiveManger : MonoBehaviour
{
    [SerializeField]
    private List<Puzzle> puzzles = new List<Puzzle>();
    private int currentPuzzle = 0;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private UIManager uiScript;
    [SerializeField]
    private Barking barkingScript;
    [SerializeField]
    private NPlayerController controllerScript;
    [SerializeField]
    private string nextScene = "";
    private bool changingScene = false;

    void Awake()
    {
        Cursor.visible = false;

        // initiating all puzzles.
        foreach (var puzzle in puzzles) {
            puzzle.managerRef = this;
            puzzle.enabled = false;
        }

        puzzles[currentPuzzle].enabled = true;
        puzzles[currentPuzzle].OnStartPuzzle();
    }

    void Update()
    {
        if (changingScene)
            SwapLevel();
    }

    public void SetCurrentObjective(string title, string objective)
    {
        uiScript.SetObjective(title, objective);
    }

    public void SetCurrentTip(string tip)
    {
        uiScript.SetTip(tip);
    }

    public void RemoveCurrentObjective()
    {
        uiScript.RemoveObjective();
    }
    public void RemoveCurrentTip()
    {
        uiScript.RemoveTip();
    }

    public void NextPuzzle()
    {
        // disabling current puzzle.
        puzzles[currentPuzzle].OnLeavePuzzle();
        puzzles[currentPuzzle++].enabled = false;

        if (currentPuzzle >= puzzles.Count) {
            changingScene = true;
            FadeOut();
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

    public void SetBarksActive(bool state)
    {
        barkingScript.enabled = state;
    }

    public void SetPlayerMovements(bool state)
    {
        controllerScript.EnableControlls(state);
    }

    public void SetPlayerVelocity(bool state)
    {
        controllerScript.EnableVelocity(state);
    }

    public void SetPlayerPosition(Vector3 position)
    {
        controllerScript.transform.position = position;
    }

    public void SwapLevel()
    {
        if (nextScene.Length != 0 && uiScript.ReadyForSceneTransition)
            SceneManager.LoadScene(nextScene);
        else if (nextScene.Length == 0)
            Application.Quit();
    }
    public void FadeOut()
    {
        uiScript.GetComponent<Animator>().SetTrigger("FadeOut");
    }
}
