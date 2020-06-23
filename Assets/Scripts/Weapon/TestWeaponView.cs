using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeaponView : WeaponView
{
    // SemiautomaticWeaponView aslında.
    public override void InputDetect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnShootInputDetected?.Invoke(this, new EventArgs());
        }
    }

    public override void Shoot()
    {
        //base.Shoot();
        Instantiate(ProjectileObject, this.transform.position, Quaternion.identity);
    }
}