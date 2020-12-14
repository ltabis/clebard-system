using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dig : MonoBehaviour
{
    public Animator anim;
    private GameObject[] holes;
    private float diggingTime = 2f;

    void Awake()
    {
        if (holes == null)
            holes = GameObject.FindGameObjectsWithTag("Hole");
        diggingTime = 2f;
    }

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
    }

    private void DogDig()
    {
        foreach (GameObject hole in holes)
        {
            bool playerInArea = hole.GetComponent<DigHole>().PlayerInArea;
            if (playerInArea) {
                anim.Play("IdleDig");
                if (diggingTime > 0)
                {
                    diggingTime -= Time.deltaTime;
                    DigParticle(hole, true);
                }
                else
                {
                    transform.position = hole.GetComponent<DigHole>().TeleportationSameScene();
                    anim.Play("Idle");
                    diggingTime = 2f;
                    DigParticle(hole, false);
                }
            }
        }
    }

    private void DigParticle(GameObject hole, bool status)
    {
        ParticleSystem diggy = hole.GetComponent<ParticleSystem>();

        if (status)
            diggy.Play();
        else
            diggy.Stop();
    }
}
