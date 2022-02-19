using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Upgrade
{
    public float Upgrade1, Upgrade2, Upgrade3;

    public Upgrade()
    {
        Upgrade1 = (float)Math.Round(UnityEngine.Random.Range((float)0.01, (float)0.2), 2);
        Upgrade2 = (float)Math.Round(UnityEngine.Random.Range((float)1, 2), 2);
        Upgrade3 = (float)Math.Round(UnityEngine.Random.Range((float)1, 2), 2);
    }
}
