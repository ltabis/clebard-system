using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggressive : Mobile
{
    public float sightRange;
    protected bool playerInSightRange;

    protected override void Wander()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        if (isScared)
            Scared();
        else if (isCharmed)
            Charmed();
        else if (!playerInSightRange)
            Patroling();
        else if (playerInSightRange)
            ChasePlayer();
    }
}
