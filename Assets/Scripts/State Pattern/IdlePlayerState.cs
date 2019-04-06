using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePlayerState : PlayerState
{
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
        //throw new NotImplementedException();
    }
    public override void Tick()
    {
        //Debug.Log("Idle");
        Idle(mPlayerController.CurrentPlayerDirection);

        if (Input.anyKey && !Input.GetMouseButton(0))
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
