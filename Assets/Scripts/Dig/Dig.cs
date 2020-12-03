using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dig : MonoBehaviour
{
    public Animator anim;
    private GameObject[] holes;
    private float diggingTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        if (holes == null)
            holes = GameObject.FindGameObjectsWithTag("Hole");
        diggingTime = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            DogDig();
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("IdleDig"))
        {
            diggingTime -= Time.deltaTime;
            if (diggingTime < 0)
                DogDig();
        }
        else
        {
            diggingTime = 2f;
        }
       // if (diggingTime < 0)
         //   DogDig();

    }

    private void DogDig()
    {
        Vector3 dir;
        float angle;
        foreach (GameObject hole in holes)
        {
            dir = hole.transform.position - transform.position;
            angle = Vector3.Angle(transform.forward, dir);
            if (Vector3.Distance(transform.position, hole.transform.position) < 1 && Mathf.Abs(angle) < 90)
            {
                anim.Play("IdleDig");
                if (diggingTime > 0)
                {
                    diggingTime -= Time.deltaTime;
                    //DigParticle(hole);
                }
                else
                {
                    GetComponent<SC_TPSController>().Teleportation(hole.GetComponent<DigHole>().Teleportation());
                    anim.Play("Idle");
                    diggingTime = 2f;
                }
            }
        }
    }

    private void DigParticle(GameObject hole)
    {
        ParticleSystem diggy = hole.GetComponent<ParticleSystem>();

        diggy.Play();
    }
}
