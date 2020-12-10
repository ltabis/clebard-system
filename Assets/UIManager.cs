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

    public void SetBarkSlot(Sprite barkSprite)
    {
        // create a new slot for a bark.
        GameObject newSlot = Instantiate(slotSprite);
        GameObject newBark = Instantiate(slotSprite);

        // create a new bark image.
        Image barkImage = newBark.GetComponent<Image>();
        barkImage.sprite = barkSprite;

        // set children to the slot bar.
        newBark.GetComponent<RectTransform>().SetParent(newSlot.transform);
        newSlot.GetComponent<RectTransform>().SetParent(barkSlotBar.transform);

        // activate the ui.
        newBark.SetActive(true);
        newSlot.SetActive(true);
    } 
}
