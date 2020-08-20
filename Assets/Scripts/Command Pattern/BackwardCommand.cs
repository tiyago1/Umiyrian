using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackwardCommand : MoveCommand
{
    public BackwardCommand(DirectionType direction, PlayerController playerController) : base(direction, playerController)
    {
    }

    public override void Execute()
    {
        base.Execute();
        Vector2 newPosition = mPlayerController.GetPosition() - new Vector2(0.0f, speed);
        mPlayerController.rigidBody.MovePosition(newPosition);
    }
}