using UnityEngine;

public class DashCommand : Command
{
    private float mDashValue = 0.085f;

    public DashCommand(DirectionType direction, PlayerController playerController) : base(direction, playerController)
    {

    }

    public override void Execute()
    {
        base.Execute();
        Vector2 dashDistance = DirectionToVector2(mPlayerController.CurrentPlayerDirection);
        Vector2 newPosition = mPlayerController.GetPosition() + dashDistance;
        mPlayerController.rigidBody.MovePosition(newPosition);

    }

    private Vector2 DirectionToVector2(DirectionType type)
    {
        Vector2 direction = Vector2.zero;
        switch (type)
        {
            case DirectionType.UpLeft:
                direction = new Vector2(-1 * mDashValue, mDashValue);
                break;
            case DirectionType.Forward:
                direction = new Vector2(0, mDashValue);
                break;
            case DirectionType.UpRight:
                direction = new Vector2(mDashValue, mDashValue);
                break;
            case DirectionType.Left:
                direction = new Vector2(-1 * mDashValue, 0);
                break;
            case DirectionType.Right:
                direction = new Vector2(mDashValue, 0);
                break;
            case DirectionType.DownLeft:
                direction = new Vector2(-1 * mDashValue, -1 * mDashValue);
                break;
            case DirectionType.Backward:
                direction = new Vector2(0, -1 * mDashValue);
                break;
            case DirectionType.DownRight:
                direction = new Vector2(mDashValue, -1 * mDashValue);
                break;
            default:
                break;
        }
        return direction;
    }
}
