using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpRightCommand : Command
{
    public UpRightCommand(DirectionType direction, PlayerController playerController) : base(direction, playerController)
    {
    }

    public override void Execute()
    {
        base.Execute();
        mPlayerController.transform.position += new Vector3(0.05f, 0.05f, 0.0f);
    }
}