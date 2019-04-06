using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerState : PlayerState
{
    private Dictionary<DirectionType, Command> mMoveCommands;
    private Coroutine mWaitCoroutine;

    public MovePlayerState(PlayerController playerController) : base(playerController)
    {
        InitializeMoveCommands();
    }

    public override void OnStateEnter()
    {
        mWaitCoroutine = null;
        WaitMoveInput(true);
    }
    public override void Tick()
    {
        //Debug.Log("Move");

        if (Input.anyKey && !Input.GetMouseButton(0))
        {
            if (Input.GetKey(KeyCode.W) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S)))
            {
                Move(DirectionType.Forward);
            }
            if (Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            {
                Move(DirectionType.Left);
            }
            if (Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A)))
            {
                Move(DirectionType.Right);
            }
            if (Input.GetKey(KeyCode.S) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)))
            {
                Move(DirectionType.Backward);
            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                Move(DirectionType.UpLeft);
            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                Move(DirectionType.UpRight);
            }
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                Move(DirectionType.DownLeft);
            }
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                Move(DirectionType.DownRight);
            }
        }
        else
        {
            WaitMoveInput(true);
        }
    }

    public override void OnStateExit()
    {
        //mPlayerController.Animator.ResetTrigger("Move_" + mPlayerController.CurrentPlayerDirection);
        WaitMoveInput(false);
    }

    private void InitializeMoveCommands()
    {
        mMoveCommands = new Dictionary<DirectionType, Command>()
        {
            { DirectionType.UpLeft,    new UpLeftCommand     ( DirectionType.UpLeft,    mPlayerController) },
            { DirectionType.Forward,   new ForwardCommand    ( DirectionType.Forward,   mPlayerController) },
            { DirectionType.UpRight,   new UpRightCommand    ( DirectionType.UpRight,   mPlayerController) },
            { DirectionType.Left,      new LeftCommand       ( DirectionType.Left,      mPlayerController) },
            { DirectionType.Right,     new RightCommand      ( DirectionType.Right,     mPlayerController) },
            { DirectionType.DownLeft,  new DownLeftCommand   ( DirectionType.DownLeft,  mPlayerController) },
            { DirectionType.Backward,  new BackwardCommand   ( DirectionType.Backward,  mPlayerController) },
            { DirectionType.DownRight, new DownRightCommand  ( DirectionType.DownRight, mPlayerController) },
        };
    }

    private void Move(DirectionType direction)
    {
        WaitMoveInput(false);
        mMoveCommands[direction].Execute();
    }

    private IEnumerator WaitMoveInput()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        mPlayerController.SetPlayerState(new IdlePlayerState(mPlayerController));
    }

    private void WaitMoveInput(bool isPlay)
    {
        if (isPlay)
        {
            if (mWaitCoroutine == null)
            {
                mWaitCoroutine = mPlayerController.StartCoroutine(WaitMoveInput());
            }
        }
        else
        {
            if (mWaitCoroutine != null)
                mPlayerController.StopCoroutine(mWaitCoroutine);
            mWaitCoroutine = null;
        }
    }
}
