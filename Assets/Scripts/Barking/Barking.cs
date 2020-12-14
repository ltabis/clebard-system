using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barking : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private UIManager UI;
    public AudioSource[] barks;
    public Sprite[] barkSprites;
    public int activeBark = 0;
    // Start is called before the first frame update
    public float pushDistance = 10;
    public float pushPower = 10;

    public float charmDistance = 10;
    public float charmTime = 5;

    public float scareDistance = 10;
    public float scareTime = 5;
    public float[] cooldownTab;

    [SerializeField]
    private Transform player;
    enum BarkType : int
    {
        Push = 0,
        Charm = 1,
        Scare = 2
    }
    bool isActive = true;

    void Awake()
    {
        Debug.Assert(
            barks.Length == barkSprites.Length,
            "There isn't the same number of bark sprites and bark audio sources."
        );

        for (uint i = 0; i < barks.Length; ++i)
            UI.SetBarkSlot(barkSprites[i], barks[i].name);

        UI.SetActivebark((uint)activeBark);

        player = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
            return;
        if (Input.GetKeyDown(KeyCode.R))
            PlayBarks();
        if (Input.GetKeyDown(KeyCode.Tab))
            ChangeBarks();
    }

    private void ChangeBarks()
    {
        activeBark += 1;
        if (activeBark == barks.Length)
            activeBark = 0;

        UI.SetActivebark((uint)activeBark);
    }

    private void PlayBarks()
    {
        barks[activeBark].Play();
        anim.Play("IdleBarking");
        if (activeBark == (int)BarkType.Push)
            PushBark();
        if (activeBark == (int)BarkType.Charm)
            CharmBark();
        if (activeBark == (int)BarkType.Scare)
            ScareBark();
    }

    private void PushBark()
    {
        Entity[] animalFound = GameObject.FindObjectsOfType<Entity>();
        Vector3 dir;
        float angle;
        float mass;
        for (int i = 0; i < animalFound.Length; i++)
        {
            if (animalFound[i].woBarks[(int)BarkType.Push] == true && Vector3.Distance(player.transform.position, animalFound[i].transform.position) <= pushDistance)
            {
                dir = animalFound[i].transform.position - player.transform.position;
                angle = Vector3.Angle(player.transform.forward, dir);
                mass = animalFound[i].GetComponent<Rigidbody>().mass;

                if (Mathf.Abs(angle) < 90)
                {
                    animalFound[i].GetComponent<Rigidbody>().AddForce(dir.normalized * pushPower * mass, ForceMode.Impulse);
                }

            }
        }
    }

    private void CharmBark()
    {
        Mobile[] animalFound = GameObject.FindObjectsOfType<Mobile>();
        for (int i = 0; i < animalFound.Length; i++)
        {
            if (animalFound[i].woBarks[(int)BarkType.Charm] == true && Vector3.Distance(player.transform.position, animalFound[i].transform.position) <= charmDistance)
            {
                animalFound[i].SetIsCharmed(true);
                animalFound[i].SetTimeCharmed(charmTime);

            }
        }
    }

   private void ScareBark()
    {
        Mobile[] animalFound = GameObject.FindObjectsOfType<Mobile>();
        for (int i = 0; i < animalFound.Length; i++)
        {
            if (animalFound[i].woBarks[(int)BarkType.Scare] == true && Vector3.Distance(player.transform.position, animalFound[i].transform.position) <= scareDistance)
            {
                animalFound[i].SetIsScared(true);
                animalFound[i].SetTimeScared(scareTime);

            }
        }
    }

    public void SetBarkActive(bool state)
    {
        isActive = state;
    }

    public void SetBarkUIVisible(bool state)
    {
        UI.SetUIVisible(state);
    }
}
