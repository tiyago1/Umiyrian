using InControl;
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
public class PlayerController : MonoBehaviour, IMoveable // Instance olabilir heryere referance
{
    public Animator Animator;
    public DirectionType CurrentPlayerDirection;
    public WeaponController CurrentWeaponController;
    public Rigidbody2D rigidBody;

    [Header("Move Commands")]
    private Dictionary<DirectionType, MoveCommand> mMoveCommands;
    private DashCommand mDashCommand;
    private PlayerActions playerActions;

    public GameObject crosshair;
    public InControlInputModule incontrolInput;

    public Vector2 GetPosition() => new Vector2(this.transform.position.x, this.transform.position.y);

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
        Move();
        Shoot();
        Aim();
        Dash();
    }

    private void Aim()
    {
        if (playerActions.Aim.LastInputType == BindingSourceType.MouseBindingSource)
        {
            MouseAim(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else
        {
            GamepadAim(new Vector2(this.transform.position.x, this.transform.position.y) + playerActions.Aim.Vector);
        }

        SetupWeaponRotation();
    }

    private void MouseAim(Vector2 value)
    {
        CurrentWeaponController.gameObject.transform.localPosition = GetAimVector(value, 1.0f);
        crosshair.transform.position = value;
    }

    private void GamepadAim(Vector2 value)
    {
        CurrentWeaponController.gameObject.transform.localPosition = GetAimVector(value, 1.0f);
        crosshair.gameObject.transform.localPosition = GetAimVector(value, 2.6f);
    }

    private Vector3 GetAimVector(Vector3 point, float range)
    {
        Vector3 aim = new Vector3(point.x, point.y, 0) - this.transform.position;
        aim.Normalize();
        aim *= range;
        return aim;
    }

    private void SetupWeaponRotation()
    {
        Vector2 distance = crosshair.transform.position - CurrentWeaponController.transform.position;
        distance.Normalize();
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        CurrentWeaponController.gameObject.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    #region General Methods

    private void Move()
    {
        if (playerActions.Move.IsPressed)
        {
            DirectionType direction = ConvertAngleToDirection(playerActions.Move.Angle);
            mMoveCommands[direction].Execute();
        }
        else
        {
            Animator.SetBool("isMove", false);
        }
    }
    public void Move(float angle)
    {
        DirectionType direction = ConvertAngleToDirection(playerActions.Move.Angle);
        mMoveCommands[direction].Execute();
    }

    private void Shoot()
    {
        if (playerActions.Shoot.IsPressed)
        {
        }
    }

    private void Dash()
    {
        if (playerActions.Dash.IsPressed)
        {
            mDashCommand.Execute();
        }
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
