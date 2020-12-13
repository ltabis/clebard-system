using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject barkSlotBar;
    [SerializeField]
    private GameObject slotSprite;
    [SerializeField]
    private Text barkTitle;
    [SerializeField]
    private GameObject objectiveIndicatorText;
    [SerializeField]
    private GameObject objectiveText;
    [SerializeField]
    private GameObject tipText;

    private List<GameObject> slots = new List<GameObject>();
    private uint currentBark = 0;

    [SerializeField]
    private float barkTitleFadeTime = 5f;
    private float startFade = 0f;
    private bool showTitle = false;
    private bool visible = true;
    private bool readyForSceneTransition = false;

    void Awake()
    {
        startFade = barkTitleFadeTime;
    }

    void Update()
    {
        if (!visible)
            return;
    
        if (startFade >= barkTitleFadeTime && showTitle) {
            barkTitle.CrossFadeAlpha(0, 5, false);
            showTitle = false;
        } else
            startFade += Time.deltaTime;
    }

    public void SetBarkSlot(Sprite barkSprite, string name)
    {
        // create a new slot for a bark.
        GameObject newSlot = Instantiate(slotSprite);
        GameObject newBark = Instantiate(slotSprite);

        newSlot.name = name;

        // create a new bark image.
        Image barkImage = newBark.GetComponent<Image>();
        barkImage.sprite = barkSprite;
        barkImage.color = Color.white;

        // set children to the slot bar.
        newBark.GetComponent<RectTransform>().SetParent(newSlot.transform);
        newSlot.GetComponent<RectTransform>().SetParent(barkSlotBar.transform);

        // activate the ui.
        newBark.SetActive(true);
        newSlot.SetActive(true);

        slots.Add(newSlot);
    }

    public void SetActivebark(uint index)
    {
        if (index >= slots.Count)
            return;

        // changing colors of the UI.
        slots[(int)currentBark].GetComponent<Image>().color = Color.white;
        slots[(int)index].GetComponent<Image>().color = Color.yellow;

        // setting the title of the bark to display.
        barkTitle.text = slots[(int)index].name;
        barkTitle.CrossFadeAlpha(1, 0, false);

        currentBark = index;
        startFade = 0f;
        showTitle = true;
    }

    public void SetUIVisible(bool state)
    {
        // don't bother reactivating the ui twice.
        if (state == visible)
            return;

        // set all slots active/inactive.    
        foreach (var slot in slots)
            slot.SetActive(state);
        visible = state;

        // reset the bark title.
        if (!state) {
            barkTitle.text = "";

        }
    }

    public bool isUIVisible()
    {
        return visible;
    }

    public void SetObjective(string title, string newOjective)
    {
        objectiveIndicatorText.SetActive(true);
        objectiveText.SetActive(true);

        objectiveIndicatorText.GetComponent<Text>().text = "Objective - " + title;
        objectiveText.GetComponent<Text>().text = newOjective;
    }

    public void RemoveObjective()
    {
        objectiveIndicatorText.SetActive(false);
        objectiveText.SetActive(false);
    }

    public void SetTip(string newTip)
    {
        tipText.SetActive(true);

        tipText.GetComponent<Text>().text = "Tip: " + newTip;
    }

    public void RemoveTip()
    {
        tipText.SetActive(false);
    }

    public void OnFadeComplete()
    {
        readyForSceneTransition = true;
    }

    public bool ReadyForSceneTransition {
        get {
            return readyForSceneTransition;
        }
    }
}
