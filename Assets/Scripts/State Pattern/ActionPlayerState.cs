using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlayerState : PlayerState
{
    private Dictionary<DirectionType, MoveCommand> mMoveCommands;
    private DashCommand mDashCommand;
    private Coroutine mWaitCoroutine;
    private bool mValidInputDetected;

    public ActionPlayerState(PlayerController playerController) : base(playerController)
    {
        InitializeCommands();
    }

    public override void OnStateEnter()
    {
        mWaitCoroutine = null;
        WaitMoveInput(true);
    }

    public override void Tick()
    {
        mValidInputDetected =
            (Input.GetKey(KeyCode.W) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))) ||
            (Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))) ||
            (Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A))) ||
            (Input.GetKey(KeyCode.S) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))) ||
            (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) ||
            (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) ||
            (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) ||
            (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) ||
            Input.GetKeyDown(KeyCode.LeftShift) // Dash
            ;

        if (mValidInputDetected)
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
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                mDashCommand.Execute();
            }
        }
        else
        {
            WaitMoveInput(true);
        }
    }

    public override void OnStateExit()
    {
        WaitMoveInput(false);
    }

    private void InitializeCommands()
    {
        mDashCommand = new DashCommand(mPlayerController.CurrentPlayerDirection, mPlayerController);
        mMoveCommands = new Dictionary<DirectionType, MoveCommand>()
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
        mPlayerController.Animator.SetBool("isMove", true);
        WaitMoveInput(false);
        mMoveCommands[direction].Execute();
    }

    private IEnumerator WaitMoveInput()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        mPlayerController.Animator.SetBool("isMove", false);
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
