﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootType
{
    Semiautomatic, //  Default olarak projectile atma
    Beam, // Basılı tutarak ışın atma
    Charged, // Basılı tutup bırakarak atma
    Burst,
    Automatic
}
[CreateAssetMenu(fileName = "Weapon",menuName = "Create Weapon", order = 51)]
public class WeaponModel : ScriptableObject
{
    public Guid ID = new Guid();
    public string Name;
    public ShootType ShootType;
    public int MagazineSize;
    public int MaxAmmo;
    public int Damage;
    public float Range;
    public float FireRate;
    public float ReloadTime;
    public int Force;
    public int Spread;
    public Sprite View;
    public Animator Animator;
    public GameObject ProjectileObject;

    [Header("Events")]
    public EventHandler<EventArgs> OnMagazineSizeChanged;

    public WeaponModel GetWeaponDataInstance()
    {
        WeaponModel holder = new WeaponModel()
        {
            ID               = this.ID,
            Name             = this.Name,
            ShootType        = this.ShootType,
            MagazineSize     = this.MagazineSize,
            MaxAmmo          = this.MaxAmmo,
            Damage           = this.Damage,
            Range            = this.Range,
            FireRate         = this.FireRate,
            ReloadTime       = this.ReloadTime,
            Force            = this.Force,
            Spread           = this.Spread,
            View             = this.View,
            Animator         = this.Animator,
            ProjectileObject = this.ProjectileObject
        };
        
        return holder;
    }
}