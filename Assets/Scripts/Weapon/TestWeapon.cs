using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : Weapon
{
    public override void Awake()
    {
        base.Awake();
        View.OnShootDetected += OnShootDetected;
    }

    public override void Shoot()
    {
        Debug.Log("TestWeapon Shoot and " + Model.Damage + " taken damage!");
        Model.MagazineSize--;
    }
    public override void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("TestWeapon Reload");
        }
    }

    private void OnShootDetected(object sender, EventArgs e)
    {
        //if (Model.MagazineSize > 0)
        //{
            Shoot();
        //}
    }
}
