﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpLeftCommand : Command
{
    public UpLeftCommand(DirectionType direction, PlayerController playerController) : base(direction, playerController)
    {
    }

    public override void Execute()
    {
        base.Execute();
    }
}