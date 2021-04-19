using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionAction
{
    private string type;
    private float gameTime;

    public LocomotionAction(string type, float gameTime)
    {
        this.type = type;
        this.gameTime = gameTime;
    }
}
