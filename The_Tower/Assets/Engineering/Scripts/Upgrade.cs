using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Upgrade
{
    public float Upgrade1, Upgrade2, Upgrade3;
    public int Upgrade1Type, Upgrade2Type, Upgrade3Type;

    /*
    0 - HP Restore
    1 - HP Cap Increase
    2 - Jump Upgrade
    3 - SPD Upgrade
    4 - Accel Upgrade
    */
    public Upgrade()
    {
        Upgrade2Type = findType();
        Upgrade2 = UpgradeFactory(Upgrade1Type);
        Upgrade2Type = findType();
        Upgrade2 = UpgradeFactory(Upgrade2Type);
        Upgrade3Type = findType();
        Upgrade3 = UpgradeFactory(Upgrade3Type);
    }

    private float UpgradeFactory(int type) {
        switch(type) {
        case 0:
        return 0f;
        case 1:
        return 0f;
        case 2:
        return (float)Math.Round(UnityEngine.Random.Range((float).1, .5f), 2);
        case 3:
        return (float)Math.Round(UnityEngine.Random.Range((float)1, 2), 2);
        case 4:
        return (float)Math.Round(UnityEngine.Random.Range((float)0.01, (float)0.2), 2);
        default:
            return 0f;
        }
    }

    private int findType() {
        int ranNum = UnityEngine.Random.Range(0, 100);
        if (ranNum < 10) return 0;
        else if (ranNum < 15) return 1;
        else if (ranNum < 50) return 2;
        else if (ranNum < 85) return 3;
        else return 4;
    }
}
