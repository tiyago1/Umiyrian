using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerController mPlayerController;

    public PlayerState(PlayerController playerController)
    {
        mPlayerController = playerController;
    }

    public abstract void OnStateEnter();
    public abstract void Tick();
    public abstract void OnStateExit();
}
