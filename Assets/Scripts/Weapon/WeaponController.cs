using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponView View;
    public WeaponModel Model;
    public bool IsPlayerWeapon; // Player classı falan gelebilir :D

    public WeaponData Data; // Public olmasi tartisilir

    public void Init(bool isPlayerWeapon)
    {
        Model = Data.GetWeaponDataInstance();
        Model.OnMagazineSizeChanged += OnMagazineSizeChanged;
        View.OnShootDetected += OnShootDetected;
        View.Init(Model.ShootType, IsPlayerWeapon, Model.ProjectileObject);
    }

    private void OnMagazineSizeChanged(object sender, EventArgs e)
    {
        if (IsPlayerWeapon)
        {
            UIManager.Instance.UIView.UpdateMagazineText(Model.MagazineSize);
        }
    }

    private void OnShootDetected(object sender, EventArgs e)
    {
        Model.MagazineSize--;
        View.Shoot();
    }
}