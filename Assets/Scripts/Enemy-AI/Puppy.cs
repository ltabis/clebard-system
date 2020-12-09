using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puppy : Dog
{
    // Start is called before the first frame update
    public Transform minSafeZone;
    public Transform maxSafeZone;
    void Start()
    {
        runSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        if (isInSafeZone())
            print("WIIN");
        if (isScared)
            Scared();
        else if (isCharmed)
            Charmed();
        else
            Sit();

    }

    private bool isInSafeZone()
    {
        //print(" pos chat : " + transform.position + "pos min " + minSafeZone + "pos max : " + maxSafeZone);
        if (transform.position.x > minSafeZone.position.x && transform.position.z > minSafeZone.position.z && transform.position.x < maxSafeZone.position.x && transform.position.z < maxSafeZone.position.z)
            return true;
        return false;
    }

    protected override void Sit()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isSitting", true);
    }
}
