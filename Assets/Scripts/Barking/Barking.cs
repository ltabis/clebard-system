using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barking : MonoBehaviour
{
    public AudioSource[] barks;
    public int activeBark = 0;
    // Start is called before the first frame update
    public float pushDistance = 10;
    public float pushPower = 10;

    public float charmDistance = 10;
    public float charmTime = 5;

    public float scareDistance = 10;
    public float scareTime = 5;
    public float[] cooldownTab;

    enum BarkType : int
    {
        Push = 0,
        Charm = 1,
        Scare = 2
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            PlayBarks();
        if (Input.GetKeyDown(KeyCode.Tab))
            ChangeBarks();

    }

    private void ChangeBarks()
    {
        activeBark += 1;
        if (activeBark == barks.Length)
            activeBark = 0;
        print("ACTIVE BARK : " + activeBark);
    }

    private void PlayBarks()
    {
        barks[activeBark].Play();
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
            if (animalFound[i].woBarks[(int)BarkType.Push] == true && Vector3.Distance(transform.position, animalFound[i].transform.position) <= pushDistance)
            {
                dir = animalFound[i].transform.position - transform.position;
                angle = Vector3.Angle(transform.forward, dir);
                mass = animalFound[i].GetComponent<Rigidbody>().mass;

                if (Mathf.Abs(angle) < 90)
                    animalFound[i].GetComponent<Rigidbody>().AddForce(dir.normalized * pushPower * mass, ForceMode.Impulse);

            }
        }
    }

    private void CharmBark()
    {
        Mobile[] animalFound = GameObject.FindObjectsOfType<Mobile>();
        for (int i = 0; i < animalFound.Length; i++)
        {
            if (animalFound[i].woBarks[(int)BarkType.Charm] == true && Vector3.Distance(transform.position, animalFound[i].transform.position) <= charmDistance)
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
            if (animalFound[i].woBarks[(int)BarkType.Scare] == true && Vector3.Distance(transform.position, animalFound[i].transform.position) <= scareDistance)
            {
                animalFound[i].SetIsScared(true);
                animalFound[i].SetTimeScared(scareTime);

            }
        }
    }
}
