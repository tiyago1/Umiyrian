using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class LookAt2D : MonoBehaviour
{
    [SerializeField] private Transform target;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void Update()
    {
        if (target == null)
        {
            return;
        }
        else
        {
            Debug.LogError("Target is null");
        }

        Vector3 direction = target.transform.position - this.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}