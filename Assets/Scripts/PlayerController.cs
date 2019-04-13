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

    [Header("Player States")]
    private PlayerState mCurrentPlayerState;
    [HideInInspector()] public MovePlayerState MovePlayerState;
    [HideInInspector()] public IdlePlayerState IdlePlayerState;

    private void Start()
    {
        InitPlayerStates();
        SetPlayerState(new IdlePlayerState(this));
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
        MovePlayerState = new MovePlayerState(this);
        IdlePlayerState = new IdlePlayerState(this);
    }
}
