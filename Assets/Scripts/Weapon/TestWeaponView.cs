using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeaponView : WeaponView // SemiautomaticWeaponView aslında.
{
    public GameObject cross;
    public override void InputDetect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnShootInputDetected?.Invoke(this, new EventArgs());
        }
    }

    public override void Update()
    {
        base.Update();
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //LookAtMouse(mouseScreenPosition);
        //MoveToRange(mouseScreenPosition);
        //Test(mouseScreenPosition, this.gameObject, 2);
        //Test(mouseScreenPosition, cross, 5);

    }

    private void Test(Vector3 mouseScreenPosition, GameObject gameObject, float range)
    {
        Vector3 aim = new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, 0);
        aim.Normalize();
        aim *= range;
        gameObject.transform.localPosition = aim;
    }

    private void MoveToRange(Vector3 mouseScreenPosition)
    {
        float moveRange = 2;
        Vector3 centerPosition = this.transform.parent.position;

        Debug.Log($"mouse {mouseScreenPosition.normalized} center : {centerPosition.normalized}");
        float distance = Vector3.Distance(mouseScreenPosition, centerPosition);

        if (distance > moveRange) //If the distance is less than the radius, it is already within the circle.
        {
            Vector3 fromOriginToObject = mouseScreenPosition - centerPosition;
            fromOriginToObject *= moveRange / distance; //Multiply by radius //Divide by Distance
            this.transform.position = centerPosition + fromOriginToObject;
        }
    }

    private void LookAtMouse(Vector3 mouseScreenPosition)
    {
        Vector3 direction = mouseScreenPosition - this.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void LookAtGamepad(float angle)
    {
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}