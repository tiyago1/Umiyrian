using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePlayerState : PlayerState
{
    private bool mInvalidInputDetected;

    public IdlePlayerState(PlayerController playerController) : base(playerController)
    {
    }

    public override void OnStateEnter()
    {
        for (int i = 0; i < mPlayerController.Animator.parameters.Length; i++)
        {
            if (mPlayerController.Animator.parameters[i].type == AnimatorControllerParameterType.Trigger)
            {
                mPlayerController.Animator.ResetTrigger(mPlayerController.Animator.parameters[i].name);
            }
        }

        mPlayerController.Animator.SetBool("Move", false);
    }
    public override void Tick()
    {
        //Debug.Log("Idle");
        mInvalidInputDetected =
            (Input.GetKey(KeyCode.W) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))) ||
            (Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))) ||
            (Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A))) ||
            (Input.GetKey(KeyCode.S) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))) ||
            (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) ||
            (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) ||
            (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) ||
            (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            ;


        if (!mInvalidInputDetected)
        {
            Idle(mPlayerController.CurrentPlayerDirection);
        }
        else
        {
            mPlayerController.SetPlayerState(new MovePlayerState(mPlayerController));
        }
    }

    public override void OnStateExit()
    {
        mPlayerController.Animator.ResetTrigger("Idle_" + mPlayerController.CurrentPlayerDirection);
    }

    private void Idle(DirectionType direction)
    {
        mPlayerController.Animator.SetTrigger("Idle_" + direction.ToString());
    }
}
