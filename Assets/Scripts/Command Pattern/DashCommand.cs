using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

public class DashCommand : Command
{
    private float mSpeed = 1.7f;
    public float Duration = 0.2f;
    public bool isDashContinues;

    public DashCommand(DirectionType direction, PlayerController playerController) : base(direction, playerController)
    {

    }

    public override void Execute()
    {
        base.Execute();

        if (!isDashContinues)
        {
            isDashContinues = true;

            Vector2 dashDistance = DirectionToVector2(mPlayerController.CurrentPlayerDirection);
            Vector2 newPosition = mPlayerController.GetPosition() + dashDistance;
            mPlayerController.SpriteRenderer.color = new Color(0.27F, 0.28F, 0.31F, 1);
            mPlayerController.RigidBody.DOMove(newPosition, .2f).OnComplete(() =>
            {
                isDashContinues = false;
                mPlayerController.SpriteRenderer.DOColor(Color.white, .2f);
                mPlayerController.Animator.SetBool("isDash", false);
            });

            mPlayerController.StartCoroutine(GhostCoroutine());
        }
    }

    private void RayTest(Vector2 direction)
    {
        var rigid = mPlayerController.RigidBody;
        mPlayerController.GetComponent<Collider2D>().enabled = false;
        RaycastHit2D[] hits =  Physics2D.RaycastAll(mPlayerController.GetPosition(), direction);
    }

    private IEnumerator GhostCoroutine()
    {
        float color = 1;
        Vector2 lastPoint = mPlayerController.GetPosition();

        List<GameObject> ghosts = new List<GameObject>();

        while (isDashContinues)
        {
            if (lastPoint != mPlayerController.GetPosition())
            {
                lastPoint = mPlayerController.GetPosition();
                GameObject ghost = new GameObject();
                ghost.transform.position = mPlayerController.GetPosition();
                ghost.AddComponent<SpriteRenderer>().sprite = mPlayerController.SpriteRenderer.sprite;
                color -= 0.2f;
                ghost.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, color);
                ghosts.Add(ghost);
            }

            yield return new WaitForSeconds(0.05f);
        }

        foreach (var item in ghosts)
        {
            item.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.2f).OnComplete(() => Object.Destroy(item));
            yield return new WaitForSeconds(0.2f);
        }

        ghosts.Clear();
    }

    private Vector2 DirectionToVector2(DirectionType type)
    {
        Vector2 direction = Vector2.zero;
        switch (type)
        {
            case DirectionType.UpLeft:
                direction = new Vector2(-1 * mSpeed, mSpeed);
                break;
            case DirectionType.Forward:
                direction = new Vector2(0, mSpeed);
                break;
            case DirectionType.UpRight:
                direction = new Vector2(mSpeed, mSpeed);
                break;
            case DirectionType.Left:
                direction = new Vector2(-1 * mSpeed, 0);
                break;
            case DirectionType.Right:
                direction = new Vector2(mSpeed, 0);
                break;
            case DirectionType.DownLeft:
                direction = new Vector2(-1 * mSpeed, -1 * mSpeed);
                break;
            case DirectionType.Backward:
                direction = new Vector2(0, -1 * mSpeed);
                break;
            case DirectionType.DownRight:
                direction = new Vector2(mSpeed, -1 * mSpeed);
                break;
            default:
                break;
        }
        return direction;
    }
}
