using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    private GameObject interactionBox;
    public Text interactionText;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        interactionBox = GameObject.Find("InteractBox");
        interactionBox.SetActive(false);
    }

    public void StartInteraction(string interaction)
    {
        interactionBox.SetActive(true);
        interactionText.text = interaction;
        isActive = true;
    }

    public void EndInteraction()
    {
        interactionBox.SetActive(false);
        interactionText.text = "";
        isActive = false;
    }

    public bool getIsActive()
    {
        return isActive;
    }
}
