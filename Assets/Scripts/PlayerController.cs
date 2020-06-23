using System.Collections;
using System.Collections.Generic;
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

    [Header("Player States")]
    private PlayerState mCurrentPlayerState;
    [HideInInspector()] public ActionPlayerState ActionPlayerState;

    private void Start()
    {
        InitPlayerStates();
        InitCurrentWeapon();
        SetPlayerState(new ActionPlayerState(this));
    }

    private void Update()
    {
        if (mCurrentPlayerState != null)
        {
            Debug.Log(mCurrentPlayerState.GetType());
            mCurrentPlayerState.Tick();
        }
    }

    public void SetPlayerState(PlayerState playerState)
    {
        if (mCurrentPlayerState != null)
            mCurrentPlayerState.OnStateExit();

        mCurrentPlayerState = playerState;
        mCurrentPlayerState.OnStateEnter();
    }

    private void InitPlayerStates()
    {
        ActionPlayerState = new ActionPlayerState(this);
    }
    
    private void InitCurrentWeapon()
    {
        CurrentWeaponController = this.transform.GetChild(0).GetComponent<WeaponController>();
        CurrentWeaponController.Init(true);
    }
}
