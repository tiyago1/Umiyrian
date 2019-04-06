using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackwardCommand : Command
{
    public BackwardCommand(DirectionType direction, PlayerController playerController) : base(direction, playerController)
    {
    }

    public override void Execute()
    {
        base.Execute();
    }
}