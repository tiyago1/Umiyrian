public abstract class MoveCommand : Command
{
    protected const float speed = 0.04f;
    public MoveCommand(DirectionType direction, PlayerController playerController) : base(direction, playerController)
    {
    }

    public override void Execute()
    {
        base.Execute();

        float x = 0;
        float y = 0;
        GetVector2ToDirection(ref x,  ref y);
        mPlayerController.Animator.SetBool("isMove", true);
        mPlayerController.Animator.SetFloat("X", x);
        mPlayerController.Animator.SetFloat("Y", y);
        mPlayerController.CurrentPlayerDirection = mDirection;
    }

    private void GetVector2ToDirection(ref float x, ref float y)
    {
        switch (mDirection)
        {
            case DirectionType.UpLeft:
                x = -1;
                y = 1;
                break;
            case DirectionType.Forward:
                x = 0;
                y = 1;
                break;
            case DirectionType.UpRight:
                x = 1;
                y = 1;
                break;
            case DirectionType.Left:
                x = -1;
                y = 0;
                break;
            case DirectionType.Right:
                x = 1;
                y = 0;
                break;
            case DirectionType.DownLeft:
                x = -1;
                y = -1;
                break;
            case DirectionType.Backward:
                x = 0;
                y = -1;
                break;
            case DirectionType.DownRight:
                x = 1;
                y = -1;
                break;
        }
    }
}
