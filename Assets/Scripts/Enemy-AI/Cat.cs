using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Peaceful
{
    protected bool isSleeping = false;
    protected float sleepTime;

    protected override void Update()
    {
        if (isScared)
            Scared();
        else if (isSleeping)
            Sleeping();
        else if (isCharmed)
            Charmed();
        else
            Patroling();
    }

    protected override void Trot()
    {
        anim.Play("Walk");
        agent.speed = trotSpeed;
    }

    protected override void RandomAnim()
    {
        isSleeping = true;
        sleepTime = Random.Range(8, 16);
        Sleep();
    }

    protected void Sleeping()
    {
        if (sleepTime > 0)
            sleepTime -= Time.deltaTime;
        else
            isSleeping = false;
    }

    private void Sleep()
    {
        anim.Play("Sleep");
    }

    /*protected override void CharmParticle(bool play)
    {
        ParticleSystem charmParticle = gameObject.GetComponent<ParticleSystem>();
        if (play)
            charmParticle.Play();
        else
            charmParticle.Clear();
    }*/
}