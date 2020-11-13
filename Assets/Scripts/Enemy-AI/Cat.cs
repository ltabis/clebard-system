using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Peaceful
{
    protected override void Trot()
    {
        anim.Play("Walk");
        agent.speed = trotSpeed;
    }

}