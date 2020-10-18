﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownLeftCommand : MoveCommand
{
    public DownLeftCommand(DirectionType direction, PlayerController playerController) : base(direction, playerController)
    {
    }

    public override void Execute()
    {
        base.Execute();
        Vector2 newPosition = mPlayerController.GetPosition() - new Vector2(speed, speed);
        mPlayerController.RigidBody.MovePosition(newPosition);
    }
}