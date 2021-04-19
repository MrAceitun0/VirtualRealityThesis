using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBItem
{
    private System.DateTime timestamp;
    private string type;
    private int totalSnap;
    private int totalTeleport;
    private float totalFree;
    private float percentageFree;
    private float totalWalk;
    private float percentageWalk;
    private float completitionTime;
    private List<LocomotionAction> actions;

    public DBItem(DateTime timestamp, string type, int totalSnap, int totalTeleport, float totalFree, float percentageFree, float totalWalk, float percentageWalk, float completitionTime)
    {
        this.timestamp = timestamp;
        this.type = type;
        this.totalSnap = totalSnap;
        this.totalTeleport = totalTeleport;
        this.totalFree = totalFree;
        this.percentageFree = percentageFree;
        this.totalWalk = totalWalk;
        this.percentageWalk = percentageWalk;
        this.completitionTime = completitionTime;
    }
}
