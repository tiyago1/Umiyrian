using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpLeftCommand : MoveCommand
{
    public UpLeftCommand(DirectionType direction, PlayerController playerController) : base(direction, playerController)
    {
    }

    public override void Execute()
    {
        base.Execute();
        Vector2 newPosition = mPlayerController.GetPosition() + new Vector2(-speed, speed);
        mPlayerController.rigidBody.MovePosition(newPosition);
    }
}