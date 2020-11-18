using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peaceful : Mobile
{
    protected override void Wander()
    {
        if (isScared)
            Scared();
        else if (isCharmed)
            Charmed();
        else
            Patroling();
    }
}
