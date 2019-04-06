using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownRightCommand : Command
{
    public DownRightCommand(DirectionType direction, PlayerController playerController) : base(direction, playerController)
    {
    }

    public override void Execute()
    {
        base.Execute();
    }
}