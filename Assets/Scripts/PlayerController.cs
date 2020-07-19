using System.Collections;
using System.Collections.Generic;
using Umiyrian.Inputs;
using UnityEngine;

public enum DirectionType
{
    UpLeft,
    Forward,
    UpRight,
    Left,
    Right,
    DownLeft,
    Backward,
    DownRight,
}
public class PlayerController : MonoBehaviour // Instance olabilir heryere referance
{
    public Animator Animator;
    public DirectionType CurrentPlayerDirection;
    public WeaponController CurrentWeaponController;

    [Header("Move Commands")]
    private Dictionary<DirectionType, MoveCommand> mMoveCommands;
    private DashCommand mDashCommand;
    private PlayerActions playerActions;

    private void Start()
    {
        InitializeCommands();
        InitCurrentWeapon();
    }

    void OnEnable()
    {
        // See PlayerActions.cs for this setup.
        playerActions = PlayerActions.CreateWithDefaultBindings();
        //playerActions.Move.OnLastInputTypeChanged += ( lastInputType ) => Debug.Log( lastInputType );
    }


    void OnDisable()
    {
        // This properly disposes of the action set and unsubscribes it from
        // update events so that it doesn't do additional processing unnecessarily.
        playerActions.Destroy();
    }

    private void Update()
    {
        if (playerActions.Move.IsPressed)
        {
            DirectionType direction = ConvertAngleToDirection(playerActions.Move.Angle);
            Move(direction);
        }
        else
        {
            Animator.SetBool("isMove", false);
        }

        if (playerActions.Shoot.IsPressed)
        {
            Shoot();
        }

    }

    #region General Methods

    private void Move(DirectionType direction)
    {
        Animator.SetBool("isMove", true);
        mMoveCommands[direction].Execute();
    }

    private void Shoot()
    {

    }

    #endregion

    #region Initialize Methods

    private void InitCurrentWeapon()
    {
        CurrentWeaponController = this.transform.GetChild(0).GetComponent<WeaponController>();
        CurrentWeaponController.Init();
    }

    private void InitializeCommands()
    {
        mDashCommand = new DashCommand(CurrentPlayerDirection, this);
        mMoveCommands = new Dictionary<DirectionType, MoveCommand>()
        {
            { DirectionType.UpLeft,    new UpLeftCommand     ( DirectionType.UpLeft,    this) },
            { DirectionType.Forward,   new ForwardCommand    ( DirectionType.Forward,   this) },
            { DirectionType.UpRight,   new UpRightCommand    ( DirectionType.UpRight,   this) },
            { DirectionType.Left,      new LeftCommand       ( DirectionType.Left,      this) },
            { DirectionType.Right,     new RightCommand      ( DirectionType.Right,     this) },
            { DirectionType.DownLeft,  new DownLeftCommand   ( DirectionType.DownLeft,  this) },
            { DirectionType.Backward,  new BackwardCommand   ( DirectionType.Backward,  this) },
            { DirectionType.DownRight, new DownRightCommand  ( DirectionType.DownRight, this) },
        };
    }

    #endregion

    #region Helper Methods

    private DirectionType ConvertAngleToDirection(float angle)
    {
        DirectionType direction = DirectionType.Backward;

        if (angle >= 338 || angle <= 23)
            direction = DirectionType.Forward;
        if (angle > 23 && angle <= 68)
            direction = DirectionType.UpRight;
        if (angle > 68 && angle <= 113)
            direction = DirectionType.Right;
        if (angle > 113 && angle <= 158)
            direction = DirectionType.DownRight;
        if (angle > 158 && angle <= 203)
            direction = DirectionType.Backward;
        if (angle > 203 && angle <= 248)
            direction = DirectionType.DownLeft;
        if (angle > 248 && angle <= 293)
            direction = DirectionType.Left;
        if (angle > 293 && angle <= 338)
            direction = DirectionType.UpLeft;

        return direction;
    }

    #endregion
}
