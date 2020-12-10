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

    private List<GameObject> slots = new List<GameObject>();
    private uint currentBark = 0;

    [SerializeField]
    private float barkTitleFadeTime = 5f;
    private float startFade = 0f;
    private bool showTitle = false;

    void Awake()
    {
        startFade = barkTitleFadeTime;
    }

    void Update()
    {
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
}
