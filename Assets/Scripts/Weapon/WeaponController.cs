using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponView View;
    public WeaponModel Model;

    public void Init()
    {
        Model = Instantiate(Model);
        Model.OnMagazineSizeChanged += OnMagazineSizeChanged;
        View.OnShootInputDetected += OnShootDetected;
        View.Init(Model.ProjectileObject);
    }

    private void OnMagazineSizeChanged(object sender, EventArgs e)
    {
        UIManager.Instance.UIView.UpdateMagazineText(Model.MagazineSize);
    }

    private void OnShootDetected(object sender, EventArgs e)
    {
        Model.MagazineSize--;
        View.Shoot();
    }
}