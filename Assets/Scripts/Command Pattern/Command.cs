public abstract class Command
{
    protected DirectionType mDirection;
    protected PlayerController mPlayerController;

    public Command(DirectionType direction, PlayerController playerController)
    {
        mPlayerController = playerController;
        mDirection = direction;
    }

    public virtual void Execute()
    {
        for (int i = 0; i < mPlayerController.Animator.parameters.Length; i++)
        {
            if (mPlayerController.Animator.parameters[i].name != mDirection.ToString() &&
                mPlayerController.Animator.parameters[i].type == UnityEngine.AnimatorControllerParameterType.Trigger)
            {
                mPlayerController.Animator.ResetTrigger(mPlayerController.Animator.parameters[i].name);
            }
        }

        mPlayerController.Animator.SetTrigger("Move_" + mDirection.ToString());
        mPlayerController.CurrentPlayerDirection = mDirection;
        mPlayerController.Animator.SetBool("Move", true);
    }
}
