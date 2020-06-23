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
        
    }
}
